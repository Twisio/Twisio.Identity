using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TWISIO.Identity.API.Attributes;
using TWISIO.Identity.API.Models;
using TWISIO.Identity.Application.DTOs.UserDTOs;
using TWISIO.Identity.Application.DTOs.UserDTOs.ResponseDTOs;
using TWISIO.Identity.Application.Interfaces.Repositories;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.API.Controllers
{
    [Route("api/user")]
    [Produces("application/json")]
    [ApiController]
    //[Authorize]
    public class UserController : Controller
    {
        private string UrlRaw => $"{Request.Scheme}://{Request.Host}";
        private readonly IWebHostEnvironment _environment;
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository, IWebHostEnvironment environment)
        {
            _userRepository = userRepository;
            _environment = environment;
        }

        /// <summary>
        /// Получить список всех пользователей
        /// </summary>
        /// <returns><see cref="GetUsersResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet()]
        [RoleValidate(Roles.ADMIN)]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(GetUsersResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userRepository.GetAll();

            return Ok(result);
        }

        /// <summary>
        /// Получить полную информацию о пользователе по идентификатору
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="UserResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet("details")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> GetById([FromQuery] GetUserByIdDto dto)
        {
            var result = await _userRepository.GetById(dto);

            return Ok(result);
        }

        /// <summary>
        /// Получить краткую информацию о пользователе по идентификатору
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="UserShortResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpGet("details_short")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UserResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> GetShortById([FromQuery] GetUserByIdDto dto)
        {
            var result = await _userRepository.GetShortById(dto);

            return Ok(result);
        }

        /// <summary>
        /// Загрузить изображения профиля
        /// </summary>
        /// <remarks>
        /// Поддерживаемые расширения: .jpg, .jpeg, .png
        /// </remarks>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="UploadImageResponseDto"/></returns>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="400">Отсутствует файл</response>
        /// <response code="400">Недопустимое расширение файла</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPost("image")]
        [DisableRequestSizeLimit]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: typeof(UploadImageResponseDto))]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> UploadImage([FromForm] UploadImageDto dto)
        {
            var result = await _userRepository.UploadImage(dto, _environment.WebRootPath, UrlRaw);

            return Ok(result);
        }

        /// <summary>
        /// Удалить изображение профмля
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpDelete("image")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> DeleteImage([FromQuery] DeleteImageDto dto)
        {
            await _userRepository.DeleteImage(dto, _environment.WebRootPath);

            return Ok();
        }

        /// <summary>
        /// Обновить персональные данные пользователя
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <response code="200">Запрос выполнен успешно</response>
        /// <response code="404">Пользователь не найден</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpPut("details")]
        [SwaggerResponse(statusCode: StatusCodes.Status200OK, type: null)]
        [SwaggerResponse(statusCode: StatusCodes.Status404NotFound, type: typeof(ErrorModel))]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: typeof(ErrorModel))]
        public async Task<IActionResult> UpdateDetails([FromBody] UpdateUserDetailsDto dto)
        {
            await _userRepository.UpdateDetails(dto);

            return Ok();
        }


    }
}
