using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с планами
    /// </summary>
    public interface IPlanService : IBaseService
    {
        /// <summary>
        /// Создаёт новый план
        /// </summary>
        /// <param name="plan">Новый план</param>
        void Create(Plan plan);

        /// <summary>
        /// Обновляет данные плана
        /// </summary>
        /// <param name="plan">Измененный план</param>
        void Update(Plan plan);

        /// <summary>
        /// Увеличивает позицию плана по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        void IncreasePosition(long id);

        /// <summary>
        /// Уменьшает позицию плана по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        void DecreasePosition(long id);

        /// <summary>
        /// Удаляет план по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор плана</param>
        void Delete(long id);
    }
}
