namespace TWISIO.Identity.Application.DTOs.UserDTOs.ResponseDTOs
{
    /// <summary>
    /// DTO, возвращаемое из метода получения пользователя
    /// </summary>
    public class UserShortResponseDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; } = null!;
        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; } = null!;
        /// <summary>
        /// Ссылка на изображение профиля
        /// </summary>
        public string ImageUrl { get; set; } = null!;
    }
}
