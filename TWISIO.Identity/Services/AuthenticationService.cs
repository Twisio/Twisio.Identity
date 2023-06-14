using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TWISIO.Identity.Application.Common.Options;
using TWISIO.Identity.Application.DTOs;

namespace TWISIO.Identity.API.Services
{
    public static class AuthenticationService
    {
        public static IServiceCollection AddAuthenticationService(this IServiceCollection services,
            JwtOptionsDto jwtOptions)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = jwtOptions.ISSUER,
                        ValidateAudience = true,
                        ValidAudience = jwtOptions.AUDIENCE,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new JwtOptions(jwtOptions.KEY).GetSymmetricSecurityKey()
                    };
                });

            return services;
        }
    }
}
