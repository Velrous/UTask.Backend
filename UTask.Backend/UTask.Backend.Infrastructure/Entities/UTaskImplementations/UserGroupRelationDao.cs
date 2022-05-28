using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Отношение пользователя и группы
    /// </summary>
    public class UserGroupRelationDao : EntityDao
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
        /// Идентификатор группы
        /// </summary>
        public long GroupId { get; set; }
        /// <summary>
        /// Группа
        /// </summary>
        public GroupDao? Group { get; set; }
    }
}
