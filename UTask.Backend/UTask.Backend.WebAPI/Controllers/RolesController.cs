using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web API контроллер работы с ролями
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<RolesController> _logger;

        #endregion

        #region Сервисы

        private readonly IRoleService _roleService;

        #endregion

        /// <summary>
        /// Web API контроллер работы с ролями
        /// </summary>
        public RolesController(ILogger<RolesController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _roleService = kernel.Get<IRoleService>();

            #endregion
        }

        [HttpGet]
        [Authorize]
        public ActionResult Get()
        {
            try
            {
                var result = _roleService.GetQueryable();
                return Ok(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"При попытке получения ролей, произошла ошибка.");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
