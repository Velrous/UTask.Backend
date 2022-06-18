using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Plans
{
    /// <summary>
    /// План (Представление)
    /// </summary>
    [DataContract]
    [Serializable]
    public class PlanView : IEntityWithId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор приоритета плана
        /// </summary>
        [Display(Name = "Id приоритета плана")]
        [DataMember]
        [JsonProperty(PropertyName = "PlanPriorityId")]
        public int PlanPriorityId { get; set; }
        /// <summary>
        /// Наименование приоритета плана
        /// </summary>
        [Display(Name = "Наименование приоритета плана")]
        [DataMember]
        [JsonProperty(PropertyName = "PlanPriorityName")]
        public string PlanPriorityName { get; set; } = string.Empty;
        /// <summary>
        /// Значение приоритета плана
        /// </summary>
        [Display(Name = "Значение приоритета плана")]
        [DataMember]
        [JsonProperty(PropertyName = "PlanPriorityValue")]
        public string PlanPriorityValue { get; set; } = string.Empty;
        /// <summary>
        /// Дата
        /// </summary>
        [Display(Name = "Дата")]
        [DataMember]
        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }
        /// <summary>
        /// Позиция
        /// </summary>
        [Display(Name = "Позиция")]
        [DataMember]
        [JsonProperty(PropertyName = "Position")]
        public int Position { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Display(Name = "Id задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskId")]
        public long TaskId { get; set; }
        /// <summary>
        /// Наименование задачи
        /// </summary>
        [Display(Name = "Наименование задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskName")]
        public string TaskName { get; set; } = string.Empty;
        /// <summary>
        /// Идентификатор типа задачи
        /// </summary>
        [Display(Name = "Id типа задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskTypeId")]
        public long TaskTypeId { get; set; }
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
        /// Признак завершенности
        /// </summary>
        [Display(Name = "Признак завершенности")]
        [DataMember]
        [JsonProperty(PropertyName = "IsComplete")]
        public bool IsComplete { get; set; }
    }
}
