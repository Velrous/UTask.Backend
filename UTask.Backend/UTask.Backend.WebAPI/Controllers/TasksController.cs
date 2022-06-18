using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;
using Task = UTask.Backend.Domain.Entities.Tasks.Task;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с задачами
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class TasksController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<TasksController> _logger;

        #endregion

        #region Сервисы

        private readonly ITaskService _taskService;
        private readonly ITaskViewService _taskViewService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с задачами
        /// </summary>
        public TasksController(ILogger<TasksController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _taskService = kernel.Get<ITaskService>();
            _taskViewService = kernel.Get<ITaskViewService>();

            #endregion
        }

        [HttpPost]
        public ActionResult Create(Task task)
        {
            try
            {
                _taskService.Create(task);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке создания задачи, произошла ошибка: {e.Message}", ServerContext.UserId, task);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet, UseQuery]
        public ActionResult Get()
        {
            try
            {
                return Ok(_taskViewService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения задач, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet("ForAddToGoal/{goalId}"), UseQuery]
        public ActionResult GetTasksToAddToGoal(long goalId)
        {
            try
            {
                return Ok(_taskViewService.GetTasksToAddToGoal(goalId));
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения задач для добавления к цели, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(Task task)
        {
            try
            {
                _taskService.Update(task);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления задачи, произошла ошибка: {e.Message}", ServerContext.UserId, task);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _taskService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления задачи, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
