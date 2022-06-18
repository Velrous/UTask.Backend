using UTask.Backend.Domain.Entities.TaskTypes;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class TaskTypeMapperProfile : AutoMapper.Profile
    {
        public TaskTypeMapperProfile()
        {
            CreateMap<TaskTypeDao, TaskType>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                ; 
        }
    }
}
