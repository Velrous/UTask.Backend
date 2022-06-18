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
        /// Возвращает текущего пользователя для веб-интерфейса
        /// </summary>
        /// <returns>Пользователь для отображения на клиенте</returns>
        UserForWeb GetCurrentUserForWeb();

        /// <summary>
        /// Возвращает интерфейс для запроса к пользователям для отображения на клиенте
        /// </summary>
        /// <returns>Интерфейс для запроса к пользователям для отображения на клиенте</returns>
        IQueryable<UserForWeb> GetQueryableForWeb();

        /// <summary>
        /// Обновляет данные пользователя
        /// <param name="userForWeb">Пользователь для отображения на клиенте</param>
        /// </summary>
        void Update(UserForWeb userForWeb);
    }
}
