using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class MyTodoContext : DbContext
    {
        public MyTodoContext(DbContextOptions<MyTodoContext> options) : base(options)
        {
        }

        public DbSet<Identity> Identites { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<UserPermission> UserPermissions { get; set; }
        public DbSet<Todo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // For migrations
#if DEBUG
            optionsBuilder.UseSqlServer("Server=CSABA-GAMING\\SQLEXPRESS;Database=MyTodo;Trusted_Connection=True");
#endif
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
            modelBuilder.Entity<Identity>().HasData(adminIdentity);

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
            modelBuilder.Entity<User>().HasData(admin);

            //Permission config
            var adminPermission = new Permission
            {
                Id = 1,
                PermissionName = "AdminPermission",
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
            });

            //Todos config
        }
    }
}
