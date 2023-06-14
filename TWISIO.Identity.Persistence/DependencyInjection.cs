using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TWISIO.Identity.Application.Common.Options;
using TWISIO.Identity.Application.Interfaces;
using TWISIO.Identity.Persistence.Services;

namespace TWISIO.Identity.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            string connectionString, EmailSenderOptions emailSenderOptions)
        {
            services.AddDbContext<DBContext>(options => options.UseNpgsql(connectionString));
            services.AddScoped<IDBContext>(provider => provider.GetService<DBContext>()!);
            services.AddScoped<IEmailSender>(x => new EmailSender(emailSenderOptions));
            services.AddScoped<IFileUploader, FileUploader>();

            return services;
        }
    }
}
