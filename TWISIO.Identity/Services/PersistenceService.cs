using TWISIO.Identity.Application.Common.Options;
using TWISIO.Identity.Persistence;

namespace TWISIO.Identity.API.Services
{
    public static class PersistenceService
    {
        public static IServiceCollection AddPersistenceService(this IServiceCollection services,
            string connectionString, IConfiguration configuration)
        {
            services.AddPersistence(connectionString,
                new EmailSenderOptions
                {
                    Name = configuration["SMTP:Name"]!,
                    Username = configuration["SMTP:Username"]!,
                    Password = configuration["SMTP:Password"]!,
                    Host = configuration["SMTP:Host"]!,
                    Port = Convert.ToInt32(configuration["SMTP:Port"])
                });

            return services;
        }
    }
}
