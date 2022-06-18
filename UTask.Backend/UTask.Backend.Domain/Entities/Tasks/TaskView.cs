using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Tasks
{
    /// <summary>
    /// Задача (Представление)
    /// </summary>
    [DataContract]
    [Serializable]
    public class TaskView : IEntityWithId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор типа задачи
        /// </summary>
        [Display(Name = "Id типа задача")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskTypeId")]
        public int TaskTypeId { get; set; }
        /// <summary>
        /// Наименование типа задачи
        /// </summary>
        [Display(Name = "Наименование типа задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskTypeName")]
        public string TaskTypeName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Display(Name = "Id категории")]
        [DataMember]
        [JsonProperty(PropertyName = "CategoryId")]
        public long? CategoryId { get; set; }
        /// <summary>
        /// Наименование категории
        /// </summary>
        [Display(Name = "Наименование категории")]
        [DataMember]
        [JsonProperty(PropertyName = "CategoryName")]
        public string? CategoryName { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
        /// </summary>
        [Display(Name = "Дата и время создания")]
        [DataMember]
        [JsonProperty(PropertyName = "Created")]
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак завершенности
        /// </summary>
        [Display(Name = "Признак завершенности")]
        [DataMember]
        [JsonProperty(PropertyName = "IsComplete")]
        public bool IsComplete { get; set; }
    }
}
