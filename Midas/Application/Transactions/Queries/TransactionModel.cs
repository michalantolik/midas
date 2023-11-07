using Domain.Wallets;

namespace Application.Transactions.Queries
{
    public class TransactionModel
    {
        public int Id { get; set; }

        public int WalletId { get; set; }

        public string TransactionType { get; set; }

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
