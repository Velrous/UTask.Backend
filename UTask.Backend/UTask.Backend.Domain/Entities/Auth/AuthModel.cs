using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Auth
{
    /// <summary>
    /// Данные для аутентификации
    /// </summary>
    [DataContract]
    [Serializable]
    public class AuthModel : IEntity
    {
        /// <summary>
        /// Логин
        /// </summary>
        [Display(Name = "Логин")]
        [DataMember(IsRequired = true)]
        [JsonProperty(PropertyName = "Login")]
        public string? Login { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Display(Name = "Пароль")]
        [DataMember]
        [JsonProperty(PropertyName = "Password")]
        public string? Password { get; set; }
    }
}
