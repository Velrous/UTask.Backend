using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Goals
{
    /// <summary>
    /// Отношение цели и задачи
    /// </summary>
    [DataContract]
    [Serializable]
    public class GoalTaskRelation : IEntity
    {
        /// <summary>
        /// Идентификатор цели
        /// </summary>
        [Display(Name = "Id цели")]
        [DataMember]
        [JsonProperty(PropertyName = "GoalId")]
        public long GoalId { get; set; }
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        [Display(Name = "Id задачи")]
        [DataMember]
        [JsonProperty(PropertyName = "TaskId")]
        public long TaskId { get; set; }
    }
}
