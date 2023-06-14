using System.ComponentModel.DataAnnotations;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO для регистрации пользователя через oAuth
    /// </summary>
    public class SignUpWithOAuthDto
    {
        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        [Required(ErrorMessage = "Псевдоним обязателен")]
        public string Username { get; set; } = null!;
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required(ErrorMessage = "Почта обязательна")]
        [EmailAddress(ErrorMessage = "Некорректная почта")]
        public string Email { get; set; } = null!;
        /// <summary>
        /// Идентификатор пользователя из стороннего сервиса
        /// </summary>
        [Required(ErrorMessage = "Идентификатор обязателен")]
        public string Id { get; set; } = null!;
        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Required(ErrorMessage = "Роль обязательна")]
        public Roles Role { get; set; }
        /// <summary>
        /// Сервис авторизации
        /// </summary>
        [Required(ErrorMessage = "Сервис авторизации обязателен")]
        public OAuthService Service { get; set; }
    }
}
