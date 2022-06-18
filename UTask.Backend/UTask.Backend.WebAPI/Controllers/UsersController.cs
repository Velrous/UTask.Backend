using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Users;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с пользователями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class UsersController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<UsersController> _logger;

        #endregion

        #region Сервисы

        private readonly IUserService _userService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с пользователями
        /// </summary>
        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _userService = kernel.Get<IUserService>();

            #endregion
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_userService.GetCurrentUserForWeb());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения текущего пользователя, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(UserForWeb userForWeb)
        {
            try
            {
                _userService.Update(userForWeb);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления данных пользователя, произошла ошибка: {e.Message}", ServerContext.UserId, userForWeb);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
