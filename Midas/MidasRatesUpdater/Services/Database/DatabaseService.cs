using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace MidasRatesUpdater.Services.Database
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
    }
}
