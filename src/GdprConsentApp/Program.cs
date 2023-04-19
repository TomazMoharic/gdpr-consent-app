using GdprConsentApp.Interfaces;
using GdprConsentApp.Services;
using Serilog;
using System;
using GdprConsentApp.Database;
using GdprConsentApp.Logging;
using GdprConsentApp.Middleware;
using GdprConsentApp.Repository;
using GdprConsentApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(Path.Combine("bin", "Debug", "GdprConsentApp.xml"));
});

// Mysql DB
builder.Services.AddDbContext<MysqlDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
});

builder.Services.AddTransient<IConsentsService, ConsentsService>();
builder.Services.AddTransient<IUsersService, UsersService>();
builder.Services.AddTransient<IUsersConsentsService, UserConsentsService>();
builder.Services.AddTransient<IUsersRepository, UsersRepository>();
builder.Services.AddTransient<IConsentsRepository, ConsentsRepository>();
builder.Services.AddTransient<IUsersConsentsRepository, UsersConsentsRepository>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

builder.Services.AddScoped<DatabaseInitializer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<CustomExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// var databaseInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
//     
// databaseInitializer.InitializeDb();

app.Run();