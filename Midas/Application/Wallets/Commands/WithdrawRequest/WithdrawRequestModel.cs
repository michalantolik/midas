namespace Application.Wallets.Commands.WithdrawRequest
{
    public class WithdrawRequestModel
    {
        public int WalletId { get; set; }
        public string CurrencyCode { get; set; }
        public decimal Amount { get; set; }
    }
}
