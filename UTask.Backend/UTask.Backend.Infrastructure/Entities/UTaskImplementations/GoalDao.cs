using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    public class GoalDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Наименование
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Описание
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак завершенности
        /// </summary>
        public bool IsComplete { get; set; }
        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
