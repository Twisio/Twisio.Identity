using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO повторной отправки письма подтверждения
    /// </summary>
    public class ResendEmailConfirmationDto
    {
        /// <summary>
        /// Зарегистрированная почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректная почта")]
        public string Email { get; set; } = string.Empty;
    }
}
