using UTask.Backend.Common.Base.Entities;

namespace UTask.Backend.Infrastructure.Entities.BaseImplementations
{
    /// <summary>
    /// Базовый абстрактный класс для всех DAO сущностей с идентификатором
    /// </summary>
    public abstract class EntityWithIdDao<TKey> : EntityDao, IEntityWithId<TKey>
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public TKey Id { get; set; }
    }
}
