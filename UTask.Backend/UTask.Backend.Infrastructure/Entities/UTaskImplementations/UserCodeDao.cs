using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Код пользователя
    /// </summary>
    public class UserCodeDao : EntityWithIdDao<long>
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
        /// Код
        /// </summary>
        public int Code { get; set; }
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Дата и время истечения срока действия
        /// </summary>
        public DateTime? ExpirationDateTime { get; set; }
        /// <summary>
        /// Признак действительности
        /// </summary>
        public bool IsValid { get; set; }
    }
}
