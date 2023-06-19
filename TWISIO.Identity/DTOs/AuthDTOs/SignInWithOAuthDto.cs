using System.ComponentModel.DataAnnotations;
using TWISIO.Identity.API.Entities.Enums;

namespace TWISIO.Identity.API.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO для авторизации пользователя через oAuth
    /// </summary>
    public class SignInWithOAuthDto
    {
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
        /// Сервис авторизации
        /// </summary>
        [Required(ErrorMessage = "Сервис авторизации обязателен")]
        public OAuthService Service { get; set; }
    }
}
