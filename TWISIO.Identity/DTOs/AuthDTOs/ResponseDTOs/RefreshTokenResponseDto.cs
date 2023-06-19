namespace TWISIO.Identity.API.DTOs.AuthDTOs.ResponseDTOs
{
    /// <summary>
    /// DTO, возвращаемое из метода обновления токена доступа
    /// </summary>
    public class RefreshTokenResponseDto
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        public string access_token { get; set; } = string.Empty;
        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public long expires { get; set; }
        /// <summary>
        /// Токен обновления
        /// </summary>
        public string refresh_token { get; set; } = string.Empty;
        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public long? refresh_token_expires { get; set; }
    }
}
