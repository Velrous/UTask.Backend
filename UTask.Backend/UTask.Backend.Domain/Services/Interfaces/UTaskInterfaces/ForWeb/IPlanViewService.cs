using UTask.Backend.Domain.Entities.Filters;
using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с представлением планов
    /// </summary>
    public interface IPlanViewService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса к представлению планов по дате
        /// </summary>
        /// <param name="planFilter">Фильтр плана</param>
        /// <returns>Интерфейс для запроса к представлению планов по дате</returns>
        IQueryable<PlanView> GetQueryableByDate(PlanFilter planFilter);
    }
}
