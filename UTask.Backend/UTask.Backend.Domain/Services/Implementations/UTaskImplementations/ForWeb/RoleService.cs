using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Domain.Entities.Roles;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с ролями пользователей
    /// </summary>
    public class RoleService : BaseService, IRoleService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<RoleDao, int> _roleRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с ролями пользователей
        /// </summary>
        public RoleService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _roleRepository = kernel.Get<IEntityWithIdRepository<RoleDao, int>>(new ConstructorArgument("context", _utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Возвращает интерфейс для запроса ролей пользователей
        /// </summary>
        /// <returns>Интерфейс для запроса ролей пользователей</returns>
        public IQueryable<Role> GetQueryable()
        {
            var roleQueryable = _roleRepository.GetQueryable();
            return _mapper.ProjectTo<Role>(roleQueryable);
        }
    }
}
