using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Заметка
    /// </summary>
    public class NoteDao : EntityWithIdDao<long>
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
        /// Описание
        /// </summary>
        [MaxLength(1024)]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
    }
}
