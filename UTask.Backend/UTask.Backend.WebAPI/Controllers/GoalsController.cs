using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Goals;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с целями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class GoalsController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<GoalsController> _logger;

        #endregion

        #region Сервисы

        private readonly IGoalService _goalService;
        private readonly IGoalViewService _goalViewService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с целями
        /// </summary>
        public GoalsController(ILogger<GoalsController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _goalService = kernel.Get<IGoalService>();
            _goalViewService = kernel.Get<IGoalViewService>();

            #endregion
        }

        [HttpPost]
        public ActionResult Create(Goal goal)
        {
            try
            {
                _goalService.Create(goal);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке создания цели, произошла ошибка: {e.Message}", ServerContext.UserId, goal);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet("{id}")]
        public ActionResult GetById(long id)
        {
            try
            {
                return Ok(_goalViewService.GetById(id));
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения целей, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet, UseQuery]
        public ActionResult Get()
        {
            try
            {
                return Ok(_goalViewService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения целей, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet("GetTasks/{id}")]
        public ActionResult GetTasksForGoal(long id)
        {
            try
            {
                return Ok(_goalViewService.GetTasksForGoal(id));
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения списка задач цели, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(Goal goal)
        {
            try
            {
                _goalService.Update(goal);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления цели, произошла ошибка: {e.Message}", ServerContext.UserId, goal);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPost("AddTask")]
        public ActionResult AddTaskToGoal(GoalTaskRelation goalTaskRelation)
        {
            try
            {
                _goalService.AddTaskToGoal(goalTaskRelation);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке добавления задачи к цели, произошла ошибка: {e.Message}", ServerContext.UserId, goalTaskRelation);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _goalService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления цели, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPost("DeleteTask")]
        public ActionResult DeleteTaskFromGoal(GoalTaskRelation goalTaskRelation)
        {
            try
            {
                _goalService.DeleteTaskFromGoal(goalTaskRelation);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления задачи у цели, произошла ошибка: {e.Message}", ServerContext.UserId, goalTaskRelation);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
