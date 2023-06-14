namespace TWISIO.Identity.Application.DTOs
{
    /// <summary>
    /// DTO настроек JWT-токена
    /// </summary>
    public class JwtOptionsDto
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string ISSUER { get; set; } = string.Empty;
        /// <summary>
        /// Аудитория токена
        /// </summary>
        public string AUDIENCE { get; set; } = string.Empty;
        /// <summary>
        /// Ключ шифрования
        /// </summary>
        public string KEY { get; set; } = string.Empty;

        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public TimeSpan EXPIRES { get; set; } = TimeSpan.Zero;
        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public TimeSpan RefreshTokenExpires { get; set; } = TimeSpan.Zero;
    }
}
