using Domain.ExchangeRates;
using Domain.Wallets;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
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
        /// DB table with wallets
        /// </summary>
        DbSet<Wallet> Wallets { get; set; }

        /// <summary>
        /// DB table with balances
        /// </summary>
        DbSet<Balance> Balances { get; set; }

        /// <summary>
        /// DB table with transactions
        /// </summary>
        DbSet<Transaction> Trasactions { get; set; }

        /// <summary>
        /// Persists database changes.
        /// </summary>
        void Save();
    }
}
