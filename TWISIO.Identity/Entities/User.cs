using Microsoft.AspNetCore.Identity;
using TWISIO.Identity.API.Entities.Enums;

namespace TWISIO.Identity.API.Entities
{
    /// <summary>
    /// Класс пользователя
    /// </summary>
    public class User : IdentityUser<Guid>
    {
        /// <summary>
        /// Полное имя пользователя
        /// </summary>
        public string? FullName { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string? FirstName { get; set; }
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string? MiddleName { get; set; }
        /// <summary>
        /// Информация о себе
        /// </summary>
        public string? AboutMe { get; set; }
        /// <summary>
        /// Ссылка на изображение профиля
        /// </summary>
        public string? ImageUrl { get; set; }
        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Дата регистрации
        /// </summary>
        public DateTime DateOfCreation { get; set; }
        /// <summary>
        /// Роль пользователя
        /// </summary>
        public Roles Role { get; set; } = Roles.NONE;
        /// <summary>
        /// Код подтверждения
        /// </summary>
        public string? ConfirmationCode { get; set; }
        /// <summary>
        /// Идентификатор пользователя в VK
        /// </summary>
        public string? VKontakteId { get; set; }
        /// <summary>
        /// Идентификатор пользователя в Yandex
        /// </summary>
        public string? YandexId { get; set; }
        /// <summary>
        /// Идентификатор пользователя в Google
        /// </summary>
        public string? GoogleId { get; set; }
        /// <summary>
        /// Идентификатор пользователя в Telegram
        /// </summary>
        public string? TelegramId { get; set; }

        /// <summary>
        /// Логи пользователя
        /// </summary>
        public List<Log> Logs { get; set; } = new List<Log>();
    }
}
