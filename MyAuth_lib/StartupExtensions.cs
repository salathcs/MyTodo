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
using static MyAuth_lib.Constants.PolicyConstants;
using static Entities.Constants.PermissionNames;
using Microsoft.OpenApi.Models;

namespace MyAuth_lib
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyAuthServer<IdentityRepo, Supplier>(this IServiceCollection services)
            where IdentityRepo : IIdentityRepository
            where Supplier : IAuthServerSupplier
        {
            var key = Encoding.UTF8.GetBytes(ENCRYPTION_KEY);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
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
            services.AddScoped(typeof(IAuthServerSupplier), typeof(Supplier));

            //policy provider
            services.AddSingleton<IAuthorizationPolicyProvider, AuthServerPolicyProvider>();

            //reqHandlers
            services.AddScoped<IAuthorizationHandler, PermissionReqHandler>();

            //policies
            services.AddAuthorization(options =>
            {
                options.AddPolicy(ADMIN_POLICY, builder => builder.AddRequirements(new PermissionReq(ADMIN_PERMISSION)));
            });

            services.AddHttpContextAccessor();

            return services;
        }

        public static IServiceCollection AddMyAuthClient<Supplier>(this IServiceCollection services)
            where Supplier : IAuthClientSupplier
        {
            services.AddAuthentication(CLIENT_AUTHENTICATION_SCHEMA)
                .AddScheme<AuthenticationSchemeOptions, FailingAuthenticationHandler>(CLIENT_AUTHENTICATION_SCHEMA, null);

            services.AddSingleton<IAuthorizationPolicyProvider, ClientPolicyProvider>();

            services.AddScoped<IAuthorizationHandler, ClientJwtAuthReqHandler>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(ADMIN_POLICY, builder => builder.AddRequirements(new ClientJwtAuthReq(ADMIN_POLICY)));
            });

            services.AddHttpClient()
                .AddHttpContextAccessor()
                .AddMemoryCache();

            services.AddScoped(typeof(IAuthClientSupplier), typeof(Supplier));

            return services;
        }

        public static IServiceCollection AddSwaggerGenWithJwtAuth(this IServiceCollection services, string swaggerTitle, string version = "v1")
        {
            services.AddSwaggerGen(option =>
            {
                option.SwaggerDoc(version, new OpenApiInfo { Title = swaggerTitle, Version = version });
                option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });
                option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });

            return services;
        }
    }
}
