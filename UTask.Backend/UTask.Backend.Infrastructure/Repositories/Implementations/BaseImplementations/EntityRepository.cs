using Microsoft.EntityFrameworkCore;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Infrastructure.Repositories.Implementations.BaseImplementations
{
    /// <summary>
    /// Базовый репозиторий работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class EntityRepository<T> : IEntityRepository<T> where T : EntityDao
    {
        protected readonly BaseDbContext Context;

        /// <summary>
        /// Базовый репозиторий работы с сущностями
        /// </summary>
        /// <param name="context">Контекст работы с БД</param>
        public EntityRepository(BaseDbContext context)
        {
            Context = context;
        }

        /// <summary>
        /// Возвращает интерфейс для запроса
        /// </summary>
        /// <returns>Интерфейс для запроса</returns>
        public IQueryable<T> GetQueryable()
        {
            return Context.Set<T>();
        }

        /// <summary>
        /// Создает объект
        /// </summary>
        /// <param name="entity">Объект, который нужно создать</param>
        /// <returns>Созданный объект</returns>
        public T Create(T entity)
        {
            return Context.Set<T>().Add(entity).Entity;
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Context.Set<T>().AddRange(entities);
        }

        /// <summary>
        /// Обновляет данные об объекте
        /// </summary>
        /// <param name="entity">Обновляемые данные об объекте</param>
        public void Update(T entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Обновляет список объектов
        /// </summary>
        /// <param name="entities">Список объектов, которые нужно обновить</param>
        public void UpdateRange(IEnumerable<T> entities)
        {
            Context.Set<T>().UpdateRange(entities);
        }

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="entity">Объект, который нужно удалить</param>
        public void Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
        }

        /// <summary>
        /// Удаляет список объектов
        /// </summary>
        /// <param name="entities">Список объектов, которые нужно удалить</param>
        public void DeleteRange(IEnumerable<T> entities)
        {
            Context.Set<T>().RemoveRange(entities);
        }

        #region IDisposable

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
