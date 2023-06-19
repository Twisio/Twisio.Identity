using Microsoft.IdentityModel.Tokens;
using System.Text;
using TWISIO.Identity.API.DTOs;

namespace TWISIO.Identity.API.Common.Options
{
    /// <summary>
    /// Класс с настройками JWT-токена
    /// </summary>
    public class JwtOptions
    {
        /// <summary>
        /// Издатель токена
        /// </summary>
        public string ISSUER { get; private set; } = string.Empty;
        /// <summary>
        /// Аудитория токена
        /// </summary>
        public string AUDIENCE { get; private set; } = string.Empty;
        /// <summary>
        /// Ключ шифрования
        /// </summary>
        public string KEY { private get; set; } = string.Empty;

        /// <summary>
        /// Время жизни токена доступа
        /// </summary>
        public DateTime EXPIRES { get; private set; } = DateTime.MinValue;
        /// <summary>
        /// Время жизни токена обновления
        /// </summary>
        public DateTime RefreshTokenExpires { get; private set; } = DateTime.MinValue;
        /// <summary>
        /// Сгенирирует ключ безопасности
        /// </summary>
        /// <returns><see cref="SymmetricSecurityKey"/></returns>
        public SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));

        /// <summary>
        /// Инициализация настроек JWT-токена
        /// </summary>
        /// <param name="optionsDto">DTO с данными для <see cref="JwtOptions"/></param>
        public JwtOptions(JwtOptionsDto optionsDto)
        {
            RefreshTokenExpires = DateTime.UtcNow.Add(optionsDto.RefreshTokenExpires);
            EXPIRES = DateTime.UtcNow.Add(optionsDto.EXPIRES);
            ISSUER = optionsDto.ISSUER;
            AUDIENCE = optionsDto.AUDIENCE;
            KEY = optionsDto.KEY;
        }

        /// <summary>
        /// Инициализация настроек JWT-токена для генерации секретного ключа
        /// </summary>
        /// <param name="key">Ключ шифрования</param>
        public JwtOptions(string key)
        {
            KEY = key;
        }
    }
}
