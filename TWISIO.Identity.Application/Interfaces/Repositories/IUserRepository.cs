using TWISIO.Identity.Application.DTOs.UserDTOs;
using TWISIO.Identity.Application.DTOs.UserDTOs.ResponseDTOs;

namespace TWISIO.Identity.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория пользователя
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns><see cref="GetUsersResponseDto"/></returns>
        Task<GetUsersResponseDto> GetAll();
        /// <summary>
        /// Получить информацию о пользователе по идентификатору
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="UserResponseDto"/></returns>
        Task<UserResponseDto> GetById(GetUserByIdDto dto);
        /// <summary>
        /// Получить короткую информацию о пользователе по идентификатору
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="UserShortResponseDto"/></returns>
        Task<UserShortResponseDto> GetShortById(GetUserByIdDto dto);
        /// <summary>
        /// Изменить персональные данные пользователя
        /// </summary>
        /// <param name="dto">Входные данные</param>
        Task UpdateDetails(UpdateUserDetailsDto dto);
        /// <summary>
        /// Загрузить изображение профиля
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <param name="hostUrl">Адрес сервера</param>
        /// <param name="webRootPath">Корневой путь проекта</param>
        /// <returns><see cref="UploadImageResponseDto"/></returns>
        Task<UploadImageResponseDto> UploadImage(UploadImageDto dto,
            string webRootPath,
            string hostUrl);
        /// <summary>
        /// Удалить изображение профиля
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <param name="webRootPath">Корневой путь проекта</param>
        Task DeleteImage(DeleteImageDto dto,
            string webRootPath);
    }
}
