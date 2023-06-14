using Microsoft.AspNetCore.Http;

namespace TWISIO.Identity.Application.Interfaces
{
    /// <summary>
    /// Интерфес загрузчика файлов
    /// </summary>
    public interface IFileUploader
    {
        /// <summary>
        /// Загружаемый файл
        /// </summary>
        IFormFile File { get; set; }
        /// <summary>
        /// Абсолютный путь к файлу
        /// </summary>
        string AbsolutePath { get; set; }
        /// <summary>
        /// Корневой путь проекта
        /// </summary>
        string WebRootPath { get; set; }

        /// <summary>
        /// Загрузить файл асинхронно
        /// </summary>
        /// <returns>Название загруженного файла</returns>
        Task<string> UploadFileAsync();
    }
}
