using UTask.Backend.Common.Base.Repositories;
using UTask.Backend.Infrastructure.Entities.BaseImplementations;

namespace UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces
{
    /// <summary>
    /// Интерфейс базового репозитория работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IEntityRepository<T> : IRepository where T : EntityDao
    {
        /// <summary>
        /// Возвращает интерфейс для запроса
        /// </summary>
        /// <returns>Интерфейс для запроса</returns>
        IQueryable<T> GetQueryable();

        /// <summary>
        /// Создает объект
        /// </summary>
        /// <param name="entity">Объект, который нужно создать</param>
        /// <returns>Созданный объект</returns>
        T Create(T entity);

        /// <summary>
        /// Добавляет список объектов
        /// </summary>
        /// <param name="entities">Список объектов, которые нужно добавить</param>
        void AddRange(IEnumerable<T> entities);

        /// <summary>
        /// Обновляет данные об объекте
        /// </summary>
        /// <param name="entity">Обновляемые данные об объекте</param>
        void Update(T entity);

        /// <summary>
        /// Обновляет список объектов
        /// </summary>
        /// <param name="entities">Список объектов, которые нужно обновить</param>
        void UpdateRange(IEnumerable<T> entities);

        /// <summary>
        /// Удаляет объект
        /// </summary>
        /// <param name="entity">Объект, который нужно удалить</param>
        void Delete(T entity);

        /// <summary>
        /// Удаляет список объектов
        /// </summary>
        /// <param name="entities">Список объектов, которые нужно удалить</param>
        void DeleteRange(IEnumerable<T> entities);
    }
}
