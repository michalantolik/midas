using Application.ExchangeRates.Commands.Queries;
using Microsoft.AspNetCore.Mvc;

namespace WalletsAPI.Controllers
{
    [ApiController]
    [Route("api/exchangerates")]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IGetExchangeRatesQuery _getExchangeRates;

        public ExchangeRatesController(IGetExchangeRatesQuery getExchangeRates)
        {
            _getExchangeRates = getExchangeRates;
        }

        /// <summary>
        /// Get all exchange rates
        /// </summary>
        /// <remarks>GET: exchangerates</remarks>
        [HttpGet()]
        public ActionResult<IEnumerable<ExchangeRateModel>> GetAllExchangeRates()
        {
            var exchangeRates = _getExchangeRates.Execute();

            return Ok(exchangeRates);
        }
    }
}
