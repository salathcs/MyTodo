using Entities;
using IntegrationTests.MyTodo_Todos;
using IntegrationTests.MyTodo_Todos.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Net.Http.Headers;
using static IntegrationTests.MyTodo_Todos.Helpers.RequestDataHelper;
using static IntegrationTests.MyTodo_Todos.Helpers.ResponseDataHelper;

namespace IntegrationTests.Tests
{
    public class Todos_AdminTests
        : IClassFixture<MyWebApplicationFactory<AdminMockedReqHandler>>
    {
        private readonly MyWebApplicationFactory<AdminMockedReqHandler> factory;

        public Todos_AdminTests(MyWebApplicationFactory<AdminMockedReqHandler> factory)
        {
            this.factory = factory;
        }

        #region GetAll
        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task GetAll_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task GetAll_ResultCountCheck(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var expectedTodos = db.Todos.ToList();

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url);
            var result = await GetTodosFromResponse(response);

            // Assert
            Assert.Equal(expectedTodos.Count(), result.Count());
        }
        #endregion GetAll

        #region GetById
        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task GetById_EndpointsReturnSuccess(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            long todoId = db.Todos.First().Id;

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url + todoId);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task GetById_ResultNotFound(string url)
        {
            // Arrange
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url + 0);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task GetById_ResultCheck(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var expectedTodo = db.Todos.First();
            long todoId = expectedTodo.Id;

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.GetAsync(url + todoId);
            var result = await GetTodoFromResponse(response);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedTodo.Id, result.Id);
            Assert.Equal(expectedTodo.UserId, result.UserId);
            Assert.Equal(expectedTodo.Title, result.Title);
        }
        #endregion GetById

        #region Create
        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Create_EndpointsReturnSuccess(string url)
        {
            // Arrange
            var postParam = CreateHttpContent(CreateTodoDto(0, 0));

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PostAsync(url, postParam);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Create_LocationHeader(string url)
        {
            // Arrange
            var postParam = CreateHttpContent(CreateTodoDto(0, 0));

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PostAsync(url, postParam);
            response.Headers.TryGetValues("Location", out var location);

            // Assert
            Assert.NotNull(location);
            Assert.NotEmpty(location);
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Create_ResultCheck(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var newTodo = CreateTodoDto(0, 0);
            var postParam = CreateHttpContent(newTodo);

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PostAsync(url, postParam);
            var result = await GetTodoFromResponse(response);

            // Assert
            Assert.NotNull(result);
            var todoByResult = db.Todos.First(x => x.Id == result.Id);
            Assert.Equal(newTodo.Title, todoByResult.Title);
        }
        #endregion Create

        #region Update
        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Update_EndpointsReturnSuccess(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var existingTodo = db.Todos.First();
            var putParam = CreateHttpContent(CreateTodoDto(existingTodo.Id, 0));

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PutAsync(url, putParam);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Update_NotFound(string url)
        {
            // Arrange
            var putParam = CreateHttpContent(CreateTodoDto(0, 0));

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PutAsync(url, putParam);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Update_ResultCheck(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var existingTodo = db.Todos.First();
            var modificationTodo = CreateTodoDto(existingTodo.Id, 0);
            modificationTodo.Title += "postfix";
            var putParam = CreateHttpContent(modificationTodo);

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.PutAsync(url, putParam);

            // Assert
            using var scopeAfterResponse = factory.Services.CreateScope();
            var dbAfterResponse = scopeAfterResponse.ServiceProvider.GetRequiredService<MyTodoContext>();
            var todoById = dbAfterResponse.Todos.First(x => x.Id == existingTodo.Id);
            Assert.Equal(modificationTodo.Title, todoById.Title);
        }
        #endregion Update

        #region Delete
        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Delete_EndpointsReturnSuccess(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var todoId = db.Todos.First().Id;

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.DeleteAsync(url + todoId);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Delete_NotFound(string url)
        {
            // Arrange
            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.DeleteAsync(url + 0);

            // Assert
            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Theory]
        [InlineData("/api/todos/crud/")]
        public async Task Delete_ResultCheck(string url)
        {
            // Arrange
            using var scope = factory.Services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<MyTodoContext>();
            var todoId = db.Todos.First().Id;

            var client = factory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");

            // Act
            var response = await client.DeleteAsync(url + todoId);

            // Assert
            using var scopeAfterResponse = factory.Services.CreateScope();
            var dbAfterResponse = scopeAfterResponse.ServiceProvider.GetRequiredService<MyTodoContext>();
            var todoById = dbAfterResponse.Todos.FirstOrDefault(x => x.Id == todoId);
            Assert.Null(todoById);
        }
        #endregion Delete
    }
}
