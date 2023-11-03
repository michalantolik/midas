namespace Application.Wallets.Commands.ConvertRequest
{
    public class ConvertRequestModel
    {
        public int WalletId { get; set; }
        public string SourceCurrencyCode { get; set; }
        public string TargetCurrencyCode { get; set; }
        public decimal SourceAmountToConvert { get; set; }
    }
}
