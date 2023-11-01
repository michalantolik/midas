using Application.Interfaces;
using Domain.ExchangeRates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.ExchangeRates;

namespace Persistence
{
    /// <inheritdoc />
    public class DatabaseService : DbContext, IDatabaseService
    {
        private const string SqlDbConnectionStringName = "MidasRatesSqlConnectionString";

        private readonly IConfiguration _configuration;

        public DatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
            Database.EnsureCreated();
        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        /// <inheritdoc />
        public void Save()
        {
            SaveChanges();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString(SqlDbConnectionStringName);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            new ExchangeRateConfiguration().Configure(builder.Entity<ExchangeRate>());
        }
    }
}
