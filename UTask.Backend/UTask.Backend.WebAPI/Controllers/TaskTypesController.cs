using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с типами задач
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class TaskTypesController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<TaskTypesController> _logger;

        #endregion

        #region Сервисы

        private readonly ITaskTypeService _taskTypeService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с типами задач
        /// </summary>
        public TaskTypesController(ILogger<TaskTypesController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _taskTypeService = kernel.Get<ITaskTypeService>();

            #endregion
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_taskTypeService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения типов задач, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
