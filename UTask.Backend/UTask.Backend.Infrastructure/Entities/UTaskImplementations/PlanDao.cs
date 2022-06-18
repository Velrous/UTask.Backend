using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// План
    /// </summary>
    public class PlanDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserDao? User { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public long TaskId { get; set; }
        /// <summary>
        /// Задача
        /// </summary>
        public TaskDao? Task { get; set; }
        /// <summary>
        /// Идентификатор приоритета плана
        /// </summary>
        public int PlanPriorityId { get; set; }
        /// <summary>
        /// Приоритет плана
        /// </summary>
        public PlanPriorityDao? PlanPriority { get; set; }
        /// <summary>
        /// Дата
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Позиция
        /// </summary>
        public int Position { get; set; }
    }
}
