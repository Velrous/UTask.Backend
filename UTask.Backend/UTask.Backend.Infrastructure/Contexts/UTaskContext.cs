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

            modelBuilder.Entity<GoalDao>().ToTable("Goals", "dbo");
            modelBuilder.Entity<GoalTaskRelationDao>().ToTable("GoalTaskRelations", "dbo");
            modelBuilder.Entity<GroupDao>().ToTable("Groups", "dbo");
            modelBuilder.Entity<NoteDao>().ToTable("Notes", "dbo");
            modelBuilder.Entity<RoleDao>().ToTable("Roles", "dbo");
            modelBuilder.Entity<TaskDao>().ToTable("Tasks", "dbo");
            modelBuilder.Entity<TaskGroupRelationDao>().ToTable("TaskGroupRelations", "dbo");
            modelBuilder.Entity<TaskNotificationDao>().ToTable("TaskNotifications", "dbo");
            modelBuilder.Entity<TaskTypeDao>().ToTable("TaskTypes", "dbo");
            modelBuilder.Entity<UserCodeDao>().ToTable("UserCodes", "dbo");
            modelBuilder.Entity<UserDao>().ToTable("Users", "dbo");
            modelBuilder.Entity<UserGoalRelationDao>().ToTable("UserGoalRelations", "dbo");
            modelBuilder.Entity<UserGroupRelationDao>().ToTable("UserGroupRelations", "dbo");
            modelBuilder.Entity<UserRoleRelationDao>().ToTable("UserRoleRelations", "dbo");
            modelBuilder.Entity<UserSettingDao>().ToTable("UserSettings", "dbo");

            #endregion

            #region Указываем первичные ключи

            #region GoalDao

            modelBuilder.Entity<GoalDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region GoalTaskRelationDao

            modelBuilder.Entity<GoalTaskRelationDao>()
                .HasKey(k=> new {k.GoalId, k.TaskId});

            #endregion

            #region GroupDao

            modelBuilder.Entity<GroupDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region NoteDao

            modelBuilder.Entity<NoteDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region RoleDao

            modelBuilder.Entity<RoleDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region TaskDao

            modelBuilder.Entity<TaskDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region TaskGroupRelationDao

            modelBuilder.Entity<TaskGroupRelationDao>()
                .HasKey(k => new {k.TaskId, k.GroupId});

            #endregion

            #region TaskNotificationDao

            modelBuilder.Entity<TaskNotificationDao>()
                .HasKey(k => k.TaskId);

            #endregion

            #region TaskTypeDao

            modelBuilder.Entity<TaskTypeDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region UserCodeDao

            modelBuilder.Entity<UserCodeDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region UserDao

            modelBuilder.Entity<UserDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #region UserGoalRelationDao

            modelBuilder.Entity<UserGoalRelationDao>()
                .HasKey(k => new {k.UserId, k.GoalId});

            #endregion

            #region UserGroupRelationDao

            modelBuilder.Entity<UserGroupRelationDao>()
                .HasKey(k => new { k.UserId, k.GroupId });

            #endregion

            #region UserRoleRelationDao

            modelBuilder.Entity<UserRoleRelationDao>()
                .HasKey(k => new { k.UserId, k.RoleId });

            #endregion

            #region UserSettingDao

            modelBuilder.Entity<UserSettingDao>()
                .Property(p => p.Id)
                .ValueGeneratedOnAdd();

            #endregion

            #endregion

            #region Указываем внешние ключи

            #region GoalTaskRelationDao

            modelBuilder.Entity<GoalTaskRelationDao>()
                .HasOne(x => x.Goal)
                .WithMany()
                .HasForeignKey(x => x.GoalId);

            modelBuilder.Entity<GoalTaskRelationDao>()
                .HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId);

            #endregion

            #region NoteDao

            modelBuilder.Entity<NoteDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            #endregion

            #region TaskDao

            modelBuilder.Entity<TaskDao>()
                .HasOne(x => x.TaskType)
                .WithMany()
                .HasForeignKey(x => x.TaskTypeId);

            #endregion

            #region TaskGroupRelationDao

            modelBuilder.Entity<TaskGroupRelationDao>()
                .HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId);

            modelBuilder.Entity<TaskGroupRelationDao>()
                .HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId);

            #endregion

            #region TaskNotificationDao

            modelBuilder.Entity<TaskNotificationDao>()
                .HasOne(x => x.Task)
                .WithMany()
                .HasForeignKey(x => x.TaskId);

            #endregion

            #region UserCodeDao

            modelBuilder.Entity<UserCodeDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            #endregion

            #region UserGoalRelationDao

            modelBuilder.Entity<UserGoalRelationDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserGoalRelationDao>()
                .HasOne(x => x.Goal)
                .WithMany()
                .HasForeignKey(x => x.GoalId);

            #endregion

            #region UserGroupRelationDao

            modelBuilder.Entity<UserGroupRelationDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserGroupRelationDao>()
                .HasOne(x => x.Group)
                .WithMany()
                .HasForeignKey(x => x.GroupId);

            #endregion

            #region UserRoleRelationDao

            modelBuilder.Entity<UserRoleRelationDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<UserRoleRelationDao>()
                .HasOne(x => x.Role)
                .WithMany()
                .HasForeignKey(x => x.RoleId);

            #endregion

            #region UserSettingDao

            modelBuilder.Entity<UserSettingDao>()
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId);

            #endregion

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
