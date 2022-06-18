using AutoMapper;
using Ninject;
using Ninject.Parameters;
using UTask.Backend.Common.Base.Contexts;
using UTask.Backend.Domain.Entities.Categories;
using UTask.Backend.Domain.Ninject;
using UTask.Backend.Domain.Services.Implementations.BaseImplementations;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb
{
    /// <summary>
    /// Сервис работы с категориями
    /// </summary>
    public class CategoryService : BaseService, ICategoryService
    {
        #region Контексты

        private readonly UTaskContext _utaskContext;

        #endregion

        #region Репозитории

        private readonly IEntityWithIdRepository<CategoryDao, long> _categoryRepository;

        #endregion

        #region Мапперы

        private readonly IMapper _mapper;

        #endregion

        /// <summary>
        /// Сервис работы с категориями
        /// </summary>
        public CategoryService()
        {
            #region Получаем экземпляры NinjectModule

            IKernel kernel = new StandardKernel(new UTaskModule());

            #endregion

            #region Получаем экземпляры EF контекстов

            _utaskContext = kernel.Get<UTaskContext>();

            #endregion

            #region Получаем экземпляры требуемых репозиториев

            _categoryRepository = kernel.Get<IEntityWithIdRepository<CategoryDao, long>>(new ConstructorArgument("context", _utaskContext));

            #endregion

            #region Получаем экземпляр маппера

            _mapper = kernel.Get<IMapper>();

            #endregion
        }

        /// <summary>
        /// Создаёт новую категорию
        /// </summary>
        /// <param name="category">Новая категория</param>
        /// <returns>Созданная категория</returns>
        public Category Create(Category category)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(category.Name))
                {
                    if (ServerContext.UserId > 0)
                    {
                        var similarCategoryDao = _categoryRepository.GetQueryable()
                            .Where(x => x.UserId == ServerContext.UserId)
                            .FirstOrDefault(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                        if (similarCategoryDao == null)
                        {
                            var categoryDao = new CategoryDao
                            {
                                UserId = ServerContext.UserId,
                                Name = category.Name,
                                Created = DateTime.Now
                            };
                            categoryDao = _categoryRepository.Create(categoryDao);
                            _utaskContext.SaveChanges();
                            return _mapper.Map<Category>(categoryDao);
                        }
                        else
                        {
                            similarCategoryDao.Created = DateTime.Now;
                            _categoryRepository.Update(similarCategoryDao);
                            _utaskContext.SaveChanges();
                            return _mapper.Map<Category>(similarCategoryDao);
                        }
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя меньше или равен 0");
                    }
                }
                else
                {
                    throw new Exception($"Передано пустое наименование");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Возвращает интерфейс для запроса категорий
        /// </summary>
        /// <returns>Интерфейс для запроса категорий</returns>
        public IQueryable<Category> GetQueryable()
        {
            try
            {
                if (ServerContext.UserId > 0)
                {
                    var categoryQueryable = _categoryRepository.GetQueryable()
                        .Where(x => x.UserId == ServerContext.UserId);
                    return _mapper.ProjectTo<Category>(categoryQueryable);
                }
                else
                {
                    throw new Exception($"Идентификатор пользователя меньше или равен 0");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Обновляет данные категории
        /// </summary>
        /// <param name="category">Измененная категория</param>
        public void Update(Category category)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(category.Name))
                {
                    var categoryDao = _categoryRepository.GetById(category.Id);
                    if (categoryDao != null)
                    {
                        if (ServerContext.UserId == categoryDao.UserId)
                        {
                            var similarCategoryDao = _categoryRepository.GetQueryable()
                                .Where(x => x.UserId == ServerContext.UserId)
                                .FirstOrDefault(x => x.Name.ToLower().Equals(category.Name.ToLower()));
                            if (similarCategoryDao == null)
                            {
                                categoryDao.Name = category.Name;
                                _categoryRepository.Update(categoryDao);
                                _utaskContext.SaveChanges();
                            }
                            else
                            {
                                _categoryRepository.Delete(categoryDao);
                                similarCategoryDao.Created = DateTime.Now;
                                _categoryRepository.Update(similarCategoryDao);
                                _utaskContext.SaveChanges();
                            }
                        }
                        else
                        {
                            throw new Exception($"Идентификатор пользователя не совпадает");
                        }
                    }
                    else
                    {
                        throw new Exception($"Категория не найдена");
                    }
                }
                else
                {
                    throw new Exception($"Передано пустое наименование");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }

        /// <summary>
        /// Удаляет категорию по переданному идентификатору
        /// </summary>
        /// <param name="id">Идентификатор категории</param>
        public void Delete(long id)
        {
            try
            {
                var categoryDao = _categoryRepository.GetById(id);
                if (categoryDao != null)
                {
                    if (ServerContext.UserId == categoryDao.UserId)
                    {
                        _categoryRepository.Delete(categoryDao);
                        _utaskContext.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"Идентификатор пользователя не совпадает");
                    }
                }
                else
                {
                    throw new Exception($"Категория не найдена");
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Exception: {e.Message} InnerException: {e.InnerException}");
            }
        }
    }
}
