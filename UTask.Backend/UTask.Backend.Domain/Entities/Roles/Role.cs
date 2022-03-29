using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Domain.Entities.Roles
{
    /// <summary>
    /// Роль пользователь
    /// </summary>
    [DataContract]
    [Serializable]
    public class Role : IEntityWithId<int>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Display(Name = "Id")]
        [DataMember]
        [JsonProperty(PropertyName = "Id")]
        public int Id { get; set; }
        /// <summary>
        /// Наименование
        /// </summary>
        [Display(Name = "Наименование")]
        [DataMember]
        [JsonProperty(PropertyName = "Name")]
        public string Name { get; set; }
        /// <summary>
        /// Дата создания
        /// </summary>
        [Display(Name = "Дата создания")]
        [DataMember]
        [JsonProperty(PropertyName = "Created")]
        public DateTime Created { get; set; }
        /// <summary>
        /// Признак активности
        /// </summary>
        [Display(Name = "Признак активности")]
        [DataMember]
        [JsonProperty(PropertyName = "IsActive")]
        public bool IsActive { get; set; }
    }
}
