using DataTransfer.DataTransferObjects;
using System.Text;
using System.Text.Json;

namespace IntegrationTests.MyTodo_Todos.Helpers
{
    public static class RequestDataHelper
    {
        public static TodoDto CreateTodoDto(long id, long userId)
        {
            return new TodoDto
            {
                Id = id,
                Title = "Test title",
                Description = "Test desc",
                Expiration = DateTime.UtcNow,
                UserId = userId
            };
        }

        public static HttpContent CreateHttpContent<T>(T param)
        {
            var jsonString = JsonSerializer.Serialize(param);

            return new StringContent(jsonString, Encoding.UTF8, "application/json");
        }
    }
}
