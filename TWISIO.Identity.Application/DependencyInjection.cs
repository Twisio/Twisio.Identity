using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TWISIO.Identity.Application.Common.Managers;
using TWISIO.Identity.Application.DTOs;
using TWISIO.Identity.Application.Interfaces;
using TWISIO.Identity.Application.Interfaces.Repositories;
using TWISIO.Identity.Application.Repositories;

namespace TWISIO.Identity.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            JwtOptionsDto jwtOptions)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<ILogRepository, LogRepository>();
            services.AddTransient<IUserRepository, UserRepository>();

            var dbContext = services.BuildServiceProvider().GetService<IDBContext>();

            if (dbContext is not null)
                services.AddTransient<ITokenManager>(x => new TokenManager(jwtOptions));

            return services;
        }
    }
}
