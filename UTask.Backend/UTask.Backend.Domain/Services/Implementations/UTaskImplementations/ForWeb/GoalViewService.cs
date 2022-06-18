using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Domain.Entities.Tasks;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с представлением целей
    /// </summary>
    public class GoalViewService : BaseService, IGoalViewService
    {
        #region Репозитории

        private readonly IEntityRepository<GoalTaskRelationDao> _goalTaskRelationRepository;
        private readonly IEntityWithIdRepository<GoalViewDao, long> _goalViewRepository;
        private readonly IEntityWithIdRepository<TaskViewDao, long> _taskViewRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с представлением целей
        /// </summary>
        public GoalViewService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _goalTaskRelationRepository = kernel.Get<IEntityRepository<GoalTaskRelationDao>>(new ConstructorArgument("context", utaskContext));
            _goalViewRepository = kernel.Get<IEntityWithIdRepository<GoalViewDao, long>>(new ConstructorArgument("context", utaskContext));
            _taskViewRepository = kernel.Get<IEntityWithIdRepository<TaskViewDao, long>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает представление цели по её идентификатору
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        /// <returns>Представление цели</returns>
        public GoalView GetById(long id)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var goalViewDao = _goalViewRepository.GetById(id);
                    if (goalViewDao != null)
                    {
                        if (goalViewDao.UserId == ServerContext.UserId)
                        {
                            return _mapper.Map<GoalView>(goalViewDao);
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"Цель не найдена");
                    }
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
        /// Возвращает интерфейс для запроса к представлению целей
        /// </summary>
        /// <returns>Интерфейс для запроса к представлению целей</returns>
        public IQueryable<GoalView> GetQueryable()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var goalViewQueryable = _goalViewRepository.GetQueryable()
                        .Where(x => x.UserId == ServerContext.UserId);
                    return _mapper.ProjectTo<GoalView>(goalViewQueryable);
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
        /// Возвращает интерфейс для запроса к представлению задач цели
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        /// <returns>Интерфейс для запроса к представлению задач цели</returns>
        public IQueryable<TaskView> GetTasksForGoal(long id)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var goalViewDao = _goalViewRepository.GetById(id);
                    if (goalViewDao != null)
                    {
                        if (goalViewDao.UserId == ServerContext.UserId)
                        {
                            var goalTaskRelations = _goalTaskRelationRepository.GetQueryable()
                                .Where(x => x.GoalId == goalViewDao.Id)
                                .ToList();
                            var taskView = _taskViewRepository.GetQueryable()
                                .Where(x => goalTaskRelations.Select(y => y.TaskId).Contains(x.Id));
                            return _mapper.ProjectTo<TaskView>(taskView);
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"Цель не найдена");
                    }
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
