using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO подтверждения почты
    /// </summary>
    public class ConfirmEmailDto
    {
        /// <summary>
        /// Почта пользователя
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
    }
}
