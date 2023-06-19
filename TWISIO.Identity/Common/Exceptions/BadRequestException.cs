using System.Runtime.Serialization;

namespace TWISIO.Identity.API.Common.Exceptions
{
    /// <summary>
    /// Исключение некорректных данных
    /// </summary>
    public class BadRequestException : Exception, ISerializable
    {
        /// <summary>
        /// Инициализация исключения с кастомным сообщением
        /// </summary>
        /// <param name="message">Сообщение исключения</param>
        public BadRequestException(string message) : base(message) { }
    }
}
