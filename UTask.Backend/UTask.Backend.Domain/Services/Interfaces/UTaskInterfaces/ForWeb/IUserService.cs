using UTask.Backend.Domain.Entities.Users;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса работы с пользователями
    /// </summary>
    public interface IUserService : IBaseService
    {
        /// <summary>
        /// Возвращает интерфейс для запроса пользователей для веб-интерфейса
        /// </summary>
        /// <returns>Интерфейс для запроса пользователей для веб-интерфейса</returns>
        IQueryable<UserForWeb> GetQueryableForWeb();
    }
}
