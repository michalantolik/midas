using Application.ExchangeRates.Commands.Queries;
using Application.Transactions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/transactions")]
    public class TransactionsController : ControllerBase
    {
        private readonly IGetTransactionsQuery _getTransactionsQuery;

        public TransactionsController(IGetTransactionsQuery getTransactionsQuery)
        {
            _getTransactionsQuery = getTransactionsQuery;
        }

        /// <summary>
        /// Get all transactions
        /// </summary>
        /// <remarks>GET: api/transactions</remarks>
        [HttpGet()]
        public ActionResult<IEnumerable<TransactionModel>> GetAllTransactions()
        {
            var transactions = _getTransactionsQuery.Execute();

            return Ok(transactions);
        }

        /// <summary>
        /// Get transactions for a given wallet ID
        /// </summary>
        /// <param name="walletId">Id of a wallet to get transactions for</param>
        /// <remarks>GET: api/transactions/{walletId}</remarks>
        [HttpGet("{walletId}")]
        public ActionResult<IEnumerable<TransactionModel>> GetTransactionsByWalletId(int walletId)
        {
            var transactions = _getTransactionsQuery.Execute(walletId);

            return Ok(transactions);
        }
    }
}
