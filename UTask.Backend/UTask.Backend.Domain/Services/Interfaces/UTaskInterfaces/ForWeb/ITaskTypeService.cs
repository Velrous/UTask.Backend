using UTask.Backend.Domain.Entities.TaskTypes;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с типами задач
    /// </summary>
    public interface ITaskTypeService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса типов задач
        /// </summary>
        /// <returns>Интерфейс для запроса типов задач</returns>
        IQueryable<TaskType> GetQueryable();
    }
}
