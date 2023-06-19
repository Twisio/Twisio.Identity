using TWISIO.Identity.API.Entities.Enums;

namespace TWISIO.Identity.API.Entities
{
    /// <summary>
    /// Класс лога
    /// </summary>
    public class Log
    {
        /// <summary>
        /// Идентификатор лога
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }
        /// <summary>
        /// Сообщение лога
        /// </summary>
        public string Message { get; set; } = null!;
        /// <summary>
        /// Внутреннее сообщение лога
        /// </summary>
        public string? InnerMessage { get; set; }
        /// <summary>
        /// Тип лога
        /// </summary>
        public LogType Type { get; set; }
        /// <summary>
        /// Дата лога
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Пользователь, к которому привязан лог
        /// </summary>
        public User? User { get; set; }
    }
}
