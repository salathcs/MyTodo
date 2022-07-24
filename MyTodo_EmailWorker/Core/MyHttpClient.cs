using DataTransfer.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using MyAuth_lib.Auth_Server.Models;
using MyTodo_EmailWorker.Exceptions;
using MyTodo_EmailWorker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MyTodo_EmailWorker.Core
{
    internal class MyHttpClient : IMyHttpClient
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        private readonly JsonSerializerOptions jsonSerializerOptions;

        public MyHttpClient(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;

            jsonSerializerOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<IEnumerable<TodoWithEmailDto>> GetTodosByExpiration(int expiration)
        {
            using var client = httpClientFactory.CreateClient();

            //auth token
            var token = await GetToken(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //request
            var todosUrl = $"{configuration.GetValue<string>("TodosUrl")}{expiration}";
            var request = new HttpRequestMessage(HttpMethod.Get, todosUrl);
            var response = await client.SendAsync(request);

            //result
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                var todos = JsonSerializer.Deserialize<IEnumerable<TodoWithEmailDto>>(jsonString, jsonSerializerOptions);

                return todos ?? Enumerable.Empty<TodoWithEmailDto>();
            }

            throw new TodosRequestFailedException($"Todos request failed! Response status code: {response.StatusCode}");
        }

        public async Task SendTodoIds(IEnumerable<long> todoIds)
        {
            using var client = httpClientFactory.CreateClient();

            //auth token
            var token = await GetToken(client);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //request
            var jsonString = JsonSerializer.Serialize(todoIds);

            var todosUrl = configuration.GetValue<string>("UpdateEmailSentUrl");
            var request = new HttpRequestMessage(HttpMethod.Put, todosUrl);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            //result
            if (response.IsSuccessStatusCode)
            {
                return;
            }

            throw new TodosRequestFailedException($"Send Todo Ids request failed! Response status code: {response.StatusCode}");
        }

        private async Task<string> GetToken(HttpClient client)
        {
            //request
            var loginUrl = configuration.GetValue<string>("LoginUrl");

            var authRequest = new AuthRequest
            {
                UserName = configuration.GetValue<string>("LoginName"),
                Password = configuration.GetValue<string>("Password"),
            };
            var jsonString = JsonSerializer.Serialize(authRequest);

            var request = new HttpRequestMessage(HttpMethod.Post, loginUrl);
            request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var response = await client.SendAsync(request);

            //response
            if (response.StatusCode == HttpStatusCode.OK)
            {
                jsonString = await response.Content.ReadAsStringAsync();

                var authResult = JsonSerializer.Deserialize<AuthResult>(jsonString, jsonSerializerOptions);

                if (authResult!.Token is null)
                {
                    throw new AuthenticationFailedException("Auth result or token is null!");
                }

                return authResult.Token;
            }

            throw new AuthenticationFailedException($"Authentication failed! Response status code: {response.StatusCode}");
        }
    }
}
