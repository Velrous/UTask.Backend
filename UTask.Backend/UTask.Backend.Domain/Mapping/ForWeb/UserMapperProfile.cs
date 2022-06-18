using UTask.Backend.Domain.Entities.Users;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class UserMapperProfile : AutoMapper.Profile
    {
        public UserMapperProfile()
        {
            CreateMap<UserDao, UserForWeb>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.DisplayName, a => a.MapFrom(p => p.DisplayName))
                .ForMember(p => p.Email, a => a.MapFrom(p => p.Email))
                .ForMember(p => p.OldPassword, a => a.MapFrom(p => string.Empty))
                .ForMember(p => p.NewPassword, a => a.MapFrom(p => string.Empty))
                ;
        }
    }
}
