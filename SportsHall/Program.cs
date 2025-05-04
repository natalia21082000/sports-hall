using SportsHall.Services.Interfaces;
using SportsHall.Services;
using dotenv.net;
using SportsHall.Infrastructure.Settings;
using SportsHall.Infrastructure.Middlewares;
using FluentValidation.AspNetCore;
using FluentValidation;

DotEnv.Load();

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

// Добавляем FluentValidation
builder.Services.AddFluentValidationAutoValidation(); // Автоматическая валидация на стороне сервера
builder.Services.AddFluentValidationClientsideAdapters(); // Адаптеры для клиентской валидации (если нужна)

// Регистрация валидаторов из сборки, где находится Program
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSingleton<IAppSettings, EnvAppSettings>();
builder.Services.AddScoped<ADatabaseConnection, MssqlConnection>();
builder.Services.AddScoped<IUsersService, UsersService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITokenService, TokenService>();


// Получаем переменные из окружения
builder.Configuration.AddEnvironmentVariables();


var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseExceptionHandling();

app.UseAuthorization();

app.MapControllers();

app.Run();
