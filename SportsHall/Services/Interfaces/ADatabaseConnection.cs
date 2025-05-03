using Microsoft.EntityFrameworkCore;
using SportsHall.Models.Entities;

public abstract class ADatabaseConnection : DbContext
{
    protected abstract string GetConnectionString();

    private readonly string _connectionString = null!;

    public string ConnectionString => _connectionString;

    public DbSet<Users> Users => Set<Users>();

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
}