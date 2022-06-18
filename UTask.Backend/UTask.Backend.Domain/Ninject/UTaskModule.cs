using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Ninject;
using Ninject.Modules;
using UTask.Backend.Domain.Options;
using UTask.Backend.Domain.Services.Implementations.UTaskImplementations.ForWeb;
using UTask.Backend.Domain.Services.Interfaces.UTaskInterfaces.ForWeb;
using UTask.Backend.Infrastructure.Contexts;
using UTask.Backend.Infrastructure.Repositories.Implementations.BaseImplementations;
using UTask.Backend.Infrastructure.Repositories.Interfaces.BaseInterfaces;

namespace UTask.Backend.Domain.Ninject
{
    /// <summary>
    /// Модуль, определяющий привязки для системы
    /// </summary>
    public class UTaskModule : NinjectModule
    {
        /// <summary>
        /// Модуль, определяющий привязки для системы
        /// </summary>
        public UTaskModule()
        {
        }

        public override void Load()
        {
            AddContextBindings();
            AddRepositoryBindings();
            AddServiceBindings();

            var mapperConfiguration = CreateConfiguration();
            Bind<MapperConfiguration>().ToConstant(mapperConfiguration).InSingletonScope();
            Bind<IMapper>().ToMethod(ctx =>
                new Mapper(mapperConfiguration, type => ctx.Kernel.Get(type)));
        }

        /// <summary>
        /// Добавляем связи контекстов
        /// </summary>
        public void AddContextBindings()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var utaskConnectionString = configuration.GetConnectionString("UTaskConnectionString");

            var utaskOptionsBuilder = new DbContextOptionsBuilder<UTaskContext>();
            var utaskOptions = utaskOptionsBuilder
                .UseNpgsql(utaskConnectionString)
                .Options;

            Bind<UTaskContext>().ToSelf().InTransientScope()
                .WithConstructorArgument("options", utaskOptions);

            var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();
            Bind<JwtOptions>().ToSelf().InTransientScope().WithConstructorArgument("options", jwtOptions);
        }

        /// <summary>
        /// Добавляем связи реализаций репозиториев и их интерфейсов EF контекста
        /// </summary>
        private void AddRepositoryBindings()
        {
            Bind(typeof(IEntityRepository<>)).To(typeof(EntityRepository<>));
            Bind(typeof(IEntityWithIdRepository<,>)).To(typeof(EntityWithIdRepository<,>));
            Bind(typeof(IReadOnlyEntityRepository<>)).To(typeof(ReadOnlyEntityRepository<>));
            Bind(typeof(IReadOnlyEntityWithIdRepository<,>)).To(typeof(ReadOnlyEntityWithIdRepository<,>));
        }

        /// <summary>
        /// Добавляем связи реализаций сервисов и их интерфейсов
        /// </summary>
        public void AddServiceBindings()
        {
            Bind<IAuthService>().To<AuthService>();
            Bind<ICategoryService>().To<CategoryService>();
            Bind<IGoalService>().To<GoalService>();
            Bind<INoteService>().To<NoteService>();
            Bind<IPlanPriorityService>().To<PlanPriorityService>();
            Bind<IPlanService>().To<PlanService>();
            Bind<IPlanViewService>().To<PlanViewService>();
            Bind<ITaskService>().To<TaskService>();
            Bind<ITaskTypeService>().To<TaskTypeService>();
            Bind<IUserService>().To<UserService>();

            Bind<IGoalViewService>().To<GoalViewService>();
            Bind<ITaskViewService>().To<TaskViewService>();
        }

        /// <summary>
        /// Создаём конфигурацию для AutoMapper
        /// </summary>
        /// <returns></returns>
        private MapperConfiguration CreateConfiguration()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(GetType().Assembly);
            });
            config.AssertConfigurationIsValid();

            return config;
        }
    }
}
