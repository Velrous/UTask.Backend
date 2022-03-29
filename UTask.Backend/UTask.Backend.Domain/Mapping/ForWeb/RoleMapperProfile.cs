using UTask.Backend.Domain.Entities.Roles;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class RoleMapperProfile : AutoMapper.Profile
    {
        public RoleMapperProfile()
        {
            CreateMap<RoleDao, Role>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Created, a => a.MapFrom(p => p.Created))
                .ForMember(p => p.IsActive, a => a.MapFrom(p => p.IsActive))
                ;
        }
    }
}
