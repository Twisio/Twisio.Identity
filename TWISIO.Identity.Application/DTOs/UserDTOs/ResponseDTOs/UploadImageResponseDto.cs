namespace TWISIO.Identity.Application.DTOs.UserDTOs.ResponseDTOs
{
    /// <summary>
    /// DTO, возвращаемое из метода загрузки изображения профиля
    /// </summary>
    public class UploadImageResponseDto
    {
        /// <summary>
        /// Ссылка на загруженное изображение
        /// </summary>
        public string Url { get; set; } = null!;
    }
}
