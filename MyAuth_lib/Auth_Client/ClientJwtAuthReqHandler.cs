using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MyAuth_lib.Interfaces;
using System.Security.Claims;
using System.Text.Json;
using System.Web;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib.Auth_Client
{
    /// <summary>
    /// Client requirement handler for all requests, because it validates them at the validation server, and also the token!
    /// </summary>
    public class ClientJwtAuthReqHandler : AuthorizationHandler<ClientJwtAuthReq>
    {
        private readonly HttpClient client;
        private readonly HttpContext httpContext;
        private readonly IMemoryCache cache;
        private readonly IAuthClientSupplier supplier;

        public ClientJwtAuthReqHandler(
            IHttpClientFactory httpClientFactory,
            IHttpContextAccessor httpContextAccessor,
            IMemoryCache cache,
            IAuthClientSupplier supplier)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(HttpContext));
            }

            client = httpClientFactory.CreateClient();
            httpContext = httpContextAccessor.HttpContext;
            this.cache = cache;
            this.supplier = supplier;
        }

        /// <inheritdoc />
        protected override async Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ClientJwtAuthReq requirement)
        {
            if (TryGetToken(out var token))
            {
                //If cache key exists, req is succeeded
                if (cache.TryGetValue(CreateCacheKey(token, requirement.Policy), out var cacheValue))
                {
                    context.Succeed(requirement);

                    if (cacheValue is IEnumerable<ClaimDto> cachedClaims)
                    {
                        httpContext.User.AddIdentity(CreateIdentity(cachedClaims as IEnumerable<ClaimDto>));
                    }

                    return;
                }

                //get validation from server
                var request = new HttpRequestMessage(HttpMethod.Get, CreateValidationUri(requirement.Policy));
                request.Headers.Add("Authorization", $"Bearer {token}");
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var claims = await GetClaims(response);

                    if (claims != null)
                    {
                        context.Succeed(requirement);

                        httpContext.User.AddIdentity(CreateIdentity(claims));

                        //set cache
                        cache.Set(
                            CreateCacheKey(token, requirement.Policy), 
                            claims, 
                            DateTimeOffset.UtcNow.AddMinutes(supplier.GetCacheExpiration()));
                    }
                }
            }
        }

        private bool TryGetToken(out string token)
        {
            //auth header
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                var splitted = authHeader.ToString().Split(' ');
                if (splitted.Length > 1)
                {
                    token = splitted[1];
                    return true;
                }
            }

            //auth cookie
            var tokenFromCookie = httpContext.Request.Cookies[AUTH_COOKIE];
            if (tokenFromCookie != null)
            {
                token = tokenFromCookie;
                return true;
            }

            token = string.Empty;
            return false;
        }

        private string CreateValidationUri(string policy)
        {
            var uriBuilder = new UriBuilder(supplier.GetValidationUrl());
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query[QUERY_POLICY] = policy;
            uriBuilder.Query = query.ToString();
            return uriBuilder.ToString();
        }

        private object CreateCacheKey(string accessToken, string policy) =>
            new
            {
                accessToken,
                policy
            };
        private async Task<IEnumerable<ClaimDto>?> GetClaims(HttpResponseMessage response)
        {
            try
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<IEnumerable<ClaimDto>>(jsonString);
            }
            catch
            {
                return null;
            }
        }

        private ClaimsIdentity CreateIdentity(IEnumerable<ClaimDto> claimDtos)
        {
            return new ClaimsIdentity(claimDtos.Select(x => new Claim(x.Type ?? string.Empty, x.Value ?? string.Empty)));
        }
    }
}
