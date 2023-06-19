namespace TWISIO.Identity.API.Interfaces
{
    /// <summary>
    /// Интерфейс отправщика сообщений
    /// </summary>
    public interface IEmailSender
    {
        /// <summary>
        /// Отправить сообщение пользователю асинхронно
        /// </summary>
        /// <param name="email">Адрес почты пользователя</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);

        /// <summary>
        /// Отправить сообщение на корпоративную почту асинхронно
        /// </summary>
        /// <param name="subject">Тема письма</param>
        /// <param name="message">Текст сообщения</param>
        /// <returns></returns>
        Task SendEmailAsync(string subject, string message);
    }
}
