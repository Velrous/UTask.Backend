using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Идентификатор типа задачи
        /// </summary>
        public int TaskTypeId { get; set; }
        /// <summary>
        /// Тип задачи
        /// </summary>
        public TaskTypeDao? TaskType { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
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
