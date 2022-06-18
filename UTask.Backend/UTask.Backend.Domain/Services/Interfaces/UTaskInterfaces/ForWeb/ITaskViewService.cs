using UTask.Backend.Domain.Entities.Tasks;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с представлением задач
    /// </summary>
    public interface ITaskViewService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса представления задач
        /// </summary>
        /// <returns>Интерфейс для запроса представления задач</returns>
        IQueryable<TaskView> GetQueryable();

        /// <summary>
        /// Возвращает интерфейс для запроса к представлению задач, которые могут быть добавлены к цели
        /// </summary>
        /// <param name="goalId">Идентификатор цели</param>
        /// <returns>Интерфейс для запроса к представлению задач, которые могут быть добавлены к цели</returns>
        IQueryable<TaskView> GetTasksToAddToGoal(long goalId);
    }
}
