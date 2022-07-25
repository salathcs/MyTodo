using DataTransfer.DataTransferObjects;
using System.Text.Json;

namespace IntegrationTests.MyTodo_Todos.Helpers
{
    public static class ResponseDataHelper
    {
        private readonly static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public static async Task<IEnumerable<TodoDto>> GetTodosFromResponse(HttpResponseMessage? response)
        {
            if (response != null && response.Content != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var todos = JsonSerializer.Deserialize<IEnumerable<TodoDto>>(jsonString, jsonSerializerOptions);

                if (todos != null)
                {
                    return todos;
                }
            }

            return Enumerable.Empty<TodoDto>();
        }

        public static async Task<TodoDto?> GetTodoFromResponse(HttpResponseMessage? response)
        {
            if (response != null && response.Content != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var todo = JsonSerializer.Deserialize<TodoDto>(jsonString, jsonSerializerOptions);

                if (todo != null)
                {
                    return todo;
                }
            }

            return null;
        }
    }
}
