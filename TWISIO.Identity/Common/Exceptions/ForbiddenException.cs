namespace TWISIO.Identity.API.Common.Exceptions
{
    /// <summary>
    /// Исключение запрещённого доступа
    /// </summary>
    public class ForbiddenException : Exception
    {
        /// <summary>
        /// Инициализация исключения с кастомным сообщением
        /// </summary>
        /// <param name="message">Сообщение исключения</param>
        public ForbiddenException(string message) : base(message) { }
    }
}
