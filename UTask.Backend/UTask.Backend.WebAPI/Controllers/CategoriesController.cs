using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Categories;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с категориями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize, UserIdentification]
    public class CategoriesController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<CategoriesController> _logger;

        #endregion

        #region Сервисы

        private readonly ICategoryService _categoryService;

        #endregion

        #region Вспомогательные переменные

        private readonly string _errorText = "Произошла ошибка при попытке выполнения запроса. Повторите позже или обратитесь в поддержку.";

        #endregion

        /// <summary>
        /// Web API контроллер работы с категориями
        /// </summary>
        public CategoriesController(ILogger<CategoriesController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _categoryService = kernel.Get<ICategoryService>();

            #endregion
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                _categoryService.Create(category);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке создания категории, произошла ошибка: {e.Message}", ServerContext.UserId, category);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpGet, UseQuery]
        public ActionResult Get()
        {
            try
            {
                return Ok(_categoryService.GetQueryable());
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке получения категорий, произошла ошибка: {e.Message}", ServerContext.UserId);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpPut]
        public ActionResult Update(Category category)
        {
            try
            {
                _categoryService.Update(category);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке обновления категории, произошла ошибка: {e.Message}", ServerContext.UserId, category);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(long id)
        {
            try
            {
                _categoryService.Delete(id);
                return Ok();
            }
            catch (Exception e)
            {
                _logger.LogError($"При попытке удаления категории, произошла ошибка: {e.Message}", ServerContext.UserId, id);
                return StatusCode(StatusCodes.Status500InternalServerError, _errorText);
            }
        }
    }
}
