using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyAuth_lib.Auth_Client;
using MyAuth_lib.Auth_Server;
using MyAuth_lib.Interfaces;
using MyAuth_lib.MyAuthPolicies.RequirementHandlers;
using MyAuth_lib.MyAuthPolicies.Requirements;
using System.Text;
using static MyAuth_lib.Constants.AuthConstants;
using static MyAuth_lib.Constants.Policies;

namespace MyAuth_lib
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyAuthServer<IdentityRepo>(this IServiceCollection services)
            where IdentityRepo : IIdentityRepository
        {
            var key = Encoding.UTF8.GetBytes(ENCRYPTION_KEY);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidAudience = AUDIENCE,
                ValidIssuer = ISSUER,
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            var tokenFromCookie = context.Request.Cookies[AUTH_COOKIE];
                            if (tokenFromCookie != null)
                            {
                                context.Token = tokenFromCookie;
                            }
                            return Task.CompletedTask;
                        }
                    };

                    options.SaveToken = true;
                    options.TokenValidationParameters = validationParameters;
                });

            //DI
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped(typeof(IIdentityRepository), typeof(IdentityRepo));

            //policy provider
            services.AddSingleton<IAuthorizationPolicyProvider, AuthServerPolicyProvider>();

            //reqHandlers
            services.AddScoped<IAuthorizationHandler, GeneralReqHandler>();
            services.AddScoped<IAuthorizationHandler, AdminReqHandler>();

            //policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(GENERAL, builder => builder.AddRequirements(new GeneralReq()));
                options.AddPolicy(ADMIN, builder => builder.AddRequirements(new AdminReq()));
            });

            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddMyAuthClient(this IServiceCollection services)
        {
            services.AddAuthentication(CLIENT_AUTHENTICATION_SCHEMA)
                .AddScheme<AuthenticationSchemeOptions, FailingAuthenticationHandler>(CLIENT_AUTHENTICATION_SCHEMA, null);

            services.AddSingleton<IAuthorizationPolicyProvider, ClientPolicyProvider>();

            services.AddScoped<IAuthorizationHandler, ClientJwtAuthReqHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(GENERAL, builder => builder.AddRequirements(new ClientJwtAuthReq(GENERAL)));
                options.AddPolicy(ADMIN, builder => builder.AddRequirements(new ClientJwtAuthReq(ADMIN)));
            });

            services.AddHttpClient()
                .AddHttpContextAccessor()
                .AddMemoryCache();

            return services;
        }
    }
}
