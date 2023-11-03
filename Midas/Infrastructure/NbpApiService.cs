using Application.Interfaces;
using Data.ExchangeRates;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;

namespace Infrastructure
{
    /// <inheritdoc />
    public class NbpApiService : INbpApiService
    {
        /// <inheritdoc />
        public ExchangeRatesTableDto GetCurrentExchangeRatesTable(string table)
        {
            using (var client = new HttpClient())
            {
                var response = client.GetAsync($"http://api.nbp.pl/api/exchangerates/tables/{table}/").Result;
                if (!response.IsSuccessStatusCode)
                {
                    return new ExchangeRatesTableDto { Rates = new List<ExchangeRateDto>() };
                }

                var ratesTable = JsonSerializer.Deserialize<List<ExchangeRatesTableDto>>(response.Content.ReadAsStringAsync().Result);
                if (ratesTable?.SingleOrDefault() == null)
                {
                    return new ExchangeRatesTableDto { Rates = new List<ExchangeRateDto>() };
                }

                var result = ratesTable.Single();
                return result;
            }
        }
    }
}
