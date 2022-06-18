using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Отображаемое имя
        /// </summary>
        [MaxLength(128)]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта
        /// </summary>
        [MaxLength(128)]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Соль
        /// </summary>
        [MaxLength(16)]
        public string Salt { get; set; } = string.Empty;
        /// <summary>
        /// Хэш пароля
        /// </summary>
        [MaxLength(256)]
        public string PasswordHash { get; set; } = string.Empty;
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
