namespace TWISIO.Identity.Application.DTOs.AuthDTOs.ResponseDTOs
{
    /// <summary>
    /// DTO, возвращаемое из метода регистрации
    /// </summary>
    public class SignUpResponseDto
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid userId { get; set; }
    }
}
