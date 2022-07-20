using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using MyAuth_lib.Interfaces;
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
                if (cache.TryGetValue(CreateCacheKey(token, requirement.Policy), out var _))
                {
                    context.Succeed(requirement);

                    return;
                }

                //get validation from server
                var request = new HttpRequestMessage(HttpMethod.Get, CreateValidationUri(requirement.Policy));
                request.Headers.Add("Authorization", $"Bearer {token}");
                var response = await client.SendAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    context.Succeed(requirement);

                    //set cache
                    cache.Set(CreateCacheKey(token, requirement.Policy), string.Empty, DateTimeOffset.UtcNow.AddMinutes(supplier.GetCacheExpiration()));
                }
            }
        }

        private bool TryGetToken(out string token)
        {
            //auth header
            if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                token = authHeader.ToString().Split(' ')[1];
                return true;
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
            var uriBuilder = new UriBuilder(supplier.GetValidationUrl());        //TODO
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
    }
}
