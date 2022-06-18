using UTask.Backend.Domain.Entities.Tasks;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views;

namespace UTask.Backend.Domain.Mapping.ForWeb.Views
{
    public class TaskViewMapperProfile : AutoMapper.Profile
    {
        public TaskViewMapperProfile()
        {
            CreateMap<TaskViewDao, TaskView>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.TaskTypeId, a => a.MapFrom(p => p.TaskTypeId))
                .ForMember(p => p.TaskTypeName, a => a.MapFrom(p => p.TaskTypeName))
                .ForMember(p => p.CategoryId, a => a.MapFrom(p => p.CategoryId))
                .ForMember(p => p.CategoryName, a => a.MapFrom(p => p.CategoryName))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Created, a => a.MapFrom(p => p.Created))
                .ForMember(p => p.IsComplete, a => a.MapFrom(p => p.IsComplete))
                ;
        }
    }
}
