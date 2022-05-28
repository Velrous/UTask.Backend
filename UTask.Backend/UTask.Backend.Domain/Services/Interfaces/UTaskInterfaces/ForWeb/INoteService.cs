using UTask.Backend.Domain.Entities.Notes;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с заметками
    /// </summary>
    public interface INoteService : IBaseService
    {
        /// <summary>
        /// Создаёт новую заметку
        /// </summary>
        /// <param name="note">Новая заметка</param>
        /// <returns>Созданная заметка</returns>
        Note Create(Note note);

        /// <summary>
        /// Возвращает интерфейс для запроса заметок
        /// </summary>
        /// <returns>Интерфейс для запроса заметок</returns>
        IQueryable<Note> GetQueryable();

        /// <summary>
        /// Обновляет данные заметки
        /// </summary>
        /// <param name="note">Измененная заметка</param>
        void Update(Note note);

        /// <summary>
        /// Удаляет переданную заметку
        /// </summary>
        /// <param name="id">Идентификатор заметки</param>
        void Delete(long id);
    }
}
