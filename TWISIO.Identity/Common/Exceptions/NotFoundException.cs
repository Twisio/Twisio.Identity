using System.Runtime.Serialization;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Common.Exceptions
{
    /// <summary>
    /// Исключение, когда сущность необходимая сущность не найдена
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Инициализация исключения с кастомным сообщением
        /// </summary>
        /// <param name="message">Сообщение исключения</param>
        public NotFoundException(string message) : base(message) { }
        /// <summary>
        /// Инициализация исключения с сообщением отсутсвия пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        public NotFoundException(User? user) : base("Пользователь не найден") { }
    }
}
