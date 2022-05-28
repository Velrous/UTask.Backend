﻿using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Common.Base.Helpers;
using UTask.Backend.Domain.Entities.Auth;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Options;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис аутентификации
    /// </summary>
    public class AuthService : BaseService, IAuthService
    {
        #region Репозитории

        private readonly IEntityWithIdRepository<UserDao, long> _userRepository;

        #endregion

        #region Настройки 

        private readonly JwtOptions _jwtOptions;

        #endregion

        /// <summary>
        /// Сервис аутентификации
        /// </summary>
        public AuthService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            var uTaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _userRepository = kernel.Get<IEntityWithIdRepository<UserDao, long>>(new ConstructorArgument("context", uTaskContext));

            #endregion

            #region Получаем экземпляр маппера

            kernel.Get<IMapper>();

            #endregion

            #region Получаем экземпляр настроек Jwt

            _jwtOptions = kernel.Get<JwtOptions>();

            #endregion
        }

        /// <summary>
        /// Производит аутентификацию пользователя для веб интерфейса
        /// </summary>
        /// <param name="authModel">Данные аутентификации</param>
        /// <returns>Результат аутентификации</returns>
        public AuthResultModel AuthByСredentials(AuthModel authModel)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(authModel.Login) && !string.IsNullOrWhiteSpace(authModel.Password))
                {
                    var userDao = _userRepository.GetQueryable()
                        .FirstOrDefault(x => x.Login.ToLower().Equals(authModel.Login.ToLower()));
                    if (userDao != null)
                    {
                        if (!string.IsNullOrWhiteSpace(userDao.PasswordHash) && !string.IsNullOrWhiteSpace(userDao.Salt))
                        {
                            var saltAndPassword = CreatePassword(userDao.Salt, authModel.Password);
                            if (saltAndPassword.passwordHash.Equals(userDao.PasswordHash))
                            {
                                var token = GenerateToken(userDao);
                                if (!string.IsNullOrWhiteSpace(token))
                                {
                                    return new AuthResultModel
                                    {
                                        DisplayName = userDao.DisplayName,
                                        Token = token,
                                        IsSuccess = true,
                                        ErrorText = string.Empty
                                    };
                                }
                                else
                                {
                                    return new AuthResultModel
                                    {
                                        IsSuccess = false,
                                        ErrorText = $"Произошла ошибка при попытке генерации токена"
                                    };
                                }
                            }
                            else
                            {
                                return new AuthResultModel
                                {
                                    IsSuccess = false,
                                    ErrorText = "Неправильный логин или пароль"
                                };
                            }
                        }
                        else
                        {
                            return new AuthResultModel
                            {
                                IsSuccess = false,
                                ErrorText = "Пользователь найден, но у него не задан пароль"
                            };
                        }
                    }
                    else
                    {
                        return new AuthResultModel
                        {
                            IsSuccess = false,
                            ErrorText = "Пользователь не найден"
                        };
                    }
                }
                else
                {
                    return new AuthResultModel
                    {
                        IsSuccess = false,
                        ErrorText = "Передан пустой логин или пароль"
                    };
                }
            }
            catch (Exception e)
            {
                return new AuthResultModel
                {
                    IsSuccess = false,
                    ErrorText = $"Произошла ошибка при попытке авторизации: {e.Message}"
                };
            }
        }

        /// <summary>
        /// Производит аутентификацию пользователя для веб интерфейса по полученному токену
        /// </summary>
        /// <returns>Результат аутентификации</returns>
        public AuthResultModel AuthByToken()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var userDao = _userRepository.GetById(ServerContext.UserId);
                    if (userDao is {IsActive: true})
                    {
                        return new AuthResultModel
                        {
                            DisplayName = userDao.DisplayName,
                            Token = string.Empty,
                            IsSuccess = true,
                            ErrorText = string.Empty
                        };
                    }
                    else
                    {
                        return new AuthResultModel
                        {
                            IsSuccess = false,
                            ErrorText = "Пользователь не найден или неактивен"
                        };
                    }
                }
                else
                {
                    return new AuthResultModel
                    {
                        IsSuccess = false,
                        ErrorText = "Некорректный идентификатор пользователя"
                    };
                }
            }
            catch (Exception e)
            {
                return new AuthResultModel
                {
                    IsSuccess = false,
                    ErrorText = $"Произошла ошибка при попытке авторизации: {e.Message}"
                };
            }
        }

        #region Private

        /// <summary>
        /// Создаёт захешированную пару пароль-соль по предоставленному паролю
        /// </summary>
        /// <param name="salt">Соль</param>
        /// <param name="password">Пароль</param>
        /// <returns>Соль и хэш пароля</returns>
        private (string salt, string passwordHash) CreatePassword(string salt, string? password)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                salt = Md5Helper.CalculateMd5Hash(Guid.NewGuid().ToString()).Substring(0,16);
            }
            var passwordHash = string.Empty; //Скрыто для общедоступного репозитория
            return (salt, passwordHash);
        }

        /// <summary>
        /// Генерирует токен
        /// </summary>
        /// <param name="userDao">Данные пользователя</param>
        /// <returns>Токен</returns>
        private string GenerateToken(UserDao userDao)
        {
            if (!string.IsNullOrWhiteSpace(_jwtOptions.Key) && 
                !string.IsNullOrWhiteSpace(_jwtOptions.Audience) && 
                !string.IsNullOrWhiteSpace(_jwtOptions.Issuer))
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
                var claims = new[]
                {
                    new Claim(ClaimTypes.UserData, userDao.Id.ToString()),
                    new Claim(ClaimTypes.Name, userDao.DisplayName),
                    //new Claim(ClaimTypes.Role, userDao.RoleId.ToString())
                };
                var token = new JwtSecurityToken(_jwtOptions.Issuer, _jwtOptions.Audience, claims,
                    expires: DateTime.Now.AddHours(_jwtOptions.ExpirationTime),
                    signingCredentials: credentials);

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            return string.Empty;
        }

        #endregion
    }
}