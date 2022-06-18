using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Filters
{
    /// <summary>
    /// Фильтр плана
    /// </summary>
    [DataContract]
    [Serializable]
    public class PlanFilter : IEntity
    {
        /// <summary>
        /// Дата
        /// </summary>
        [Display(Name = "Дата")]
        [DataMember]
        [JsonProperty(PropertyName = "Date")]
        public DateTime Date { get; set; }
    }
}
