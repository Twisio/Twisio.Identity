using TWISIO.Identity.Application.DTOs.AuthDTOs;
using TWISIO.Identity.Application.DTOs.AuthDTOs.ResponseDTOs;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.Interfaces.Repositories
{
    /// <summary>
    /// Интерфейс репозитория авторизации
    /// </summary>
    public interface IAuthRepository
    {
        /// <summary>
        /// Регистрация администратора
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="SignUpResponseDto"/>, а так же письмо на корпоративную 
        /// почту с кодом подтверждения регистрации</returns>
        Task<SignUpResponseDto> AdminSignUp(SignUpDto dto);

        /// <summary>
        /// Авторизация пользователя
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <param name="isOAuth">Авторизация через oAuth или нет</param>
        /// <returns><see cref="SignInResponseDto"/></returns>
        Task<SignInResponseDto> SignIn(SignInDto dto, bool isOAuth);

        /// <summary>
        /// Авторизация пользователя через oAuth
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="SignInResponseDto"/></returns>
        Task<SignInResponseDto> SignInWithOAuth(SignInWithOAuthDto dto);

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <param name="isOAuth">Регистрация через oAuth или нет</param>
        /// <param name="oAuthId">Идентификатор из стороннего сервиса</param>
        /// <param name="service">Сторонний сервис</param>
        /// <returns><see cref="SignUpResponseDto"/>, а так же письмо на почту
        /// с кодом подтверждения регистрации</returns>
        Task<SignUpResponseDto> SignUp(SignUpDto dto, bool isOAuth, string? oAuthId, OAuthService? service);

        /// <summary>
        /// Регистрация пользователя через oAuth
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns><see cref="SignUpResponseDto"/></returns>
        Task<SignUpResponseDto> SignUpWithOAuth(SignUpWithOAuthDto dto);

        /// <summary>
        /// Подтверждение смены пароля
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns>Письмо на почту с кодом подтверждения сброса пароля</returns>
        Task ForgotPassword(ForgotPasswordDto dto);

        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns></returns>
        Task ResetPassword(ResetPasswordDto dto);

        /// <summary>
        /// Подтверждение почты
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns></returns>
        Task ConfirmEmail(ConfirmEmailDto dto);

        /// <summary>
        /// Повторная отправка письма подтверждения почты
        /// </summary>
        /// <param name="dto">Входные данные</param>
        /// <returns></returns>
        Task ResendEmailConfirmation(ResendEmailConfirmationDto dto);

        /// <summary>
        /// Обновление токена обновления
        /// </summary>
        /// <param name="refresh">Старый токен обновления</param>
        /// <returns></returns>
        Task<RefreshTokenResponseDto> RefreshToken(string refresh);
    }
}
