using Microsoft.EntityFrameworkCore;
using SportsHall.Services.Interfaces;

namespace SportsHall.Services
{
    public class SqliteConnection : ADatabaseConnection
    {
        private const string _DATABASE_NAME = "production.db";

        protected override string ReturnConnectionString()
        {
            return $"Data Source={_DATABASE_NAME}";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.ConnectionString);
        }
    }
}