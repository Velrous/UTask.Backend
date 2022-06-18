using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;
using Task = UTask.Backend.Domain.Entities.Tasks.Task;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с задачами
    /// </summary>
    public class TaskService : BaseService, ITaskService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<CategoryDao, long> _categoryRepository;
        private readonly IEntityRepository<GoalTaskRelationDao> _goalTaskRelationRepository;
        private readonly IEntityWithIdRepository<PlanDao, long> _planRepository;
        private readonly IEntityWithIdRepository<TaskDao, long> _taskRepository;

        #endregion

        /// <summary>
        /// Сервис работы с задачами
        /// </summary>
        public TaskService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _categoryRepository = kernel.Get<IEntityWithIdRepository<CategoryDao, long>>(new ConstructorArgument("context", _utaskContext));
            _goalTaskRelationRepository = kernel.Get<IEntityRepository<GoalTaskRelationDao>>(new ConstructorArgument("context", _utaskContext));
            _planRepository = kernel.Get<IEntityWithIdRepository<PlanDao, long>>(new ConstructorArgument("context", _utaskContext));
            _taskRepository = kernel.Get<IEntityWithIdRepository<TaskDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion
        }

        /// <summary>
        /// Создаёт новую задачу
        /// </summary>
        /// <param name="task">Новая задача</param>
        public void Create(Task task)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(task.Name))
                {
                    if (ServerContext.UserId > 0)
                    {
                        if (task.CategoryId.HasValue)
                        {
                            var categoryDao = _categoryRepository.GetById(task.CategoryId.Value);
                            if (categoryDao == null || categoryDao.UserId != ServerContext.UserId)
                            {
                                throw new Exception($"Категория не найдена или категория не принадлежит пользователю");
                            }
                        }

                        var taskDao = new TaskDao
                        {
                            UserId = ServerContext.UserId,
                            TaskTypeId = task.TaskTypeId,
                            CategoryId = task.CategoryId,
                            Name = task.Name,
                            Created = DateTime.Now,
                            IsComplete = false
                        };
                        _taskRepository.Create(taskDao);
                        _utaskContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Некорректный идентификатор пользователя");
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
        /// Обновляет данные задачи
        /// </summary>
        /// <param name="task">Измененная задача</param>
        public void Update(Task task)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    if (!string.IsNullOrWhiteSpace(task.Name))
                    {
                        var taskDao = _taskRepository.GetById(task.Id);
                        if (taskDao != null)
                        {
                            if (ServerContext.UserId == taskDao.UserId)
                            {
                                if (task.CategoryId.HasValue)
                                {
                                    var categoryDao = _categoryRepository.GetById(task.CategoryId.Value);
                                    if (categoryDao == null || categoryDao.UserId != ServerContext.UserId)
                                    {
                                        throw new Exception($"Категория не найдена или категория не принадлежит пользователю");
                                    }
                                }

                                taskDao.TaskTypeId = task.TaskTypeId;
                                taskDao.CategoryId = task.CategoryId;
                                taskDao.Name = task.Name;
                                taskDao.IsComplete = task.IsComplete;
                                _taskRepository.Update(taskDao);
                                _utaskContext.SaveChanges();
                            }
                            else
                            {
                                throw new Exception($"Идентификатор пользователя не совпадает");
                            }
                        }
                        else
                        {
                            throw new Exception($"Задача не найдена");
                        }
                    }
                    else
                    {
                        throw new Exception($"Передано пустое наименование");
                    }
                }
                else
                {
                    throw new Exception($"Некорректный идентификатор пользователя");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет задачу по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        public void Delete(long id)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var taskDao = _taskRepository.GetById(id);
                    if (taskDao != null)
                    {
                        if (ServerContext.UserId == taskDao.UserId)
                        {
                            var goalTaskRelationDaos = _goalTaskRelationRepository.GetQueryable()
                                .Where(x => x.TaskId == taskDao.Id)
                                .ToList();
                            if (goalTaskRelationDaos.Any())
                            {
                                _goalTaskRelationRepository.DeleteRange(goalTaskRelationDaos);
                                _utaskContext.SaveChanges();
                            }
                            var planDaos = _planRepository.GetQueryable()
                                .Where(x => x.TaskId == taskDao.Id)
                                .ToList();
                            if (planDaos.Any())
                            {
                                _planRepository.DeleteRange(planDaos);
                                _utaskContext.SaveChanges();
                            }
                            _taskRepository.Delete(taskDao);
                            _utaskContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"Задача не найдена");
                    }
                }
                else
                {
                    throw new Exception($"Некорректный идентификатор пользователя");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
