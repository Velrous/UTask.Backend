using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Infrastructure.Repositories.Implementations.BaseImplementations
{
    /// <summary>
    /// Базовый репозиторий работы с сущностями с идентификаторами в режиме "только чтение"
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    /// <typeparam name="TKey">Тип идентификатора сущности</typeparam>
    public class ReadOnlyEntityWithIdRepository<T, TKey> : ReadOnlyEntityRepository<T>, IReadOnlyEntityWithIdRepository<T, TKey> where T : EntityWithIdDao<TKey>
    {
        /// <summary>
        /// Базовый репозиторий работы с сущностями с идентификаторами в режиме "только чтение"
        /// </summary>
        /// <param name="context">Контекст работы с БД</param>
        public ReadOnlyEntityWithIdRepository(BaseDbContext context) : base(context)
        {
        }

        /// <summary>
        /// Возвращает объект по его идентификатору
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Объект</returns>
        public T GetById(TKey id)
        {
            return Context.Set<T>().Find(id);
        }
    }
}
