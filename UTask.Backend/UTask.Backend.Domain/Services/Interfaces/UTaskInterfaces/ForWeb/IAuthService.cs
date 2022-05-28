using UTask.Backend.Domain.Entities.Auth;
using UTask.Backend.Domain.Services.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb
{
    /// <summary>
    /// Интерфейс сервиса аутентификации
    /// </summary>
    public interface IAuthService : IBaseService
    {
        /// <summary>
        /// Производит аутентификацию пользователя для веб интерфейса
        /// </summary>
        /// <param name="authModel">Данные аутентификации</param>
        /// <returns>Результат аутентификации</returns>
        AuthResultModel AuthByСredentials(AuthModel authModel);

        /// <summary>
        /// Производит аутентификацию пользователя для веб интерфейса по полученному токену
        /// </summary>
        /// <returns>Результат аутентификации</returns>
        AuthResultModel AuthByToken();
    }
}
