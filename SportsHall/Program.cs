using SportsHall.Services.Interfaces;
using SportsHall.Services;
using dotenv.net;
using SportsHall.Infrastructure.Settings;

DotEnv.Load();

Console.WriteLine($"Загружены переменные из .env");

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Регистрация зависимостей
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

app.UseAuthorization();

app.MapControllers();

app.Run();
