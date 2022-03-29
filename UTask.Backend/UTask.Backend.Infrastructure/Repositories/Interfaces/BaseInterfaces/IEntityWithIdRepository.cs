using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Интерфейс базового репозитория работы с сущностями с идентификаторами
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public interface IEntityWithIdRepository<T, TKey> : IEntityRepository<T> where T : EntityWithIdDao<TKey>
    {
        /// <summary>
        /// Возвращает объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Объект</returns>
        T GetById(TKey id);

        /// <summary>
        /// Возвращает объект по его идентификатору (только из БД)
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Объект</returns>
        T GetByIdFromDatabase(TKey id);
    }
}
