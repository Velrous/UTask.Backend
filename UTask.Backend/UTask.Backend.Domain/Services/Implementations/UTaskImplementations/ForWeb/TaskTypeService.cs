using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Domain.Entities.TaskTypes;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с типами задач
    /// </summary>
    public class TaskTypeService : BaseService, ITaskTypeService
    {
        #region Репозитории

        private readonly IEntityWithIdRepository<TaskTypeDao, int> _taskTypeRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с типами задач
        /// </summary>
        public TaskTypeService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _taskTypeRepository = kernel.Get<IEntityWithIdRepository<TaskTypeDao, int>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса типов задач
        /// </summary>
        /// <returns>Интерфейс для запроса типов задач</returns>
        public IQueryable<TaskType> GetQueryable()
        {
            try
            {
                var taskTypeQueryable = _taskTypeRepository.GetQueryable();
                return _mapper.ProjectTo<TaskType>(taskTypeQueryable);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
