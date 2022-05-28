using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Тип задачи
    /// </summary>
    public class TaskTypeDao : EntityWithIdDao<int>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
