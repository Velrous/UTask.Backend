using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Notes;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с заметками
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class NotesController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<NotesController> _logger;

        #endregion

        #region Сервисы

        private readonly INoteService _noteService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с ролями
        /// </summary>
        public NotesController(ILogger<NotesController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _noteService = kernel.Get<INoteService>();

            #endregion
        }

        [HttpPost]
        public ActionResult Create(Note note)
        {
            try
            {
                _noteService.Create(note);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке создания заметки, произошла ошибка: {e.Message}", ServerContext.UserId, note);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet, UseQuery]
        public ActionResult Get()
        {
            try
            {
                return Ok(_noteService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения заметок, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(Note note)
        {
            try
            {
                _noteService.Update(note);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления заметки, произошла ошибка: {e.Message}", ServerContext.UserId, note);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _noteService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления заметки, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
