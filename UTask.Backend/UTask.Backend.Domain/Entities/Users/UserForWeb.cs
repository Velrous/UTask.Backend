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
        /// Логин
        /// </summary>
        [Display(Name = "Логин")]
        [DataMember]
        [JsonProperty(PropertyName = "Login")]
        public string Login { get; set; } = string.Empty;

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
        /// Дата рождения
        /// </summary>
        [Display(Name = "Дата рождения")]
        [DataMember]
        [JsonProperty(PropertyName = "DateOfBirth")]
        public DateTime DateOfBirth { get; set; }
    }
}
