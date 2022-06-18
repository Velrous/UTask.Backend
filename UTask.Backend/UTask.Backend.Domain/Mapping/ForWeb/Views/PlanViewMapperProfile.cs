using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views;

namespace UTask.Backend.Domain.Mapping.ForWeb.Views
{
    public class PlanViewMapperProfile : AutoMapper.Profile
    {
        public PlanViewMapperProfile()
        {
            CreateMap<PlanViewDao, PlanView>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.PlanPriorityId, a => a.MapFrom(p => p.PlanPriorityId))
                .ForMember(p => p.PlanPriorityName, a => a.MapFrom(p => p.PlanPriorityName))
                .ForMember(p => p.PlanPriorityValue, a => a.MapFrom(p => p.PlanPriorityValue))
                .ForMember(p => p.Date, a => a.MapFrom(p => p.Date))
                .ForMember(p => p.Position, a => a.MapFrom(p => p.Position))
                .ForMember(p => p.TaskId, a => a.MapFrom(p => p.TaskId))
                .ForMember(p => p.TaskName, a => a.MapFrom(p => p.TaskName))
                .ForMember(p => p.TaskTypeId, a => a.MapFrom(p => p.TaskTypeId))
                .ForMember(p => p.TaskTypeName, a => a.MapFrom(p => p.TaskTypeName))
                .ForMember(p => p.CategoryId, a => a.MapFrom(p => p.CategoryId))
                .ForMember(p => p.CategoryName, a => a.MapFrom(p => p.CategoryName))
                .ForMember(p => p.IsComplete, a => a.MapFrom(p => p.IsComplete))
                ;
        }
    }
}
