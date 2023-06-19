using AutoMapper;
using TWISIO.Identity.API.DTOs.AuthDTOs;
using TWISIO.Identity.API.DTOs.UserDTOs;
using TWISIO.Identity.API.DTOs.UserDTOs.ResponseDTOs;
using TWISIO.Identity.API.Entities;

namespace TWISIO.Identity.API.Common.Mappings.MappingProfiles
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
