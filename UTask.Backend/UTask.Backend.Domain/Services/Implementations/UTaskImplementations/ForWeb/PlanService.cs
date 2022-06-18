using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с планами
    /// </summary>
    public class PlanService : BaseService, IPlanService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<PlanDao, long> _planRepository;

        #endregion

        /// <summary>
        /// Сервис работы с планами
        /// </summary>
        public PlanService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _planRepository = kernel.Get<IEntityWithIdRepository<PlanDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion
        }

        /// <summary>
        /// Создаёт новый план
        /// </summary>
        /// <param name="plan">Новый план</param>
        public void Create(Plan plan)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var similarPlanDao = _planRepository.GetQueryable()
                        .Where(x => x.UserId == ServerContext.UserId)
                        .Where(x => x.Date.Date == plan.Date.Date)
                        .FirstOrDefault(x => x.TaskId == plan.TaskId);
                    if (similarPlanDao == null)
                    {
                        var lastPlanDao = _planRepository.GetQueryable()
                            .Where(x => x.UserId == ServerContext.UserId)
                            .Where(x => x.Date.Date == plan.Date.Date)
                            .OrderByDescending(x => x.Position)
                            .FirstOrDefault();
                        var planDao = new PlanDao
                        {
                            UserId = ServerContext.UserId,
                            TaskId = plan.TaskId,
                            PlanPriorityId = plan.PlanPriorityId,
                            Date = plan.Date,
                            Position = lastPlanDao?.Position + 1 ?? 0
                        };
                        _planRepository.Create(planDao);
                        _utaskContext.SaveChanges();
                    }
                    else
                    {
                        similarPlanDao.PlanPriorityId = plan.PlanPriorityId;
                        similarPlanDao.Date = plan.Date;
                        _planRepository.Update(similarPlanDao);
                        _utaskContext.SaveChanges();
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
        /// Обновляет данные плана
        /// </summary>
        /// <param name="plan">Измененный план</param>
        public void Update(Plan plan)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var planDao = _planRepository.GetById(plan.Id);
                    if (planDao != null)
                    {
                        if (planDao.UserId == ServerContext.UserId)
                        {
                            var similarPlanDao = _planRepository.GetQueryable()
                                .Where(x => x.UserId == ServerContext.UserId)
                                .Where(x => x.Date.Date == plan.Date.Date)
                                .FirstOrDefault(x => x.TaskId == plan.TaskId);
                            if (similarPlanDao != null)
                            {
                                _planRepository.Delete(similarPlanDao);
                                _utaskContext.SaveChanges();
                            }
                            var lastPlanDao = _planRepository.GetQueryable()
                                .Where(x => x.UserId == ServerContext.UserId)
                                .Where(x => x.Date.Date == plan.Date.Date)
                                .OrderByDescending(x => x.Position)
                                .FirstOrDefault();
                            planDao.TaskId = plan.TaskId;
                            planDao.PlanPriorityId = plan.PlanPriorityId;
                            planDao.Date = plan.Date;
                            planDao.Position = lastPlanDao?.Position + 1 ?? 0;
                            _planRepository.Update(planDao);
                            _utaskContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"План не найден");
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
        /// Увеличивает позицию плана по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        public void IncreasePosition(long id)
        {
            try
            {
                ChangePosition(id, true);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Уменьшает позицию плана по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        public void DecreasePosition(long id)
        {
            try
            {
                ChangePosition(id, false);
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет план по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        public void Delete(long id)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var planDao = _planRepository.GetById(id);
                    if (planDao != null)
                    {
                        if (planDao.UserId == ServerContext.UserId)
                        {
                            _planRepository.Delete(planDao);
                            _utaskContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"План не найден");
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
        /// Изменяет позицию плана
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        /// <param name="isIncrease">Признак увеличения позиции (если false, то понижение)</param>
        private void ChangePosition(long id, bool isIncrease)
        {
            if (ServerContext.UserId > 0)
            {
                var planDao = _planRepository.GetById(id);
                if (planDao != null)
                {
                    if (planDao.UserId == ServerContext.UserId)
                    {
                        var planDaos = _planRepository.GetQueryable()
                            .Where(x => x.UserId == ServerContext.UserId)
                            .Where(x => x.Date.Date == planDao.Date.Date)
                            .OrderBy(x => x.Position)
                            .ToList();
                        var index = planDaos.IndexOf(planDao);
                        if (index > -1)
                        {
                            var updatedPlanDaos = new List<PlanDao>();
                            if ((index + 1 != planDaos.Count && isIncrease) || (index != 0 && !isIncrease))
                            {
                                var tempPlanDao = isIncrease ? planDaos[index + 1] : planDaos[index - 1];
                                (tempPlanDao.Position, planDao.Position) = (planDao.Position, tempPlanDao.Position);
                                updatedPlanDaos.Add(planDao);
                                updatedPlanDaos.Add(tempPlanDao);
                            }
                            if (updatedPlanDaos.Any())
                            {
                                _planRepository.UpdateRange(updatedPlanDaos);
                                _utaskContext.SaveChanges();
                            }
                        }
                        else
                        {
                            throw new Exception($"План не найден в списке по дате");
                        }
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя не совпадает");
                    }
                }
                else
                {
                    throw new Exception($"План не найден");
                }
            }
            else
            {
                throw new Exception($"Некорректный идентификатор пользователя");
            }
        }
    }
}
