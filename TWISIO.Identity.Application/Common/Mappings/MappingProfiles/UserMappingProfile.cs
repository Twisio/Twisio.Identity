using AutoMapper;
using TWISIO.Identity.Application.DTOs.AuthDTOs;
using TWISIO.Identity.Application.DTOs.UserDTOs;
using TWISIO.Identity.Application.DTOs.UserDTOs.ResponseDTOs;
using TWISIO.Identity.Domain;

namespace TWISIO.Identity.Application.Common.Mappings.MappingProfiles
{
    /// <summary>
    /// Профиль конфигурации маппинга пользователя
    /// </summary>
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserShortResponseDto>(MemberList.Source);
            CreateMap<User, UserResponseDto>(MemberList.Source);
            CreateMap<SignUpDto, User>(MemberList.Source)
                .ForMember(user => user.DateOfCreation, opt => opt.MapFrom(dto => DateTime.UtcNow));
            CreateMap<UpdateUserDetailsDto, User>(MemberList.Source)
                .ForMember(user => user.Id, opt => opt.MapFrom(dto => dto.UserId));
        }
    }
}
