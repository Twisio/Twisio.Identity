using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Interfaces
{
    /// <summary>
    /// Интерфейс менеджера токена
    /// </summary>
    public interface ITokenManager
    {
        /// <summary>
        /// Создать токен доступа асинхронно
        /// </summary>
        /// <param name="user">Пользователь, для которого токен будет издан</param>
        /// <returns><see cref="JwtSecurityToken"/></returns>
        Task<JwtSecurityToken> CreateAccessTokenAsync(User user);
        /// <summary>
        /// Создать токен обновления асинхронно
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, которому нужен токен обновления</param>
        /// <returns><see cref="JwtSecurityToken"/></returns>
        Task<JwtSecurityToken> CreateRefreshTokenAsync(Guid userId);
        /// <summary>
        /// Получить значения из токена обновления
        /// </summary>
        /// <param name="token">Токен обновления</param>
        /// <returns></returns>
        Task<ClaimsPrincipal> GetPrincipalFromRefreshTokenAsync(string token);
    }
}
