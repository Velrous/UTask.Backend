using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UTask.Backend.Infrastructure.Entities.UTaskImplementations;

namespace UTask.Backend.Infrastructure.Contexts
{
    /// <summary>
    /// EF DbContext для подключения к БД UTask
    /// </summary>
    public class UTaskContext : BaseDbContext
    {
        /// <summary>
        /// EF DbContext для подключения к БД UTask
        /// </summary>
        public UTaskContext(DbContextOptions options) : base(options)
        {
            //Database.CommandTimeout = 60 * 60;
            //Database.SetCommandTimeout((int)TimeSpan.FromMinutes(15).TotalSeconds);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Указываем связанные с сущностями таблицы в БД

            modelBuilder.Entity<RoleDao>().ToTable("Roles", "dbo");

            #endregion

            #region Указываем первичные ключи

            #region RoleDao

            modelBuilder.Entity<RoleDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #endregion

            #region Указываем внешние ключи

            #endregion

            base.OnModelCreating(modelBuilder);
        }

        public static readonly ILoggerFactory UTaskLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddDebug();
            //builder.AddConsole();
        });

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(UTaskLoggerFactory);
            if (optionsBuilder.IsConfigured == false)
            {
                base.OnConfiguring(optionsBuilder);
            }
        }
    }
}
