using UTask.Backend.Domain.Entities.Categories;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class CategoryMapperProfile : AutoMapper.Profile
    {
        public CategoryMapperProfile()
        {
            CreateMap<CategoryDao, Category>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Name, a => a.MapFrom(p => p.Name))
                .ForMember(p => p.Created, a => a.MapFrom(p => p.Created))
                ;
        }
    }
}
