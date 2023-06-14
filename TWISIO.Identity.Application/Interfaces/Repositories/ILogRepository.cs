using TWISIO.Identity.Domain;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория логгера
    /// </summary>
    public interface ILogRepository
    {
        /// <summary>
        /// Сохранить лог в базу данных
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="type">Тип лога</param>
        /// <returns></returns>
        Task AddAsync(string message, LogType type);
        /// <summary>
        /// Сохранить лог в базу данных
        /// </summary>
        /// <param name="message">Текст сообщения</param>
        /// <param name="innerMessage">Внутренний текст сообщения </param>
        /// <param name="type">Тип лога</param>
        /// <returns></returns>
        Task AddAsync(string message, string? innerMessage, LogType type);
        /// <summary>
        /// Сохранить лог в базу данных
        /// </summary>
        /// <param name="user">Пользователь, к которому относится лог</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="type">Тип лога</param>
        /// <returns></returns>
        Task AddAsync(User user, string message, LogType type);
        /// <summary>
        /// Сохранить лог в базу данных
        /// </summary>
        /// <param name="user">Пользователь, к которому относится лог</param>
        /// <param name="message">Текст сообщения</param>
        /// <param name="innerMessage">Внутренний текст сообщения </param>
        /// <param name="type">Тип лога</param>
        /// <returns></returns>
        Task AddAsync(User user, string message, string innerMessage, LogType type);
    }
}
