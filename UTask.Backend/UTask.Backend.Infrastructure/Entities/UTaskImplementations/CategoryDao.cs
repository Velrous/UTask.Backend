using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Категория
    /// </summary>
    public class CategoryDao : EntityWithIdDao<long>
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
        /// Наименование
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
    }
}
