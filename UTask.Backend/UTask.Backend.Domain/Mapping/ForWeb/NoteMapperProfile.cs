using UTask.Backend.Domain.Entities.Notes;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Domain.Mapping.ForWeb
{
    public class NoteMapperProfile : AutoMapper.Profile
    {
        public NoteMapperProfile()
        {
            CreateMap<NoteDao, Note>()
                .ForMember(p => p.Id, a => a.MapFrom(p => p.Id))
                .ForMember(p => p.Description, a => a.MapFrom(p => p.Description))
                .ForMember(p => p.Created, a => a.MapFrom(p => p.Created))
                ;
        }
    }
}
