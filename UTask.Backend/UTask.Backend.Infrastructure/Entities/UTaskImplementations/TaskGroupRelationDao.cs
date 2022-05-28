using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Отношение задачи к группе
    /// </summary>
    public class TaskGroupRelationDao : EntityDao
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
        /// Идентификатор группы
        /// </summary>
        public long GroupId { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public GroupDao? Group { get; set; }
    }
}
