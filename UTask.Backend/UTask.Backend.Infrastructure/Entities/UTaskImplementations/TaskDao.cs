﻿using System.ComponentModel.DataAnnotations;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Entities.UTaskImplementations
{
    /// <summary>
    /// Задача
    /// </summary>
    public class TaskDao : EntityWithIdDao<long>
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
        /// Идентификатор типа задачи
        /// </summary>
        public int TaskTypeId { get; set; }
        /// <summary>
        /// Тип задачи
        /// </summary>
        public TaskTypeDao? TaskType { get; set; }
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        public long? CategoryId { get; set; }
        /// <summary>
        /// Категория
        /// </summary>
        public CategoryDao? Category { get; set; }
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
