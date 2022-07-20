using Entities;
using MyAuth.Repositories;
using MyAuth_lib;
using MyAuth_lib.Auth_Server;
using MyLogger;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMyAuthServer<IdentityRepository, DefaultAuthServerSupplier>();
builder.Services.AddMyTodoContext(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Host.UseSerilog();

builder.Services.AddMyLogger();

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
