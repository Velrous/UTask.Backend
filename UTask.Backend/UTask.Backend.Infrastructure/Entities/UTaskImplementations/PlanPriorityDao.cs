using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Приоритет плана
    /// </summary>
    public class PlanPriorityDao : EntityWithIdDao<int>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Значение
        /// </summary>
        [MaxLength(64)]
        public string Value { get; set; } = string.Empty;
    }
}
