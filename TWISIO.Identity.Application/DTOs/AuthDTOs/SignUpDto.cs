using System.ComponentModel.DataAnnotations;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO для регистрации
    /// </summary>
    public class SignUpDto
    {
        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        [Required(ErrorMessage = "Псевдоним обязателен")]
        public string UserName { get; set; } = null!;
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректная почта")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Пароль пользователя
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        [MinLength(8, ErrorMessage = "Длина пароля должна быть не меньше 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required(ErrorMessage = "Роль обязательна")]
        public Roles Role { get; set; }
    }
}
