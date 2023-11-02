using Data.Wallets;
using System.Collections.Generic;

namespace Application.Wallets.Queries.GetWalletsList
{
    public interface IGetWalletsListQuery
    {
        List<WalletWithBalancesDto> Execute();
    }
}