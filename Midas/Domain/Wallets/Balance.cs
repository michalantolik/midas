using Domain.Common;

namespace Domain.Wallets
{
    /// <summary>
    /// DB entity class for a single currency balance in a wallet.
    /// </summary>
    public class Balance : IEntity
    {
        public int Id { get; set; }

        public string CurrencyCode { get; set; }

        public decimal Amount { get; set; }

        public Wallet Wallet { get; set; }
    }
}
