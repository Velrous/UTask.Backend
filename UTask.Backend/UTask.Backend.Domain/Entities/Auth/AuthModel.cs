﻿using System.ComponentModel.DataAnnotations;
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
        /// Электронная почта
        /// </summary>
        [Display(Name = "Электронная почта")]
        [DataMember(IsRequired = true)]
        [JsonProperty(PropertyName = "Email")]
        public string? Email { get; set; }
        /// <summary>
        /// Пароль
        /// </summary>
        [Display(Name = "Пароль")]
        [DataMember]
        [JsonProperty(PropertyName = "Password")]
        public string? Password { get; set; }
        /// <summary>
        /// Признак необходимости запомнить пользователя
        /// </summary>
        [Display(Name = "Признак необходимости запомнить пользователя")]
        [DataMember]
        [JsonProperty(PropertyName = "IsNeedRemember")]
        public bool IsNeedRemember { get; set; }
    }
}
