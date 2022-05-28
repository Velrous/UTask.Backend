using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Notes;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с заметками
    /// </summary>
    public class NoteService : BaseService, INoteService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<NoteDao, long> _noteRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с заметками
        /// </summary>
        public NoteService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _noteRepository = kernel.Get<IEntityWithIdRepository<NoteDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="note">Новая заметка</param>
        /// <returns>Созданная заметка</returns>
        public Note Create(Note note)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(note.Description))
                {
                    if (ServerContext.UserId > 0)
                    {
                        var noteDao = new NoteDao
                        {
                            UserId = ServerContext.UserId,
                            Description = note.Description,
                            Created = DateTime.Now
                        };
                        noteDao = _noteRepository.Create(noteDao);
                        _utaskContext.SaveChanges();
                        return _mapper.Map<Note>(noteDao);
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя меньше или равен 0");
                    }
                }
                else
                {
                    throw new Exception($"Передано пустое описание");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Возвращает интерфейс для запроса заметок
        /// </summary>
        /// <returns>Интерфейс для запроса заметок</returns>
        public IQueryable<Note> GetQueryable()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var noteQueryable = _noteRepository.GetQueryable()
                        .Where(x=>x.UserId == ServerContext.UserId)
                        .OrderByDescending(x => x.Created);
                    return _mapper.ProjectTo<Note>(noteQueryable);
                }
                else
                {
                    throw new Exception($"Идентификатор пользователя меньше или равен 0");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Обновляет данные заметки
        /// </summary>
        /// <param name="note">Измененная заметка</param>
        public void Update(Note note)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(note.Description))
                {
                    var noteDao = _noteRepository.GetById(note.Id);
                    if (noteDao != null)
                    {
                        if (ServerContext.UserId == noteDao.UserId)
                        {
                            noteDao.Description = note.Description;
                            _noteRepository.Update(noteDao);
                            _utaskContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"Заметка не найдена");
                    }
                }
                else
                {
                    throw new Exception($"Передано пустое описание");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет переданную заметку
        /// </summary>
        /// <param name="id">Идентификатор заметки</param>
        public void Delete(long id)
        {
            try
            {
                var noteDao = _noteRepository.GetById(id);
                if (noteDao != null)
                {
                    if (ServerContext.UserId == noteDao.UserId)
                    {
                        _noteRepository.Delete(noteDao);
                        _utaskContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя не совпадает");
                    }
                }
                else
                {
                    throw new Exception($"Заметка не найдена");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
