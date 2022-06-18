using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Users
{
    /// <summary>
    /// Пользователь для отображения на клиенте
    /// </summary>
    [DataContract]
    [Serializable]
    public class UserForWeb : IEntityWithId<long>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public long Id { get; set; }

        /// <summary>
        /// Отображаемое имя
        /// </summary>
        [Display(Name = "Отображаемое имя")]
        [DataMember]
        [JsonProperty(PropertyName = "DisplayName")]
        public string DisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Электронная почта
        /// </summary>
        [Display(Name = "Электронная почта")]
        [DataMember]
        [JsonProperty(PropertyName = "Email")]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Старый пароль
        /// </summary>
        [Display(Name = "Старый пароль")]
        [DataMember]
        [JsonProperty(PropertyName = "OldPassword")]
        public string OldPassword { get; set; } = string.Empty;

        /// <summary>
        /// Новый пароль
        /// </summary>
        [Display(Name = "Новый пароль")]
        [DataMember]
        [JsonProperty(PropertyName = "NewPassword")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
