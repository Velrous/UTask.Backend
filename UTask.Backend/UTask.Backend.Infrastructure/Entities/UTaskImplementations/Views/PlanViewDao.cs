using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views
{
    /// <summary>
    /// План (представление)
    /// </summary>
    public class PlanViewDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Идентификатор приоритета плана
        /// </summary>
        public int PlanPriorityId { get; set; }
        /// <summary>
        /// Наименование приоритета плана
        /// </summary>
        [MaxLength(128)]
        public string PlanPriorityName { get; set; } = string.Empty;
        /// <summary>
        /// Значение приоритета плана
        /// </summary>
        [MaxLength(64)]
        public string PlanPriorityValue { get; set; } = string.Empty;
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Позиция
        /// </summary>
        public int Position { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        [MaxLength(512)]
        public string TaskName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор типа задачи
        /// </summary>
        public long TaskTypeId { get; set; }
        /// <summary>
        /// Наименование типа задачи
        /// </summary>
        [MaxLength(128)]
        public string TaskTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public long? CategoryId { get; set; }
        /// <summary>
        /// Наименование категории
        /// </summary>
        [MaxLength(128)]
        public string? CategoryName { get; set; }
        /// <summary>
        /// Признак завершенности
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
