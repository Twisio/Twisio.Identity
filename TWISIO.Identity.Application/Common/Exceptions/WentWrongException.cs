namespace TWISIO.Identity.Application.Common.Exceptions
{
    /// <summary>
    /// Исключение "Что-то пошло не так"
    /// </summary>
    public class WentWrongException : Exception
    {
        /// <summary>
        /// Инициализация исключения со кастомным сообщением
        /// </summary>
        public WentWrongException(string message) : base(message) { }

        /// <summary>
        /// Инициализация исключения со стандартным сообщением
        /// </summary>
        public WentWrongException() : base("Что-то пошло не так...") { }
    }
}
