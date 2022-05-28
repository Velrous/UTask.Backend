using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Results
{
    /// <summary>
    /// Базовый результат
    /// </summary>
    [DataContract]
    [Serializable]
    public class BaseResult : IEntity
    {
        /// <summary>
        /// Текст ошибки
        /// </summary>
        [Display(Name = "Текст ошибки")]
        [DataMember]
        [JsonProperty(PropertyName = "ErrorText")]
        public string? ErrorText { get; set; }
        /// <summary>
        /// Признак успешности
        /// </summary>
        [Display(Name = "Признак успешности")]
        [DataMember]
        [JsonProperty(PropertyName = "IsSuccess")]
        public bool IsSuccess { get; set; }
    }
}
