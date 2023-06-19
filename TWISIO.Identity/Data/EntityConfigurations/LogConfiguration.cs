using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Data.EntityConfigurations
{
    /// <summary>
    /// Класс конфигурации таблицы логов
    /// </summary>
    public class LogConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder.ToTable("Logs");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Message).IsRequired();
            builder.Property(x => x.Type).IsRequired();
            builder.Property(x => x.Date).IsRequired();
        }
    }
}
