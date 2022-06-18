using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Filters;
using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с представлением планов
    /// </summary>
    public class PlanViewService : BaseService, IPlanViewService
    {
        #region Репозитории

        private readonly IEntityWithIdRepository<PlanViewDao, long> _planViewRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с представлением планов
        /// </summary>
        public PlanViewService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _planViewRepository = kernel.Get<IEntityWithIdRepository<PlanViewDao, long>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса к представлению планов по дате
        /// </summary>
        /// <param name="planFilter">Фильтр плана</param>
        /// <returns>Интерфейс для запроса к представлению планов по дате</returns>
        public IQueryable<PlanView> GetQueryableByDate(PlanFilter planFilter)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var planViewQueryable = _planViewRepository.GetQueryable()
                        .Where(x => x.UserId == ServerContext.UserId)
                        .Where(x => x.Date.Date == planFilter.Date.Date)
                        .OrderBy(x => x.Position);
                    return _mapper.ProjectTo<PlanView>(planViewQueryable);
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
    }
}
