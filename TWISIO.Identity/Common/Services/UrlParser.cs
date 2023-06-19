namespace TWISIO.Identity.API.Common.Services
{
    /// <summary>
    /// Парсер стандартного пути до файла к пути, пригодного для статического отображения
    /// </summary>
    public static class UrlParser
    {
        /// <summary>
        /// Преобразует путь до файла в ссылку для статического отображения
        /// </summary>
        /// <param name="url">Путь, который надо преобразовать в ссылку</param>
        /// <param name="baseUrl">Адрес текущего хоста</param>
        /// <param name="baseDir">Корневая директория. Может быть id объекта</param>
        /// <returns>Преобразованная ссылка</returns>
        public static string? Parse(string baseUrl, string baseDir, string url)
        {
            if (url is null || url == string.Empty)
                return null;

            if (baseDir is null)
                return null;

            var baseUri = new Uri(baseUrl);
            var uriResult = new Uri(baseUri, Path.Combine(baseDir, url));
            return uriResult.ToString();
        }

        /// <summary>
        /// Преобразует пути до файла в ссылки для статического отображения
        /// </summary>
        /// <param name="baseUrl">Адрес текущего хоста</param>
        /// <param name="baseDir">Корневая директория. Может быть id объекта</param>
        /// <param name="urls">Список ссылок для преобразования</param>
        /// <returns>Преобразованные ссылки</returns>
        public static string[]? Parse(string baseUrl, string baseDir, string[]? urls)
        {
            if (urls is null || urls.Length == 0)
                return null;

            if (baseDir is null)
                return null;

            for (int j = 0; j < urls.Length; j++)
            {
                var url = Parse(baseUrl, baseDir, urls[j]);
                urls[j] = url!;

                var baseUri = new Uri(baseUrl);
                urls[j] = new Uri(baseUri, url).ToString();
            }

            return urls;
        }


    }
}
