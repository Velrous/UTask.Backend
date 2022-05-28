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
                .ForMember(p => p.Login, a => a.MapFrom(p => p.Login))
                .ForMember(p => p.DisplayName, a => a.MapFrom(p => p.DisplayName))
                .ForMember(p => p.Email, a => a.MapFrom(p => p.Email))
                .ForMember(p => p.DateOfBirth, a => a.MapFrom(p => p.DateOfBirth))
                ;
        }
    }
}
