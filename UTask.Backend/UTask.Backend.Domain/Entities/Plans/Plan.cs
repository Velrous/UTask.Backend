using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Plans
{
    /// <summary>
    /// План
    /// </summary>
    [DataContract]
    [Serializable]
    public class Plan : IEntityWithId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Display(Name = "Id задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskId")]
        public long TaskId { get; set; }
        /// <summary>
        /// Идентификатор приоритета плана
        /// </summary>
        [Display(Name = "Id приоритета плана")]
        [DataMember]
        [JsonProperty(PropertyName = "PlanPriorityId")]
        public int PlanPriorityId { get; set; }
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
    }
}
