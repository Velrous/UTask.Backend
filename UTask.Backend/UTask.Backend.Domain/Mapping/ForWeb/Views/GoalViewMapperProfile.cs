using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views;

namespace UTask.Backend.Domain.Mapping.ForWeb.Views
{
    public class GoalViewMapperProfile : AutoMapper.Profile
    {
        public GoalViewMapperProfile()
        {
            CreateMap<GoalViewDao, GoalView>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Description, a => a.MapFrom(p => p.Description))
                .ForMember(p => p.Created, a => a.MapFrom(p => p.Created))
                .ForMember(p => p.PercentageCompletion, a => a.MapFrom(p => p.PercentageCompletion))
                ;
        }
    }
}
