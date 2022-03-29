using UTask.Backend.Domain.Entities.Roles;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с ролями пользователей
    /// </summary>
    public interface IRoleService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса ролей пользователей
        /// </summary>
        /// <returns>Интерфейс для запроса ролей пользователей</returns>
        IQueryable<Role> GetQueryable();
    }
}
