using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TWISIO.Identity.Application.Common.Exceptions;
using TWISIO.Identity.Application.DTOs.AuthDTOs;
using TWISIO.Identity.Application.DTOs.AuthDTOs.ResponseDTOs;
using TWISIO.Identity.Application.Interfaces;
using TWISIO.Identity.Application.Interfaces.Repositories;
using TWISIO.Identity.Domain;
using TWISIO.Identity.Domain.Enums;

namespace TWISIO.Identity.Application.Repositories
{
    /// <inheritdoc/>
    public class AuthRepository : IAuthRepository
    {
        private readonly IEmailSender _emailSender;
        private readonly IDBContext _dbContext;
        private readonly ILogRepository _logRepository;
        private readonly ITokenManager _tokenManager;
        private readonly ILogger<AuthRepository> _logger;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        public readonly IMapper _mapper;

        public AuthRepository(IDBContext dbContext, ILogger<AuthRepository> logger,
            SignInManager<User> signInManager, ITokenManager tokenmanager,
            UserManager<User> userManager, IEmailSender emailSender, ILogRepository logRepository, IMapper mapper)
        {
            _dbContext = dbContext;
            _logger = logger;
            _signInManager = signInManager;
            _tokenManager = tokenmanager;
            _userManager = userManager;
            _emailSender = emailSender;
            _logRepository = logRepository;
            _mapper = mapper;
        }

        public async Task<SignUpResponseDto> AdminSignUp(SignUpDto dto)
        {
            var user = new User
            {
                UserName = dto.Email,
                Email = dto.Email,
                Role = Roles.ADMIN
            };

            var getUser = await _userManager.FindByEmailAsync(dto.Email);

            if (getUser is not null)
                throw new BadRequestException("Пользователь с такой почтой уже существует");

            var result = await _userManager.CreateAsync(user, dto.Password);
            var userId = await _userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                var message = "Администратор был зарегистрирован";
                _logger.LogInformation($"{userId} - {message}");
                await _logRepository.AddAsync(user, message, LogType.INFO);

                var code = await GenerateCode(user);

                await _emailSender.SendEmailAsync("Подтвердите регистрацию администратора",
                    $"Был подан запрос на регистрацию нового администратора " +
                    $"с почтой {dto.Email}. Код для подтверждения аккаунта: {code}. " +
                    $"Сообщите его новому администратору. \n" +
                    "Если вы не знаете этого человека, то проигнорируйте это сообщение");

                return new SignUpResponseDto
                {
                    userId = Guid.Parse(userId)
                };
            }
            foreach (var error in result.Errors)
            {
                throw new BadRequestException(error.Description);
            }

            throw new WentWrongException();
        }

