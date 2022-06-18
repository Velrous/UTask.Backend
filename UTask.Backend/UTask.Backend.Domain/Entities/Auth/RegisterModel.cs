using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Auth
{
    /// <summary>
    /// Данные для регистрации
    /// </summary>
    [DataContract]
    [Serializable]
    public class RegisterModel : IEntity
    {
        /// <summary>
        /// Отображаемое имя
        /// </summary>
        [Display(Name = "Отображаемое имя")]
        [DataMember(IsRequired = true)]
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Электронная почта
        /// </summary>
        [Display(Name = "Электронная почта")]
        [DataMember(IsRequired = true)]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; } = string.Empty;
        /// <summary>
        /// Пароль
        /// </summary>
        [Display(Name = "Пароль")]
        [DataMember]
        [JsonProperty(PropertyName = "Password")]
        public string Password { get; set; } = string.Empty;
    }
}
