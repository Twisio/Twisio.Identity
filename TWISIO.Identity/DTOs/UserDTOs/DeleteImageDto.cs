using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.API.DTOs.UserDTOs
{
    /// <summary>
    /// DTO для удаления изображения профиля
    /// </summary>
    public class DeleteImageDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }
    }
}
