using Application.Interfaces;
using Data.Wallets;
using System.Collections.Generic;
using System.Linq;

namespace Application.Wallets.Queries.GetWalletsList
{
    public class GetWalletsListQuery : IGetWalletsListQuery
    {
        private readonly IDatabaseService _database;

        public GetWalletsListQuery(IDatabaseService database)
        {
            _database = database;
        }

        public List<WalletWithBalancesDto> Execute(int? id = null)
        {
            var wallets = id == null
                ? _database.Wallets.ToList()
                : _database.Wallets.Where(x => x.Id == id).ToList();

            var walletDto = wallets.Select(wallet => new WalletWithBalancesDto
            {
                Id = wallet.Id,
                Name = wallet.Name,
                Balances = wallet.Balances.Select(balance => new BalanceDto
                {
                    Id = balance.Id,
                    CurrencyCode = balance.CurrencyCode,
                    Amount = balance.Amount
                }).ToList()
            }).ToList();

            return walletDto;
        }
    }
}
