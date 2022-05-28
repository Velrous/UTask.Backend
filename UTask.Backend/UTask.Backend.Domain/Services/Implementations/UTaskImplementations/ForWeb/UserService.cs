using AutoMapper;
using Ninject;
using Ninject.Parameters;
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

            var utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _userRepository = kernel.Get<IEntityWithIdRepository<UserDao, long>>(new ConstructorArgument("context", utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса пользователей для веб-интерфейса
        /// </summary>
        /// <returns>Интерфейс для запроса пользователей для веб-интерфейса</returns>
        public IQueryable<UserForWeb> GetQueryableForWeb()
        {
            var userQueryable = _userRepository.GetQueryable().Where(x=>x.IsActive);
            return _mapper.ProjectTo<UserForWeb>(userQueryable);
        }
    }
}
