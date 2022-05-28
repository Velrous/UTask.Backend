using UTask.Backend.Domain.Entities.Categories;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с категориями
    /// </summary>
    public interface ICategoryService : IBaseService
    {
        /// <summary>
        /// Создаёт новую категорию
        /// </summary>
        /// <param name="category">Новая категория</param>
        /// <returns>Созданная категория</returns>
        Category Create(Category category);

        /// <summary>
        /// Возвращает интерфейс для запроса категорий
        /// </summary>
        /// <returns>Интерфейс для запроса категорий</returns>
        IQueryable<Category> GetQueryable();

        /// <summary>
        /// Обновляет данные категории
        /// </summary>
        /// <param name="category">Измененная категория</param>
        void Update(Category category);

        /// <summary>
        /// Удаляет категорию по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        void Delete(long id);
    }
}
