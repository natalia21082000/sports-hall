using Microsoft.EntityFrameworkCore;
using SportsHall.Services.Interfaces;

namespace SportsHall.Services
{
    public class SqliteConnection : ADatabaseConnection
    {
        private const string DATABASE_NAME = "production.db";

        protected override string GetConnectionString()
        {
            return $"Data Source={DATABASE_NAME}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(ConnectionString);
        }
    }
}