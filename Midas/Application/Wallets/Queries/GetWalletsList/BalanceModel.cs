namespace Application.Wallets.Queries.GetWalletsList
{
    public class BalanceModel
    {
        public int Id { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }
    }
}
