using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TWISIO.Identity.Application.Interfaces;
using TWISIO.Identity.Domain;

namespace TWISIO.Identity.Persistence
{
    /// <inheritdoc/>
    public class DBContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>, IDBContext
    {
        /// <summary>
        /// Конструктор, инициализирующий первоначальные настройки контекста
        /// </summary>
        /// <param name="options">Первоначальные настройки</param>
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }

        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

    }
}
