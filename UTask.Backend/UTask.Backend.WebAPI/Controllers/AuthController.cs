﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ninject;
using UTask.Backend.Common.Base.Extensions;
using UTask.Backend.Domain.Entities.Auth;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.WebAPI.Attributes;

namespace UTask.Backend.WebAPI.Controllers
{
    /// <summary>
    /// Web Api контроллер аутентификации
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        #region Логгер

        private readonly ILogger<AuthController> _logger;

        #endregion

        #region Сервисы

        private readonly IAuthService _authService;

        #endregion

        /// <summary>
        /// Web Api контроллер аутентификации
        /// </summary>
        public AuthController(ILogger<AuthController> logger)
        {
            _logger = logger;

            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _authService = kernel.Get<IAuthService>();

            #endregion
        }

        [HttpPost("Login")]
        public ActionResult AuthByСredentials(AuthModel authModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var authResult = _authService.AuthByСredentials(authModel);
                if (authResult.IsSuccess)
                {
                    return Ok(authResult);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, authResult);
                }
            }
            catch (BaseException e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при аутентификации по данным");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }

        [HttpGet("LoginByToken")]
        [Authorize, UserIdentification]
        public IActionResult AuthByToken()
        {
            try
            {
                var authResult = _authService.AuthByToken();
                if (authResult.IsSuccess)
                {
                    return Ok(authResult);
                }
                else
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, authResult);
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Произошла ошибка при аутентификации по токену");
                return StatusCode(StatusCodes.Status500InternalServerError, e);
            }
        }
    }
}
