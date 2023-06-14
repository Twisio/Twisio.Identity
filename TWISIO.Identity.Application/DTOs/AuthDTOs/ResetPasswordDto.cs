using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO для смены пароля
    /// </summary>
    public class ResetPasswordDto
    {
        /// <summary>
        /// Зарегистрированная почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректная почта")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Код подтверждения из письма
        /// </summary>
        [Required(ErrorMessage = "Код обязателен")]
        [MinLength(6, ErrorMessage = "Длина кода должна быть равна 6 символам")]
        [MaxLength(6, ErrorMessage = "Длина кода должна быть равна 6 символам")]
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Новый пароль
        /// </summary>
        [Required(ErrorMessage = "Новый пароль обязателен")]
        [MinLength(8, ErrorMessage = "Длина пароля должна быть не меньше 8 символов")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}
