using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SportsHall.Services.Interfaces;

namespace SportsHall.Services
{
    public class MssqlConnection : ADatabaseConnection
    {
        private const string DATABASE_NAME = "messenger";

        protected override string GetConnectionString()
        {
            const string SERVER_VAR = "DATABASE_SERVER_NAME";
            const string PORT_VAR = "DATABASE_PORT";
            const string USER_VAR = "USER_NAME";
            const string PASS_VAR = "USER_PASSWORD";
            var server = Environment.GetEnvironmentVariable(SERVER_VAR) ?? "localhost";
            var port = Environment.GetEnvironmentVariable(PORT_VAR) ?? "5000";
            var user = Environment.GetEnvironmentVariable(USER_VAR) ?? "sa";
            var password = Environment.GetEnvironmentVariable(PASS_VAR) ?? "fakePassw0rd";

            if (!string.IsNullOrEmpty(server) && !string.IsNullOrEmpty(port) &&
                !string.IsNullOrEmpty(user) && !string.IsNullOrEmpty(password))
            {
                return $"Server={server},{port};Database={DATABASE_NAME};" +
                       $"User Id={user};Password={password};Encrypt=False;TrustServerCertificate=True;";
            }

            throw new InvalidOperationException("Не заданы переменные окружения для подключения к MSSQL.");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        public MssqlConnection()
        {
            if (Debugger.IsAttached)
            {
                Console.WriteLine($"Подключение к MSSQL: {ConnectionString}");
            }
        }
    }
}