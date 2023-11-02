namespace Application.Wallets.Commands.DepositRequest
{
    public class DepositRequestModel
    {
        public int WalletId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}
