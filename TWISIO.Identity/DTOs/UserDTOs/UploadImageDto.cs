using System.ComponentModel.DataAnnotations;
using TWISIO.Identity.API.Common.Attributes;

namespace TWISIO.Identity.API.DTOs.UserDTOs
{
    /// <summary>
    /// DTO для загрузки изображения профиля
    /// </summary>
    public class UploadImageDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }
        /// <summary>
        /// Файл с изображением
        /// </summary>
        [Required(ErrorMessage = "Файл обязателен")]
        [ExtensionValidator(Extensions = "jpg,jpeg,png")]
        public IFormFile File { get; set; } = null!;
    }
}
