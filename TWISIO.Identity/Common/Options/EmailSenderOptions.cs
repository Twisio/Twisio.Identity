using TWISIO.Identity.API.Interfaces;

namespace TWISIO.Identity.API.Common.Options
{
    /// <summary>
    /// Класс с настройками для <see cref="IEmailSender"/>
    /// </summary>
    public class EmailSenderOptions
    {
        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Имя пользователя от SMTP клиента
        /// </summary>
        public string Username { get; set; } = string.Empty;
        /// <summary>
        /// Пароль от SMTP клиента
        /// </summary>
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Адрес SMTP клиента
        /// </summary>
        public string Host { get; set; } = string.Empty;
        /// <summary>
        /// Порт SMTP клиента
        /// </summary>
        public int Port { get; set; } = int.MaxValue;
    }
}
