using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Группа
    /// </summary>
    public class GroupDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
