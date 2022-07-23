using DataTransfer;
using Entities;
using MyAuth_lib;
using MyAuth_lib.Auth_Client;
using MyLogger;
using MyTodo_Users.Interfaces;
using MyTodo_Users.Repositories;
using MyTodo_Users.Services;
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

// Api logic DI
builder.Services.AddScoped<ICrudService, CrudService>();
builder.Services.AddScoped<ICrudRepository, CrudRepository>();

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
