using DataTransfer;
using Entities;
using MyAuth_lib;
using MyAuth_lib.Auth_Client;
using MyLogger;
using MyTodo_Todos.Interfaces;
using MyTodo_Todos.Repositories;
using MyTodo_Todos.Services;
using MyUtilities;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJwtAuth("MyTodo Users Api");

builder.Services.AddMyAuthClient<DefaultAuthClientSupplier>();
builder.Services.AddMyTodoContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Host.UseSerilog();

builder.Services.AddMyLogger();

builder.Services.AddMyAutoMapper();

builder.Services.AddMyUtilities();

// Api logic DI
builder.Services.AddScoped<ITodosService, TodosService>();
builder.Services.AddScoped<ITodosRepository, TodosRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
