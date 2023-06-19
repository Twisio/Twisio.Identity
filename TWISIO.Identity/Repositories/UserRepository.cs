using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using TWISIO.Identity.API.Common.Exceptions;
using TWISIO.Identity.API.Common.Services;
using TWISIO.Identity.API.DTOs.UserDTOs;
using TWISIO.Identity.API.DTOs.UserDTOs.ResponseDTOs;
using TWISIO.Identity.API.Interfaces;
using TWISIO.Identity.API.Interfaces.Repositories;

namespace TWISIO.Identity.API.Repositories
{
    /// <summary>
    /// Репозиторий пользователя
    /// </summary>
    /// <inheritdoc/>
    public class UserRepository : IUserRepository
    {
        private readonly IDBContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IEmailSender _emailSender;
        private readonly IFileUploader _uploader;

        public UserRepository(IDBContext dbContext, IMapper mapper, IEmailSender emailSender, IFileUploader uploader)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _emailSender = emailSender;
            _uploader = uploader;
        }

        public async Task<GetUsersResponseDto> GetAll()
        {
            var users = await _dbContext.Users
                .AsNoTracking()
                .ProjectTo<UserShortResponseDto>(_mapper.ConfigurationProvider)
                .ToListAsync(CancellationToken.None);

            var result = new GetUsersResponseDto
            {
                Users = users
            };

            return result;
        }

        public async Task<UserResponseDto> GetById(GetUserByIdDto dto)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, CancellationToken.None);

            if (user is null)
                throw new NotFoundException(user);

            var result = _mapper.Map<UserResponseDto>(user);

            return result;
        }

        public async Task UpdateDetails(UpdateUserDetailsDto dto)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, CancellationToken.None);

            if (user is null)
                throw new NotFoundException(user);

            user = _mapper.Map(dto, user);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public async Task<UserShortResponseDto> GetShortById(GetUserByIdDto dto)
        {
            var user = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, CancellationToken.None);

            if (user is null)
                throw new NotFoundException(user);

            var result = _mapper.Map<UserShortResponseDto>(user);

            return result;
        }

        public async Task<UploadImageResponseDto> UploadImage(UploadImageDto dto,
            string webRootPath,
            string hostUrl)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, CancellationToken.None);

            if (user is null)
                throw new NotFoundException(user);

            var result = new UploadImageResponseDto();

            _uploader.File = dto.File;
            _uploader.WebRootPath = webRootPath is null
                ? throw new ArgumentException("Корневой путь проекта " +
                "не может быть пустым")
                : webRootPath;
            _uploader.AbsolutePath = user.Id.ToString();

            var imageName = await _uploader.UploadFileAsync();

            user.ImageUrl = imageName;

            var imagePath = UrlParser.Parse(imageName, user.Id.ToString(), hostUrl);
            result.Url = imagePath!;

            await _dbContext.SaveChangesAsync(CancellationToken.None);

            return result;
        }

        public async Task DeleteImage(DeleteImageDto dto,
            string webRootPath)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == dto.UserId, CancellationToken.None);

            if (user is null)
                throw new NotFoundException(user);

            File.Delete(Path.Combine(
                webRootPath is null
                ? throw new ArgumentException("Корневой путь проекта не может быть пустым")
                : webRootPath,
                user.Id.ToString(), user.ImageUrl!));

            user.ImageUrl = null;

            await _dbContext.SaveChangesAsync(CancellationToken.None);
        }
    }
}
