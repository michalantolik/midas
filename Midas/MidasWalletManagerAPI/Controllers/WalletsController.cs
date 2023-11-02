using Application.Wallets.Commands.DepositRequest;
using Application.Wallets.Queries.GetWalletsList;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletsController : ControllerBase
    {
        private readonly IGetWalletsListQuery _getWalletsListQuery;
        private readonly IDepositRequestCommand _depositRequestCommand;

        public WalletsController(
            IGetWalletsListQuery getWalletsListQuery,
            IDepositRequestCommand depositRequestCommand)
        {
            _getWalletsListQuery = getWalletsListQuery;
            _depositRequestCommand = depositRequestCommand;
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

        // POST: api/wallets/deposit
        [HttpPost("deposit")]
        public IActionResult DepositMoney([FromBody] DepositRequestModel model)
        {
            var succeeded = _depositRequestCommand.Execute(model);

            return succeeded ? Ok() : BadRequest();
        }
    }
}
