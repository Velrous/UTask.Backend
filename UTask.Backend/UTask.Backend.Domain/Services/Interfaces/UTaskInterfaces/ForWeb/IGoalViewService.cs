using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Domain.Entities.Tasks;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с представлением целей
    /// </summary>
    public interface IGoalViewService : IBaseService
    {
        /// <summary>
        /// Возвращает представление цели по её идентификатору
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        /// <returns>Представление цели</returns>
        GoalView GetById(long id);

        /// <summary>
        /// Возвращает интерфейс для запроса к представлению целей
        /// </summary>
        /// <returns>Интерфейс для запроса к представлению целей</returns>
        IQueryable<GoalView> GetQueryable();

        /// <summary>
        /// Возвращает интерфейс для запроса к представлению задач цели
        /// </summary>
        /// <param name="id">Идентификатор цели</param>
        /// <returns>Интерфейс для запроса к представлению задач цели</returns>
        IQueryable<TaskView> GetTasksForGoal(long id);
    }
}
