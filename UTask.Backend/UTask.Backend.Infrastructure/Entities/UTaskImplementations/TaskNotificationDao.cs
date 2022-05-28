using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Уведомление по задаче
    /// </summary>
    public class TaskNotificationDao : EntityDao
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// Задача
        /// </summary>
        public TaskDao? Task { get; set; }
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак активности
        /// </summary>
        public bool IsActive { get; set; }
    }
}
