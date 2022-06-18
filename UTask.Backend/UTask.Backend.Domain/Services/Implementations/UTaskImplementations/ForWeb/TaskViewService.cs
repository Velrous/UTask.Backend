using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
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
    /// Сервис работы с представлением задач
    /// </summary>
    public class TaskViewService : BaseService, ITaskViewService
    {
        #region Репозитории

        private readonly IEntityWithIdRepository<GoalViewDao, long> _goalViewRepository;
        private readonly IEntityRepository<GoalTaskRelationDao> _goalTaskRelationRepository;
        private readonly IEntityWithIdRepository<TaskViewDao, long> _taskViewRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с представлением задач
        /// </summary>
        public TaskViewService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _goalViewRepository = kernel.Get<IEntityWithIdRepository<GoalViewDao, long>>(new ConstructorArgument("context", utaskContext));
            _goalTaskRelationRepository = kernel.Get<IEntityRepository<GoalTaskRelationDao>>(new ConstructorArgument("context", utaskContext));
            _taskViewRepository = kernel.Get<IEntityWithIdRepository<TaskViewDao, long>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса представления задач
        /// </summary>
        /// <returns>Интерфейс для запроса представления задач</returns>
        public IQueryable<TaskView> GetQueryable()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var taskViewQueryable = _taskViewRepository.GetQueryable()
                        .Where(x => x.UserId == ServerContext.UserId);
                    return _mapper.ProjectTo<TaskView>(taskViewQueryable);
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
        /// Возвращает интерфейс для запроса к представлению задач, которые могут быть добавлены к цели
        /// </summary>
        /// <param name="goalId">Идентификатор цели</param>
        /// <returns>Интерфейс для запроса к представлению задач, которые могут быть добавлены к цели</returns>
        public IQueryable<TaskView> GetTasksToAddToGoal(long goalId)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var goalViewDao = _goalViewRepository.GetById(goalId);
                    if (goalViewDao != null)
                    {
                        if (goalViewDao.UserId == ServerContext.UserId)
                        {
                            var goalTaskRelationDaos = _goalTaskRelationRepository.GetQueryable()
                                .Where(x => x.GoalId == goalViewDao.Id)
                                .ToList();
                            var taskViewQueryable = _taskViewRepository.GetQueryable()
                                .Where(x => x.UserId == ServerContext.UserId)
                                .Where(x => !goalTaskRelationDaos.Select(y => y.TaskId).Contains(x.Id));
                            return _mapper.ProjectTo<TaskView>(taskViewQueryable);
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
