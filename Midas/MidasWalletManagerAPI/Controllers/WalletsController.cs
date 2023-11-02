using Application.Wallets.Queries.GetWalletsList;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IGetWalletsListQuery _getWalletsListQuery;

        public WalletsController(IGetWalletsListQuery getWalletsListQuery)
        {
            _getWalletsListQuery = getWalletsListQuery;
        }

        // GET: api/wallets
        [HttpGet()]
        public ActionResult<IEnumerable<WalletWithBalancesModel>> GetAllWallets()
        {
            var dtos = _getWalletsListQuery.Execute();

            return Ok(dtos);
        }

        // GET: api/wallets/{walletId}
        [HttpGet("{walletId}")]
        public ActionResult<IEnumerable<WalletWithBalancesModel>> GetOneWallet(int walletId)
        {
            var dtos = _getWalletsListQuery.Execute(walletId);

            return Ok(dtos);
        }
    }
}
