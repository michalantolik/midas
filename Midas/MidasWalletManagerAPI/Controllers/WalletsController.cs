using Application.Wallets.Commands.ConvertRequest;
using Application.Wallets.Commands.DeleteWallet;
using Application.Wallets.Commands.DepositRequest;
using Application.Wallets.Commands.WithdrawRequest;
using Application.Wallets.Queries.GetWalletsList;
using Microsoft.AspNetCore.Mvc;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/wallets")]
    public class WalletsController : ControllerBase
    {
        private readonly IGetWalletsListQuery _getWalletsListQuery;
        private readonly ICreateWalletCommand _createWalletCommand;
        private readonly IDeleteWalletCommand _deleteWalletCommand;
        private readonly IDepositRequestCommand _depositRequestCommand;
        private readonly IWithdrawRequestCommand _withdrawRequestCommand;
        private readonly IConvertRequestCommand _convertRequestCommand;

        public WalletsController(
            IGetWalletsListQuery getWalletsListQuery,
            ICreateWalletCommand createWalletCommand,
            IDeleteWalletCommand deleteWalletCommand,
            IDepositRequestCommand depositRequestCommand,
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

        #region Get all wallets

        /// <summary>
        /// Get all wallets (with their balanaces, but without transactions)
        /// </summary>
        /// <remarks>GET: api/wallets</remarks>
        [HttpGet()]
        public ActionResult<IEnumerable<WalletWithBalancesModel>> GetAllWallets()
        {
            var dtos = _getWalletsListQuery.Execute();

            return Ok(dtos);
        }

        #endregion List all wallets


        #region Get one wallet

        /// <summary>
        /// Get one wallet (with its balanaces, but without transactions)
        /// </summary>
        /// <param name="walletId">ID of a wallet to get</param>
        /// <remarks>GET: api/wallets/{walletId}</remarks>
        [HttpGet("{walletId}")]
        public ActionResult<IEnumerable<WalletWithBalancesModel>> GetOneWallet(int walletId)
        {
            var dtos = _getWalletsListQuery.Execute(walletId);

            return Ok(dtos);
        }

        #endregion Get one wallet


        #region Create new wallet

        /// <summary>
        /// Create new wallet
        /// </summary>
        /// <param name="model">Model of a new wallet</param>
        /// <remarks>POST: api/wallets/create/</remarks>
        [HttpPost("create")]
        public IActionResult CreateWallet(CreateWalletModel model)
        {
            var result = _createWalletCommand.Execute(model);

            return result;
        }

        /// <summary>
        /// Create new wallet
        /// </summary>
        /// <param name="walletName">Name of a new wallet</param>
        /// <remarks>POST: api/wallets/create/{walletName}</remarks>
        [HttpPost("create/{walletName}")]
        public IActionResult CreateWallet(string walletName)
        {
            var model = new CreateWalletModel { WalletName = walletName };

            var result = _createWalletCommand.Execute(model);

            return result;
        }

        #endregion Create new wallet


        #region Delete wallet

        /// <summary>
        /// Delete wallet
        /// </summary>
        /// <param name="model">Model of a wallet to be deleted</param>
        /// <remarks>POST: api/wallets/delete/</remarks>
        [HttpPost("delete")]
        public IActionResult DeleteWallet(DeleteWalletModel model)
        {
            var result = _deleteWalletCommand.Execute(model);

            return result;
        }

        /// <summary>
        /// Delete wallet
        /// </summary>
        /// <param name="walletId">Id of a wallet to be deleted</param>
        /// <remarks>POST: api/wallets/delete/{walletId}</remarks>
        [HttpPost("delete/{walletId}")]
        public IActionResult DeleteWallet(int walletId)
        {
            var model = new DeleteWalletModel { WalletId = walletId };

            var result = _deleteWalletCommand.Execute(model);

            return result;
        }

        #endregion Delete wallet


        #region Deposit money

        /// <summary>
        /// Deposit money in a wallet
        /// </summary>
        /// <param name="model">Model of a deposit request</param>
        /// <remarks>POST: api/wallets/deposit</remarks>
        [HttpPost("deposit")]
        public IActionResult DepositMoney([FromBody] DepositRequestModel model)
        {
            var result = _depositRequestCommand.Execute(model);

            return result;
        }

        /// <summary>
        /// Deposit money in a wallet
        /// </summary>
        /// <param name="walletId">Id of a wallet to deposit money in</param>
        /// <param name="currencyCode">Currency code of deposit</param>
        /// <param name="amount">Amount of deposit</param>
        /// <remarks>POST: api/wallets/deposit</remarks>
        [HttpPost("{walletId}/deposit/{amount}/{currencyCode}")]
        public IActionResult DepositMoney(int walletId, string currencyCode, decimal amount)
        {
            var model = new DepositRequestModel
            {
                WalletId = walletId,
                CurrencyCode = currencyCode,
                Amount = amount
            };

            var result = _depositRequestCommand.Execute(model);

            return result;
        }

        #endregion Deposit money


        #region Withdraw money

        /// <summary>
        /// Withdraw money from a wallet
        /// </summary>
        /// <param name="model">Model of a deposit request</param>
        /// <remarks>POST: api/wallets/withdraw</remarks>
        [HttpPost("withdraw")]
        public IActionResult WithdrawMoney([FromBody] WithdrawRequestModel model)
        {
            var result = _withdrawRequestCommand.Execute(model);

            return result;
        }

        /// <summary>
        /// Withdraw money from a wallet
        /// </summary>
        /// <param name="walletId">Id of a wallet to withdraw money from</param>
        /// <param name="currencyCode">Currency code of withdraw</param>
        /// <param name="amount">Amount of money to withdraw</param>
        /// <remarks>POST: api/wallets/{walletId}/withdraw/{amount}/{currencyCode}</remarks>
        [HttpPost("{walletId}/withdraw/{amount}/{currencyCode}")]
        public IActionResult WithdrawMoney(int walletId, string currencyCode, decimal amount)
        {
            var model = new WithdrawRequestModel
            {
                WalletId = walletId,
                CurrencyCode = currencyCode,
                Amount = amount
            };

            var result = _withdrawRequestCommand.Execute(model);

            return result;
        }

        #endregion Withdraw money


        #region Convert money

        /// <summary>
        /// Convert a part of a wallet into another currency
        /// </summary>
        /// <param name="model">Model of a convert request</param>
        /// <remarks>POST: api/wallets/convert</remarks>
        [HttpPost("convert")]
        public IActionResult ConvertMoney([FromBody] ConvertRequestModel model)
        {
            var result = _convertRequestCommand.Execute(model);

            return result;
        }

        public int WalletId { get; set; }
        public string SourceCurrencyCode { get; set; }
        public string TargetCurrencyCode { get; set; }
        public decimal SourceAmountToConvert { get; set; }

        /// <summary>
        /// Convert a part of a wallet into another currency
        /// </summary>
        /// <param name="walletId">Id of a wallet to convert money in</param>
        /// <param name="sourceCurrencyCode">Currency code of currency to convert from</param>
        /// <param name="targetCurrencyCode">Currency code of currency to convert to</param>
        /// <param name="sourceAmountToConvert">Amount of the money to (in source currency) to be converted to target currency</param>
        /// <remarks>POST: api/wallets/{walletId}/convert/{sourceAmountToConvert}/{sourceCurrencyCode}/{targetCurrencyCode}</remarks>
        [HttpPost("{walletId}/convert/{sourceAmountToConvert}/{sourceCurrencyCode}/{targetCurrencyCode}")]
        public IActionResult ConvertMoney(
            int walletId,
            string sourceCurrencyCode,
            string targetCurrencyCode,
            decimal sourceAmountToConvert)
        {
            var model = new ConvertRequestModel
            {
                WalletId = walletId,
                SourceCurrencyCode = sourceCurrencyCode,
                TargetCurrencyCode = targetCurrencyCode,
                SourceAmountToConvert = sourceAmountToConvert
            };

            var result = _convertRequestCommand.Execute(model);

            return result;
        }

        #endregion Convert money
    }
}
