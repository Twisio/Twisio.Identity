using System.ComponentModel.DataAnnotations;

namespace TWISIO.Identity.API.DTOs.UserDTOs
{
    /// <summary>
    /// DTO для получения информации о пользователе по идентификатору
    /// </summary>
    public class GetUserByIdDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; } = Guid.Empty;
    }
}
