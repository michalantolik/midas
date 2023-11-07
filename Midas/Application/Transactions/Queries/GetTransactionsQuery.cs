using Application.Interfaces;
using Domain.Wallets;

namespace Application.Transactions.Queries
{
    public class GetTransactionsQuery : IGetTransactionsQuery
    {
        private readonly IDatabaseService _database;

        public GetTransactionsQuery(IDatabaseService database)
        {
            _database = database;
        }

        public List<TransactionModel> Execute(int? walletId = null)
        {
            var transactions = new List<Transaction>();

            if (walletId == null)
            {
                transactions = _database.Trasactions.ToList();
            }
            else
            {
                transactions = _database.Trasactions.Where(t => t.Wallet.Id == walletId).ToList();
            }

            var results = transactions.Select(transaction => new TransactionModel
            {
                Id = transaction.Id,
                WalletId = transaction.Wallet.Id,
                TransactionType = transaction.TransactionType.ToString(),
                CurrencyCode = transaction.CurrencyCode,
                Amount = transaction.Amount,
                TargetCurrencyCode = transaction.TargetCurrencyCode,
                TargetAmount = transaction.TargetAmount,
                Timestamp = transaction.Timestamp
            }).ToList();

            return results;
        }
    }
}
