using Microsoft.EntityFrameworkCore;
using MidasRatesUpdater.Services.Database.Entities;

namespace MidasRatesUpdater.Services.Database
{
    /// <summary>
    /// Service for accessing database.
    /// </summary>
    public interface IDatabaseService : IDisposable
    {
        /// <summary>
        /// DB table with currency exchange rates.
        /// </summary>
        DbSet<ExchangeRate> ExchangeRates { get; set; }

        /// <summary>
        /// Persists database changes.
        /// </summary>
        void Save();
    }
}
