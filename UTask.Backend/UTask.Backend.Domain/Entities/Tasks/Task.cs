using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Tasks
{
    /// <summary>
    /// Задача
    /// </summary>
    [DataContract]
    [Serializable]
    public class Task : IEntityWithId<long>
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
        [Display(Name = "Id типа задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskTypeId")]
        public int TaskTypeId { get; set; }
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Display(Name = "Id категории")]
        [DataMember]
        [JsonProperty(PropertyName = "CategoryId")]
        public long? CategoryId { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Признак завершенности
        /// </summary>
        [Display(Name = "Признак завершенности")]
        [DataMember]
        [JsonProperty(PropertyName = "IsComplete")]
        public bool IsComplete { get; set; }
    }
}
