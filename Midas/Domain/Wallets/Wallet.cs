using Domain.Common;
using System.Collections.Generic;

namespace Domain.Wallets
{
    /// <summary>
    /// DB entity class for a multi-currency wallet.
    /// </summary>
    public class Wallet : IEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Balance> Balances { get; set; } = new List<Balance>();

        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();
    }
}
