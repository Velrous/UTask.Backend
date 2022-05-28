using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Filters;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;

namespace UTask.Backend.WebAPI.Attributes
{
    /// <summary>
    /// Атрибут для идентификации пользователя
    /// </summary>
    public class UserIdentificationAttribute : Attribute, IActionFilter
    {
        #region Логирование

        private readonly ILogger<UserIdentificationAttribute> _logger;

        #endregion

        #region Сервисы

        private readonly IUserService _userService;

        #endregion

        public UserIdentificationAttribute()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляр сервиса логирования

            var factory = LoggerFactory.Create(b =>
            {
                b.AddEventLog();
            });
            _logger = factory.CreateLogger<UserIdentificationAttribute>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _userService = kernel.Get<IUserService>();

            #endregion
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            try
            {
                if (context.HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    var userClaims = identity.Claims;
                    var userData = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.UserData)?.Value;
                    if (!string.IsNullOrWhiteSpace(userData))
                    {
                        if (long.TryParse(userData, out long userId))
                        {
                            var userForWeb = _userService.GetQueryableForWeb().FirstOrDefault(x => x.Id == userId);
                            if (userForWeb != null)
                            {
                                ServerContext.UserId = userForWeb.Id;
                            }
                            else
                            {
                                _logger.Log(LogLevel.Error, $"Произошла ошибка при попытке идентификации пользователя: Пользователь не найден или неактивен {userId}");
                            }
                        }
                        else
                        {
                            _logger.Log(LogLevel.Error, $"Произошла ошибка при попытке идентификации пользователя: Не удалось сконвертировать id пользователя {userData}");
                        }
                    }
                    else
                    {
                        _logger.Log(LogLevel.Error, $"Произошла ошибка при попытке идентификации пользователя: Пустые данные пользователя");
                    }
                }
                else
                {
                    _logger.Log(LogLevel.Error, $"Произошла ошибка при попытке идентификации пользователя: Не удалось получить ClaimsIdentity");
                }
            }
            catch (Exception e)
            {
                _logger.Log(LogLevel.Error, $"Произошла ошибка при попытке идентификации пользователя: {e.Message}");
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
