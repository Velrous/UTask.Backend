using UTask.Backend.Domain.Entities.PlanPriorities;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с приоритетами планов
    /// </summary>
    public interface IPlanPriorityService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса к приоритетам планов
        /// </summary>
        /// <returns>Интерфейс для запроса к приоритетам планов</returns>
        IQueryable<PlanPriority> GetQueryable();
    }
}
