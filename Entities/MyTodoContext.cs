using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class MyTodoContext : DbContext
    {
        public MyTodoContext(DbContextOptions<MyTodoContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Identity> Identites { get; set; }

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
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<Identity>().ToTable("identity");

            //Data
            var adminIdentity = new Identity { Id = 1, UserName = "admin", Password = "admin" };
            modelBuilder.Entity<Identity>().HasData(adminIdentity);
            modelBuilder.Entity<User>().HasData(new User 
            { 
                Id = 1, 
                Name = "MyAdmin", 
                Email = "MyAdmin@tmp.com", 
                Created = DateTime.UtcNow, 
                Updated = DateTime.UtcNow, 
                CreatedBy = "System", 
                UpdatedBy = "System",
                IdentityId = adminIdentity.Id
            });
        }
    }
}