        public async Task<SignInResponseDto> SignIn(SignInDto dto, bool isOAuth)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }

            var result = new SignInResultDto();

            if (!isOAuth)
            {
                if (!user.EmailConfirmed)
                {
                    var message = "Почта не подтверждена";

                    _logger.LogWarning($"{user.Id} - {message}");
                    await _logRepository.AddAsync(user, message, LogType.ERROR);

                    throw new ForbiddenException(message);
                }

                var passwordIsValid = await _userManager.CheckPasswordAsync(user, dto.Password);

                if (!passwordIsValid)
                    throw new BadRequestException("Неверный пароль");

                var signInResult = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);
                result.Succeeded = signInResult.Succeeded;
                result.IsLockedOut = signInResult.IsLockedOut;
            }
            else result = new SignInResultDto
            {
                Succeeded = true,
                IsLockedOut = user.LockoutEnabled
            };

            if (result.Succeeded)
            {
                var message = "Пользователь авторизован";

                _logger.LogInformation($"{user.Id} - {message}");
                await _logRepository.AddAsync(user, message, LogType.INFO);

                JwtSecurityToken accessToken = await _tokenManager.CreateAccessTokenAsync(user);

                JwtSecurityToken? refreshToken = null;

                if (dto.RememberMe)
                    refreshToken = await _tokenManager.CreateRefreshTokenAsync(user.Id);

                return new SignInResponseDto
                {
                    userId = user.Id,
                    access_token = new JwtSecurityTokenHandler().WriteToken(accessToken),
                    expires = DateToMilleseconds(accessToken.ValidTo),
                    refresh_token = refreshToken is null
                        ? null
                        : new JwtSecurityTokenHandler().WriteToken(refreshToken),
                    refresh_token_expires = refreshToken is null
                        ? null
                        : DateToMilleseconds(refreshToken!.ValidTo)
                };
            }
            if (result.IsLockedOut)
            {
                var message = "Аккаунт заблокирован";
                _logger.LogWarning($"{user.Id} - {message}");
                await _logRepository.AddAsync(user, message, LogType.ERROR);

                user.AccessFailedCount = 0;
                await _userManager.UpdateAsync(user);

                throw new ForbiddenException(message);
            }
            else
            {
                var message = "Неудачная попытка входа";
                _logger.LogWarning($"{user.Id} - {message}");
                await _logRepository.AddAsync(user, message, LogType.ERROR);

                throw new BadRequestException(message);
            }
        }

        public async Task<SignInResponseDto> SignInWithOAuth(SignInWithOAuthDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }

            switch (dto.Service)
            {
                case OAuthService.None: throw new BadRequestException("Не указан сервис oAuth");
                case OAuthService.VK:
                    if (user.VKontakteId != dto.Id)
                        throw new ForbiddenException("Пользователь существует, но VK не привязан к профилю");
                    break;
                case OAuthService.YANDEX:
                    if (user.YandexId != dto.Id)
                        throw new ForbiddenException("Пользователь существует, но Yandex не привязан к профилю");
                    break;
                case OAuthService.GOOGLE:
                    if (user.GoogleId != dto.Id)
                        throw new ForbiddenException("Пользователь существует, но Google не привязан к профилю");
                    break;
                case OAuthService.TELEGRAM:
                    if (user.TelegramId != dto.Id)
                        throw new ForbiddenException("Пользователь существует, но Telegram не привязан к профилю");
                    break;
            }

            var result = await SignIn(new SignInDto
            {
                Email = dto.Email,
                Password = string.Empty,
                RememberMe = true
            }, true);

            return result;
        }

        public async Task<SignUpResponseDto> SignUp(SignUpDto dto, bool isOAuth, 
            string? oAuthId, OAuthService? service)
        {
            var existUser = await _userManager.FindByEmailAsync(dto.Email);

            if (existUser is not null)
                throw new BadRequestException("Пользователь с такой почтой уже существует");

            var user = _mapper.Map<User>(dto);

            if (isOAuth)
            {
                switch (service)
                {
                    case OAuthService.None: throw new BadRequestException("Не указан сервис oAuth");
                    case OAuthService.VK:
                        user.VKontakteId = oAuthId;
                        break;
                    case OAuthService.YANDEX:
                        user.YandexId = oAuthId;
                        break;
                    case OAuthService.GOOGLE:
                        user.GoogleId = oAuthId;
                        break;
                    case OAuthService.TELEGRAM:
                        user.TelegramId = oAuthId;
                        break;
                }
            }

            var result = await _userManager.CreateAsync(user, dto.Password);
            var userId = await _userManager.GetUserIdAsync(user);

            if (result.Succeeded)
            {
                var message = "Пользователь был зарегистрирован";
                _logger.LogInformation($"{userId} - {message}");
                await _logRepository.AddAsync(user, message, LogType.INFO);

                if (!isOAuth)
                {
                    var code = await GenerateCode(user);

                    await _emailSender.SendEmailAsync(dto.Email, "Подтвердите свою почту",
                        $"Код для подтверждения аккаунта: {code}. \n" +
                        "Если вы не запрашивали код, то проигнорируйте это сообщение");
                }

                return new SignUpResponseDto
                {
                    userId = Guid.Parse(userId)
                };
            }
            foreach (var error in result.Errors)
            {
                throw new BadRequestException(error.Description);
            }

            throw new WentWrongException();
        }

        public async Task<SignUpResponseDto> SignUpWithOAuth(SignUpWithOAuthDto dto)
        {
            var result = await SignUp(new SignUpDto
            {
                Email = dto.Email,
                Password = string.Empty,
                Role = dto.Role,
                UserName = dto.Username
            }, true, dto.Id, dto.Service);

            return result;
        }

        public async Task ForgotPassword(ForgotPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }

            var code = await GenerateCode(user);

            await _emailSender.SendEmailAsync(
                dto.Email,
                "Подтверждение сброса пароля",
                $"Код для подтверждения сброса пароля: {code}. \n" +
                "Если вы не запрашивали код, то проигнорируйте это сообщение");

            var message = "Запрошен код для сброса пароля";

            _logger.LogInformation($"{user.Id} - {message}");
            await _logRepository.AddAsync(user, message, LogType.SECURITY);
        }

        public async Task ResetPassword(ResetPasswordDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new NotFoundException("Пользователь не найден");
            }

            if (user.ConfirmationCode != dto.Code)
                throw new BadRequestException("Неправильный код подтверждения");

            user.ConfirmationCode = string.Empty;
            user.PasswordHash = string.Empty;
            await _userManager.RemovePasswordAsync(user);

            var result = await _userManager.AddPasswordAsync(user, dto.Password);

            if (result.Succeeded)
            {
                var message = "Пароль был сброшен";

                _logger.LogInformation($"{user.Id} - {message}");
                await _logRepository.AddAsync(user, message, LogType.SECURITY);

                await _emailSender.SendEmailAsync(
                    dto.Email,
                    "Пароль был успешно изменён",
                    "На вашем аккаунте был изменён пароль. " +
                    "Если это сделали не вы, то срочно поменяйте его");

                return;
            }
            foreach (var error in result.Errors)
            {
                throw new BadRequestException(error.Description);
            }

            throw new WentWrongException();
        }

        public async Task ConfirmEmail(ConfirmEmailDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }

            if (user.ConfirmationCode != dto.Code)
                throw new BadRequestException("Неправильный код подтверждения");

            user.ConfirmationCode = string.Empty;
            user.EmailConfirmed = true;

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            var message = "Почта была подтверждена";

            _logger.LogInformation($"{user.Id} - {message}");
            await _logRepository.AddAsync(user, message, LogType.INFO);

            await _emailSender.SendEmailAsync(
                dto.Email,
                "Почта была успешно подтверждена",
                "Приветствуем вас на нашем сайте. Удачных покупок!");
        }

        public async Task ResendEmailConfirmation(ResendEmailConfirmationDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user is null)
            {
                throw new NotFoundException(user);
            }

            var code = user.ConfirmationCode;

            if (code is null)
                code = await GenerateCode(user);

            await _emailSender.SendEmailAsync(dto.Email, "Подтвердите свою почту",
                    $"Код для подтверждения аккаунта: {code}. \n" +
                    "Если вы не запрашивали код, то проигнорируйте это сообщение");
        }

        public async Task<RefreshTokenResponseDto> RefreshToken(string refresh)
        {
            var principal = await _tokenManager.GetPrincipalFromRefreshTokenAsync(refresh)!;

            if (principal is null)
                throw new BadRequestException("Недействительный токен");

            var userId = principal.Claims.First(u => u.Type == ClaimTypes.NameIdentifier).Value;

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
                throw new NotFoundException(user);

            var newAccessToken = await _tokenManager.CreateAccessTokenAsync(user);
            var newRefreshToken = await _tokenManager.CreateRefreshTokenAsync(Guid.Parse(userId));

            await _userManager.UpdateAsync(user);

            return new RefreshTokenResponseDto
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                expires = DateToMilleseconds(newAccessToken.ValidTo),
                refresh_token = new JwtSecurityTokenHandler().WriteToken(newRefreshToken),
                refresh_token_expires = DateToMilleseconds(newRefreshToken.ValidTo)
            };
        }

        private long DateToMilleseconds(DateTime date)
        {
            return (long)(date - new DateTime(1970, 1, 1)).TotalMilliseconds;
        }

        private async Task<string> GenerateCode(User user)
        {
            var random = new Random();
            var code = random.Next(100000, 999999).ToString();

            user.ConfirmationCode = code;
            await _userManager.UpdateAsync(user);

            return code;
        }

        private class SignInResultDto : SignInResult
        {
            public new bool Succeeded { get; set; }
            public new bool IsLockedOut { get; set; }
        };
    }
}
