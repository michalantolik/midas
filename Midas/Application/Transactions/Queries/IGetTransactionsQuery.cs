namespace Application.Transactions.Queries
{
    public interface IGetTransactionsQuery
    {
        List<TransactionModel> Execute(int? walletId = null);
    }
}
