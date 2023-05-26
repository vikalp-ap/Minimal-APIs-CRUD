using System;
using CRUDwithMinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CRUDwithMinimalAPI
{
    public class AppDbContext : DbContext
    {
        private const string ConnectionString = "Server=localhost;Database=mydatabase;Port=5432;User Id=appperfect";

        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(ConnectionString);
        }
    }
}
