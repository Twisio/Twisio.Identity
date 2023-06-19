using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.API.DTOs.UserDTOs
{
    /// <summary>
    /// DTO для обновления персональных данных пользователя
    /// </summary>
    public class UpdateUserDetailsDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }
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
        /// Дата рождения
        /// </summary>
        public DateTime? DateOfBirth { get; set; }
        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string? PhoneNumber { get; set; }
    }
}
