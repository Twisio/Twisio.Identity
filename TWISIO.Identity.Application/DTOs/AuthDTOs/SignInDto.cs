using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.Application.DTOs.AuthDTOs
{
    /// <summary>
    /// DTO для авторизации
    /// </summary>
    public class SignInDto
    {
        /// <summary>
        /// Зарегистрированная почта пользователя
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
        /// Нужен токен обновления или нет
        /// </summary>
        public bool RememberMe { get; set; } = false;
    }
}
