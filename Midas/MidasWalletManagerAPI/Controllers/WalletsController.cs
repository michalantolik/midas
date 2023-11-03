using Application.Wallets.Commands.ConvertRequest;
using Application.Wallets.Commands.DepositRequest;
using Application.Wallets.Commands.WithdrawRequest;
using Application.Wallets.Queries.GetWalletsList;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly IGetWalletsListQuery _getWalletsListQuery;
        private readonly IDepositRequestCommand _depositRequestCommand;
        private readonly IWithdrawRequestCommand _withdrawRequestCommand;
        private readonly IConvertRequestCommand _convertRequestCommand;

        public WalletsController(
            IGetWalletsListQuery getWalletsListQuery,
            IDepositRequestCommand depositRequestCommand,
            IWithdrawRequestCommand withdrawRequestCommand,
            IConvertRequestCommand convertRequestCommand)
        {
            _getWalletsListQuery = getWalletsListQuery;
            _depositRequestCommand = depositRequestCommand;
            _withdrawRequestCommand = withdrawRequestCommand;
            _convertRequestCommand = convertRequestCommand;
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
            var result = _depositRequestCommand.Execute(model);

            return result;
        }

        // POST: api/wallets/withdraw
        [HttpPost("withdraw")]
        public IActionResult WithdrawMoney([FromBody] WithdrawRequestModel model)
        {
            var result = _withdrawRequestCommand.Execute(model);

            return result;
        }

        // POST: api/wallets/convert
        [HttpPost("convert")]
        public IActionResult ConvertMoney([FromBody] ConvertRequestModel model)
        {
            var result = _convertRequestCommand.Execute(model);

            return result;
        }
    }
}
