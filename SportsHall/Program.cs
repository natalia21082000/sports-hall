using SportsHall.Services.Interfaces;
using SportsHall.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ����������� ������������
builder.Services.AddScoped<IUsersService, UsersService>();
builder.Services.AddScoped<ADatabaseConnection, MssqlConnection>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
    .SetIsOriginAllowed(origin => true));

ADatabaseConnection connection = new MssqlConnection();

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
