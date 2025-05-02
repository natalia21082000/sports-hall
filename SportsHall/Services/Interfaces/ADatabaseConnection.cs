using Microsoft.EntityFrameworkCore;
using SportsHall.Models.Entities;

namespace SportsHall.Services.Interfaces
{
    public abstract class ADatabaseConnection : DbContext
    {
        protected abstract string GetConnectionString();

        protected ADatabaseConnection() : base()
        {
            var connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                _connectionString = connectionString;
            }
            try
            {
                Database.EnsureCreated();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при создании БД: {ex.Message}");
            }
        }

        private readonly string _connectionString;

        protected string ConnectionString => _connectionString;

        public DbSet<Users> Users => Set<Users>();
    }
}