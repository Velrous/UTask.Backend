using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations.Views
{
    /// <summary>
    /// Задача (Представление)
    /// </summary>
    public class TaskViewDao : EntityWithIdDao<long>
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Идентификатор типа задачи
        /// </summary>
        public int TaskTypeId { get; set; }
        /// <summary>
        /// Наименование типа задачи
        /// </summary>
        [MaxLength(128)]
        public string TaskTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public long? CategoryId { get; set; }
        /// <summary>
        /// Наименование категории
        /// </summary>
        [MaxLength(128)]
        public string? CategoryName { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [MaxLength(512)]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак завершенности
        /// </summary>
        public bool IsComplete { get; set; }
    }
}
