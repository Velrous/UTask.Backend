using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Filters;
using UTask.Backend.Domain.Entities.Plans;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с планами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class PlansController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<PlansController> _logger;

        #endregion

        #region Сервисы

        private readonly IPlanService _planService;
        private readonly IPlanViewService _planViewService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с планами
        /// </summary>
        public PlansController(ILogger<PlansController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _planService = kernel.Get<IPlanService>();
            _planViewService = kernel.Get<IPlanViewService>();

            #endregion
        }

        [HttpPost]
        public ActionResult Create(Plan plan)
        {
            try
            {
                _planService.Create(plan);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке создания плана, произошла ошибка: {e.Message}", ServerContext.UserId, plan);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPost("GetByDate")]
        public ActionResult GetByDate(PlanFilter planFilter)
        {
            try
            {
                return Ok(_planViewService.GetQueryableByDate(planFilter));
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения планов по дню, произошла ошибка: {e.Message}", ServerContext.UserId, planFilter);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet("Inc/{id}")]
        public ActionResult IncreasePosition(long id)
        {
            try
            {
                _planService.IncreasePosition(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке увеличения позиции плана, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet("Dec/{id}")]
        public ActionResult DecreasePosition(long id)
        {
            try
            {
                _planService.DecreasePosition(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке уменьшения позиции плана, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(Plan plan)
        {
            try
            {
                _planService.Update(plan);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления плана, произошла ошибка: {e.Message}", ServerContext.UserId, plan);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _planService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления плана, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
