using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с целями
    /// </summary>
    public class GoalService : BaseService, IGoalService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<GoalDao, long> _goalRepository;
        private readonly IEntityRepository<GoalTaskRelationDao> _goalTaskRelationRepository;
        private readonly IEntityWithIdRepository<TaskDao, long> _taskRepository;

        #endregion

        /// <summary>
        /// Сервис работы с целями
        /// </summary>
        public GoalService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _goalRepository = kernel.Get<IEntityWithIdRepository<GoalDao, long>>(new ConstructorArgument("context", _utaskContext));
            _goalTaskRelationRepository = kernel.Get<IEntityRepository<GoalTaskRelationDao>>(new ConstructorArgument("context", _utaskContext));
            _taskRepository = kernel.Get<IEntityWithIdRepository<TaskDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion
        }

        /// <summary>
        /// Создаёт новую цель
        /// </summary>
        /// <param name="goal">Новая цель</param>
        public void Create(Goal goal)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(goal.Name))
                {
                    if (ServerContext.UserId > 0)
                    {
                        var goalDao = new GoalDao
                        {
                            UserId = ServerContext.UserId,
                            Name = goal.Name,
                            Description = goal.Description,
                            Created = DateTime.Now
                        };
                        _goalRepository.Create(goalDao);
                        _utaskContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя меньше или равен 0");
                    }
                }
                else
                {
                    throw new Exception($"Передано пустое наименование");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Обновляет данные цели
        /// </summary>
        /// <param name="goal">Измененная цель</param>
        public void Update(Goal goal)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(goal.Name))
                {
                    var goalDao = _goalRepository.GetById(goal.Id);
                    if (goalDao != null)
                    {
                        if (ServerContext.UserId == goalDao.UserId)
                        {
                            goalDao.Name = goal.Name;
                            goalDao.Description = goal.Description;
                            _goalRepository.Update(goalDao);
                            _utaskContext.SaveChanges();
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
                    throw new Exception($"Передано пустое наименование");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Добавляет задачу к цели
        /// </summary>
        /// <param name="goalTaskRelation">Отношение цели и задачи</param>
        public void AddTaskToGoal(GoalTaskRelation goalTaskRelation)
        {
            try
            {
                var goalDao = _goalRepository.GetById(goalTaskRelation.GoalId);
                if (goalDao != null)
                {
                    if (goalDao.UserId == ServerContext.UserId)
                    {
                        var taskDao = _taskRepository.GetById(goalTaskRelation.TaskId);
                        if (taskDao != null)
                        {
                            if (taskDao.UserId == ServerContext.UserId)
                            {
                                var goalTaskRelationDao = new GoalTaskRelationDao
                                {
                                    GoalId = goalTaskRelation.GoalId,
                                    TaskId = goalTaskRelation.TaskId
                                };
                                _goalTaskRelationRepository.Create(goalTaskRelationDao);
                                _utaskContext.SaveChanges();
                            }
                            else
                            {
                                throw new Exception($"Идентификатор пользователя не совпадает с идентификатором у задачи");
                            }
                        }
                        else
                        {
                            throw new Exception($"Задача не найдена");
                        }
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя не совпадает с идентификатором у цели");
                    }
                }
                else
                {
                    throw new Exception($"Цель не найдена");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет цель по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        public void Delete(long id)
        {
            try
            {
                var goalDao = _goalRepository.GetById(id);
                if (goalDao != null)
                {
                    if (ServerContext.UserId == goalDao.UserId)
                    {
                        var goalTaskRelationDaos = _goalTaskRelationRepository.GetQueryable()
                            .Where(x => x.GoalId == goalDao.Id)
                            .ToList();
                        if (goalTaskRelationDaos.Any())
                        {
                            _goalTaskRelationRepository.DeleteRange(goalTaskRelationDaos);
                        }
                        _goalRepository.Delete(goalDao);
                        _utaskContext.SaveChanges();
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
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет задачу у цели
        /// </summary>
        /// <param name="goalTaskRelation">Отношение цели и задачи</param>
        public void DeleteTaskFromGoal(GoalTaskRelation goalTaskRelation)
        {
            try
            {
                var goalTaskRelationDao = _goalTaskRelationRepository.GetQueryable()
                    .Where(x => x.GoalId == goalTaskRelation.GoalId)
                    .FirstOrDefault(x => x.TaskId == goalTaskRelation.TaskId);
                if (goalTaskRelationDao != null)
                {
                    var goalDao = _goalRepository.GetById(goalTaskRelationDao.GoalId);
                    if (goalDao != null)
                    {
                        if (goalDao.UserId == ServerContext.UserId)
                        {
                            _goalTaskRelationRepository.Delete(goalTaskRelationDao);
                            _utaskContext.SaveChanges();
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
                    throw new Exception($"Связь цели и задачи не найдена");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
