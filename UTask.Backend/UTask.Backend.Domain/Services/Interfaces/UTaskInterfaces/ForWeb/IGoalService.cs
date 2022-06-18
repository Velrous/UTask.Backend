using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с целями
    /// </summary>
    public interface IGoalService : IBaseService
    {
        /// <summary>
        /// Создаёт новую цель
        /// </summary>
        /// <param name="goal">Новая цель</param>
        void Create(Goal goal);

        /// <summary>
        /// Обновляет данные цели
        /// </summary>
        /// <param name="goal">Измененная цель</param>
        void Update(Goal goal);

        /// <summary>
        /// Добавляет задачу к цели
        /// </summary>
        /// <param name="goalTaskRelation">Отношение цели и задачи</param>
        void AddTaskToGoal(GoalTaskRelation goalTaskRelation);

        /// <summary>
        /// Удаляет цель по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        void Delete(long id);

        /// <summary>
        /// Удаляет задачу у цели
        /// </summary>
        /// <param name="goalTaskRelation">Отношение цели и задачи</param>
        void DeleteTaskFromGoal(GoalTaskRelation goalTaskRelation);
    }
}
