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
    /// Web API контроллер работы с приоритетами планов
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class PlanPrioritiesController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<PlanPrioritiesController> _logger;

        #endregion

        #region Сервисы

        private readonly IPlanPriorityService _planPriorityService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с приоритетами планов
        /// </summary>
        public PlanPrioritiesController(ILogger<PlanPrioritiesController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _planPriorityService = kernel.Get<IPlanPriorityService>();

            #endregion
        }

        [HttpGet]
        public ActionResult Get()
        {
            try
            {
                return Ok(_planPriorityService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения типов задач, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
