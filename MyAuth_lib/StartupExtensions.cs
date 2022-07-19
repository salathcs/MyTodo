using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MyAuth_lib.Auth_Client;
using MyAuth_lib.Auth_Server;
using MyAuth_lib.Interfaces;
using System.Text;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyAuthServer(this IServiceCollection services)
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

            //service
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddMyAuthClient(this IServiceCollection services)
        {
            services.AddAuthentication("DefaultAuth")
                .AddScheme<AuthenticationSchemeOptions, FailingAuthenticationHandler>("DefaultAuth", null);

            services.AddSingleton<IAuthorizationPolicyProvider, ClientPolicyProvider>();

            services.AddScoped<IAuthorizationHandler, ClientJwtAuthReqHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("General", builder => builder.AddRequirements(new ClientJwtAuthReq("General")));
                options.AddPolicy("Admin", builder => builder.AddRequirements(new ClientJwtAuthReq("Admin")));
            });

            services.AddHttpClient()
                .AddHttpContextAccessor()
                .AddMemoryCache();

            return services;
        }
    }
}
