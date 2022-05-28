using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Отношение пользователя и цели
    /// </summary>
    public class UserGoalRelationDao : EntityDao
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
        /// Идентификатор цели
        /// </summary>
        public long GoalId { get; set; }
        /// <summary>
        /// Цель
        /// </summary>
        public GoalDao? Goal { get; set; }
    }
}
