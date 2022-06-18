using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Domain.Entities.PlanPriorities;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с приоритетами планов
    /// </summary>
    public class PlanPriorityService : BaseService, IPlanPriorityService
    {
        #region Репозитории

        private readonly IEntityWithIdRepository<PlanPriorityDao, int> _planPriorityRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с приоритетами планов
        /// </summary>
        public PlanPriorityService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _planPriorityRepository = kernel.Get<IEntityWithIdRepository<PlanPriorityDao, int>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса к приоритетам планов
        /// </summary>
        /// <returns>Интерфейс для запроса к приоритетам планов</returns>
        public IQueryable<PlanPriority> GetQueryable()
        {
            try
            {
                var planPriorityQueryable = _planPriorityRepository.GetQueryable();
                return _mapper.ProjectTo<PlanPriority>(planPriorityQueryable);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
