using Microsoft.EntityFrameworkCore;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Interfaces
{
    /// <summary>
    /// Интерфейс, описывающий таблицы, используемые в проекте
    /// </summary>
    public interface IDBContext
    {
        /// <summary>
        /// Получить/установить список пользователей
        /// </summary>
        DbSet<User> Users { get; set; }
        /// <summary>
        /// Получить/установить список логов
        /// </summary>
        DbSet<Log> Logs { get; set; }

        /// <summary>
        ///     Ассинхронно сохраняет сделанные изменения
        /// </summary>
        /// <param name="cancellationToken">Токен отмены операции</param>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
