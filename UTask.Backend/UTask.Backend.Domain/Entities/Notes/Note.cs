using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Notes
{
    /// <summary>
    /// Заметка
    /// </summary>
    [DataContract]
    [Serializable]
    public class Note : IEntityWithId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [Display(Name = "Описание")]
        [DataMember]
        [JsonProperty(PropertyName = "Description")]
        public string Description { get; set; } = string.Empty;
        /// <summary>
        /// Дата и время создания
        /// </summary>
        [Display(Name = "Дата и время создания")]
        [DataMember]
        [JsonProperty(PropertyName = "Created")]
        public DateTime Created { get; set; }
    }
}
