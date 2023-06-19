namespace TWISIO.Identity.API.DTOs.UserDTOs.ResponseDTOs
{
    /// <summary>
    /// DTO, возвращаемое из метода получения информации о пользователе
    /// </summary>
    public class UserResponseDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Почта пользователя
        /// </summary>
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Номер телефона пользователя
        /// </summary>
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
