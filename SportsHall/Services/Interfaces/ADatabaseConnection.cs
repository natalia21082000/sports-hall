using Microsoft.EntityFrameworkCore;
using SportsHall.Models.Entities;

namespace SportsHall.Services.Interfaces
{
    public abstract class ADatabaseConnection : DbContext
    {
        protected abstract string ReturnConnectionString();

        protected string ConnectionString { get; private set; }

        public DbSet<Users> Users => Set<Users>();

        public ADatabaseConnection()
        {
            this.ConnectionString = this.ReturnConnectionString();
            this.Database.EnsureCreated();
        }
    }
}