using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;
using Task = UTask.Backend.Domain.Entities.Tasks.Task;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с задачами
    /// </summary>
    public interface ITaskService : IBaseService
    {
        /// <summary>
        /// Создаёт новую задачу
        /// </summary>
        /// <param name="task">Новая задача</param>
        void Create(Task task);

        /// <summary>
        /// Обновляет данные задачи
        /// </summary>
        /// <param name="task">Измененная задача</param>
        void Update(Task task);

        /// <summary>
        /// Удаляет задачу по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        void Delete(long id);
    }
}
