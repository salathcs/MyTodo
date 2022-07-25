using Entities.Models;
using Microsoft.EntityFrameworkCore;
using static Entities.Constants.PermissionNames;

namespace Entities
{
    public class MyTodoContext : DbContext
    {
        public MyTodoContext()
        { }
        public MyTodoContext(DbContextOptions<MyTodoContext> options) : base(options)
        { }

        public DbSet<Identity> Identites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // For migrations
            //optionsBuilder.UseSqlServer("Server=CSABA-GAMING\\SQLEXPRESS;Database=MyTodo;Trusted_Connection=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Tables
            modelBuilder.Entity<Identity>().ToTable("identities");
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Permission>().ToTable("permissions");
            modelBuilder.Entity<UserPermission>().ToTable("userPermissions");
            modelBuilder.Entity<Todo>().ToTable("todos");

            //Identity config
            modelBuilder.Entity<Identity>().HasIndex(x => x.UserName).IsUnique();

            var adminIdentity = new Identity { Id = 1, UserName = "admin", Password = "admin" };
            var emailWorkerIdentity = new Identity { Id = 2, UserName = "emailWorker", Password = "r1eH#emE295&" };
            modelBuilder.Entity<Identity>().HasData(adminIdentity, emailWorkerIdentity);

            //User config
            //one to one connection between identity and user
            modelBuilder.Entity<User>().HasOne(x => x.UserIdentity).WithOne(y => y.IdentityUser);

            var admin = new User
            {
                Id = 1,
                Name = "MyAdmin",
                Email = "MyAdmin@tmp.com",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedBy = "System",
                IdentityId = adminIdentity.Id
            };
            var emailWorker = new User
            {
                Id = 2,
                Name = "EmailWorker",
                Email = "MyAdmin@tmp.com",
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedBy = "System",
                IdentityId = emailWorkerIdentity.Id
            };
            modelBuilder.Entity<User>().HasData(admin, emailWorker);

            //Permission config
            modelBuilder.Entity<Permission>().HasIndex(x => x.PermissionName).IsUnique();

            var adminPermission = new Permission
            {
                Id = 1,
                PermissionName = ADMIN_PERMISSION,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
                CreatedBy = "System",
                UpdatedBy = "System",
            };
            modelBuilder.Entity<Permission>().HasData(adminPermission);

            //UserPermission config
            modelBuilder.Entity<UserPermission>().HasData(new UserPermission
            {
                Id = 1,
                UserId = admin.Id,
                PermissionId = adminPermission.Id
            },
            new UserPermission
            {
                Id = 2,
                UserId = emailWorker.Id,
                PermissionId = adminPermission.Id
            });

            //Todos config
        }
    }
}
