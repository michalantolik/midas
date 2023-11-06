using Application.Wallets.Commands.ConvertRequest;
using Application.Wallets.Commands.DeleteWallet;
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
        private readonly ICreateWalletCommand _createWalletCommand;
        private readonly IDeleteWalletCommand _deleteWalletCommand;
        private readonly ICreateWalletCommand _depositRequestCommand;
        private readonly IWithdrawRequestCommand _withdrawRequestCommand;
        private readonly IConvertRequestCommand _convertRequestCommand;

        public WalletsController(
            IGetWalletsListQuery getWalletsListQuery,
            ICreateWalletCommand createWalletCommand,
            IDeleteWalletCommand deleteWalletCommand,
            ICreateWalletCommand depositRequestCommand,
            IWithdrawRequestCommand withdrawRequestCommand,
            IConvertRequestCommand convertRequestCommand)
        {
            _getWalletsListQuery = getWalletsListQuery;
            _createWalletCommand = createWalletCommand;
            _deleteWalletCommand = deleteWalletCommand;
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

        // POST: api/wallets/create
        [HttpPost("create/{walletName}")]
        public IActionResult CreateWallet(string walletName)
        {
            var model = new CreateWalletModel { Name = walletName };

            var result = _createWalletCommand.Execute(model);

            return result;
        }

        // POST: api/wallets/delete
        [HttpPost("delete/{walletId}")]
        public IActionResult DeleteWallet(int walletId)
        {
            var model = new DeleteWalletModel { Id = walletId };

            var result = _deleteWalletCommand.Execute(model);

            return result;
        }

        // POST: api/wallets/deposit
        [HttpPost("deposit")]
        public IActionResult DepositMoney([FromBody] CreateWalletModel model)
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
