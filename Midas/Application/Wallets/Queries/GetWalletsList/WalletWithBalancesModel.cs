using System.Collections.Generic;

namespace Application.Wallets.Queries.GetWalletsList
{
    public class WalletWithBalancesModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<BalanceModel> Balances { get; set; }
    }
}
