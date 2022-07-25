using Entities;
using Entities.Models;

namespace IntegrationTests.MyTodo_Todos.Db
{
    public static class DbInitializer
    {
        private static User bob;

        private readonly static DateTime dateTime = DateTime.UtcNow;

        private readonly static SemaphoreSlim semaphore = new SemaphoreSlim(1, 1);

        public static void InitializeDbForTests(MyTodoContext db)
        {
            semaphore.Wait();

            try
            {               

                bob = GetBob();
                db.Users.Add(bob);
                db.Todos.AddRange(GetTodos());

                db.SaveChanges();
            }
            finally
            {
                semaphore.Release();
            }
        }

        public static void ClearDbForTests(MyTodoContext db)
        {
            //TODO: remove data

            InitializeDbForTests(db);
        }

        public static User GetBob()
        {
            return new User
            {
                Name = "Bob HeyHo",
                Email = "Bob@tmp.com",
                UserIdentity = new Identity
                {
                    UserName = "Bob",
                    Password = "BobsPw"
                },

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
        }

        public static IEnumerable<Todo> GetTodos()
        {
            yield return new Todo
            {
                Title = "Todo1",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo2",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo3",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo4",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo5",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo6",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo7",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo8",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Todo9",
                UserId = 1,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };

            //Bob's todos
            yield return new Todo
            {
                Title = "Bob_Todo1",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo2",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo3",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo4",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo5",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo6",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo7",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo8",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
            yield return new Todo
            {
                Title = "Bob_Todo9",
                UserId = bob.Id,

                CreatedBy = "System",
                UpdatedBy = "System",
                Created = dateTime,
                Updated = dateTime
            };
        }
    }
}
