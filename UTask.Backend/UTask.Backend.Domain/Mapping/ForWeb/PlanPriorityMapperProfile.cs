using UTask.Backend.Domain.Entities.PlanPriorities;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class PlanPriorityMapperProfile : AutoMapper.Profile
    {
        public PlanPriorityMapperProfile()
        {
            CreateMap<PlanPriorityDao, PlanPriority>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Value, a => a.MapFrom(p => p.Value))
                ;
        }
    }
}
