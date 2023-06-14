namespace TWISIO.Identity.API.Models
{
    /// <summary>
    /// Модель HTTP ошибки
    /// </summary>
    public class ErrorModel
    {
        /// <summary>
        /// HTTP статус код
        /// </summary>
        public int StatusCode { get; set; } = int.MinValue;
        /// <summary>
        /// Сообщение ошибки
        /// </summary>
        public string Message { get; set; } = string.Empty;
    }
}
