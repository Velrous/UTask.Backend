using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Настройки пользователя
    /// </summary>
    public class UserSettingDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Пользователь
        /// </summary>
        public UserDao? User { get; set; }
    }
}
