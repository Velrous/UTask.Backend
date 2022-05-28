using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Отношение пользователя и роли
    /// </summary>
    public class UserRoleRelationDao : EntityDao
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
        /// Идентификатор роли
        /// </summary>
        public int RoleId { get; set; }
        /// <summary>
        /// Роль
        /// </summary>
        public RoleDao? Role { get; set; }
        /// <summary>
        /// Дата и время истечения срока действия
        /// </summary>
        public DateTime? ExpirationDateTime { get; set; }
    }
}
