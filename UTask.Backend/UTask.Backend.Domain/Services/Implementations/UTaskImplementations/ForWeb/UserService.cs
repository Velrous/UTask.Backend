using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Common.Base.Helpers;
using UTask.Backend.Domain.Entities.Users;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с пользователями
    /// </summary>
    public class UserService : BaseService, IUserService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<UserDao, long> _userRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с пользователями
        /// </summary>
        public UserService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _userRepository = kernel.Get<IEntityWithIdRepository<UserDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает текущего пользователя для веб-интерфейса
        /// </summary>
        /// <returns>Пользователь для отображения на клиенте</returns>
        public UserForWeb GetCurrentUserForWeb()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var userDao = _userRepository.GetById(ServerContext.UserId);
                    if (userDao != null)
                    {
                        return _mapper.Map<UserForWeb>(userDao);
                    }
                    else
                    {
                        throw new Exception($"Пользователь не найден");
                    }
                }
                else
                {
                    throw new Exception($"Некорректный идентификатор пользователя");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Возвращает интерфейс для запроса к пользователям для отображения на клиенте
        /// </summary>
        /// <returns>Интерфейс для запроса к пользователям для отображения на клиенте</returns>
        public IQueryable<UserForWeb> GetQueryableForWeb()
        {
            var userQueryable = _userRepository.GetQueryable().Where(x=>x.IsActive);
            return _mapper.ProjectTo<UserForWeb>(userQueryable);
        }

        /// <summary>
        /// Обновляет данные пользователя
        /// <param name="userForWeb">Пользователь для отображения на клиенте</param>
        /// </summary>
        public void Update(UserForWeb userForWeb)
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    if (ServerContext.UserId == userForWeb.Id)
                    {
                        if (!string.IsNullOrWhiteSpace(userForWeb.DisplayName))
                        {
                            if (!string.IsNullOrWhiteSpace(userForWeb.Email))
                            {
                                var userDao = _userRepository.GetById(userForWeb.Id);
                                if (userDao != null)
                                {
                                    var similarUserDao = _userRepository.GetQueryable()
                                        .Where(x => x.Id != userDao.Id)
                                        .FirstOrDefault(x => x.Email.ToLower() == userForWeb.Email.ToLower());
                                    if (similarUserDao == null)
                                    {
                                        userDao.DisplayName = userForWeb.DisplayName;
                                        userDao.Email = userForWeb.Email;
                                        _userRepository.Update(userDao);
                                        _utaskContext.SaveChanges();
                                        if (!string.IsNullOrWhiteSpace(userForWeb.OldPassword))
                                        {
                                            if (!string.IsNullOrWhiteSpace(userForWeb.NewPassword))
                                            {
                                                var saltAndPasswordHash = CreatePassword(userDao.Salt, userForWeb.OldPassword);
                                                if (!string.IsNullOrWhiteSpace(saltAndPasswordHash.salt) &&
                                                    !string.IsNullOrWhiteSpace(saltAndPasswordHash.passwordHash))
                                                {
                                                    if (userDao.PasswordHash.Equals(saltAndPasswordHash.passwordHash))
                                                    {
                                                        saltAndPasswordHash = CreatePassword(userDao.Salt, userForWeb.NewPassword);
                                                        if (!string.IsNullOrWhiteSpace(saltAndPasswordHash.salt) &&
                                                            !string.IsNullOrWhiteSpace(saltAndPasswordHash.passwordHash))
                                                        {
                                                            userDao.PasswordHash = saltAndPasswordHash.passwordHash;
                                                            _userRepository.Update(userDao);
                                                            _utaskContext.SaveChanges();
                                                        }
                                                        else
                                                        {
                                                            throw new Exception($"Ошибка при генерации нового пароля");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        throw new Exception($"Неверный старый пароль");
                                                    }
                                                }
                                                else
                                                {
                                                    throw new Exception($"Ошибка при генерации пароля");
                                                }
                                            }
                                            else
                                            {
                                                throw new Exception($"Передан пустой новый пароль");
                                            }
                                        }
                                    }
                                    else
                                    {
                                        throw new Exception($"Пользователь с таким email уже существует");
                                    }
                                }
                                else
                                {
                                    throw new Exception($"Пользователь не найден");
                                }
                            }
                            else
                            {
                                throw new Exception($"Передана пустая электронная почта");
                            }
                        }
                        else
                        {
                            throw new Exception($"Передано пустое имя пользователя");
                        }
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя не совпадает с пользователем выполняющим запрос");
                    }
                }
                else
                {
                    throw new Exception($"Некорректный идентификатор пользователя");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Создаёт захешированную пару пароль-соль по предоставленному паролю
        /// </summary>
        /// <param name="salt">Соль</param>
        /// <param name="password">Пароль</param>
        /// <returns>Соль и хэш пароля</returns>
        private (string salt, string passwordHash) CreatePassword(string salt, string password)
        {
            if (string.IsNullOrWhiteSpace(salt))
            {
                salt = Md5Helper.CalculateMd5Hash(Guid.NewGuid().ToString()).Substring(0, 16);
            }
            var passwordHash = string.Empty; //Скрыто для общедоступного репозитория
            return (salt, passwordHash);
        }
    }
}
