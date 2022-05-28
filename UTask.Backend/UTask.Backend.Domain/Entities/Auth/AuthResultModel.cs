using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Domain.Entities.Results;

namespace UTask.Backend.Domain.Entities.Auth
{
    /// <summary>
    /// Результат аутентификации для утилиты
    /// </summary>
    [DataContract]
    [Serializable]
    public class AuthResultModel : BaseResult
    {
        /// <summary>
        /// Отображаемое имя
        /// </summary>
        [Display(Name = "Отображаемое имя")]
        [DataMember]
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Токен
        /// </summary>
        [Display(Name = "Токен")]
        [DataMember]
        [JsonProperty(PropertyName = "Token")]
        public string Token { get; set; } = string.Empty;
    }
}
