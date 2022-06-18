using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.PlanPriorities
{
    /// <summary>
    /// Приоритет плана
    /// </summary>
    [DataContract]
    [Serializable]
    public class PlanPriority : IEntityWithId<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Значение
        /// </summary>
        [Display(Name = "Значение")]
        [DataMember]
        [JsonProperty(PropertyName = "Value")]
        public string Value { get; set; } = string.Empty;
    }
}
