using System.Collections.Generic;

namespace Application.Wallets.Queries.GetWalletsList
{
    public interface IGetWalletsListQuery
    {
        List<WalletWithBalancesModel> Execute(int? id = null);
    }
}
