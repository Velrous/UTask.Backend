using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Отношение цели и задачи
    /// </summary>
    public class GoalTaskRelationDao : EntityDao
    {
        /// <summary>
        /// Идентификатор цели
        /// </summary>
        public long GoalId { get; set; }
        /// <summary>
        /// Цель
        /// </summary>
        public GoalDao? Goal { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// Задача
        /// </summary>
        public TaskDao? Task { get; set; }
    }
}
