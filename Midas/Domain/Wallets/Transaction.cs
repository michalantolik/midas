using Domain.Common;
using System;

namespace Domain.Wallets
{
    /// <summary>
    /// DB entity class for a transaction made on a multi-currency wallet.
    /// </summary>
    public class Transaction : IEntity
    {
        public int Id { get; set; }

        public Wallet Wallet { get; set; }

        public TransactionType TransactionType { get; set; }

        public string CurrencyCode { get; set; }

        /// <summary>
        /// Transaction amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// For conversion transactions, the target currency code
        /// </summary>
        public string? TargetCurrencyCode { get; set; }

        /// <summary>
        /// For conversion transactions, the converted amount
        /// </summary>
        public decimal? TargetAmount { get; set; }

        /// <summary>
        /// Timestamp of the transaction
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }
    }
}
