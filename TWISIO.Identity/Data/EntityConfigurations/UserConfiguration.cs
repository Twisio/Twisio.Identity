using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Data.EntityConfigurations
{
    /// <summary>
    /// Класс конфигурации таблицы пользователя
    /// </summary>
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.Id).IsUnique();

            builder.Property(x => x.Role).IsRequired();

            builder.Property(x => x.Email).IsRequired();

            builder.Property(x => x.UserName).IsRequired();

            builder.HasMany(u => u.Logs).WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
        }
    }
}
