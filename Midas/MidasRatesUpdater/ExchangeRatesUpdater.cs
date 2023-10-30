using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MidasRatesUpdater.Services.NbpWebApi;
using MidasRatesUpdater.Services.NbpWebApi.Data;
using System.Text.Json;

namespace MidasRatesUpdater
{
    public class ExchangeRatesUpdater
    {
        private readonly ILogger _logger;

        public ExchangeRatesUpdater(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ExchangeRatesUpdater>();
        }

        [Function("ExchangeRatesUpdater")]
        public void Run([TimerTrigger("*/5 * * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function started execution at: {DateTime.Now}");

            var nbpService = new NbpWebApiService();
            var response = nbpService.GetCurrentExchangeRates("B");

            Console.WriteLine($"{nameof(response.Success),-15}: {response.Success}");
            Console.WriteLine($"{nameof(response.StatusCode),-15}: {response.StatusCode}");
            Console.WriteLine($"{nameof(response.ReasonPhrase),-15}: {response.ReasonPhrase}\n");

            if (response.Success)
            {
                var ratesTablesCollection = JsonSerializer.Deserialize<List<RatesTableDto>>(response.Content);

                if (ratesTablesCollection?.SingleOrDefault()?.Rates == null)
                {
                    Console.WriteLine($"Problem with NBP Web API deserialization occurred !!!");
                    return;
                }

                var ratesTable = ratesTablesCollection.Single();

                Console.WriteLine($"{nameof(ratesTable.Table),-15}: {ratesTable.Table}");
                Console.WriteLine($"{nameof(ratesTable.No),-15}: {ratesTable.No}");
                Console.WriteLine($"{nameof(ratesTable.EffectiveDate),-15}: {ratesTable.EffectiveDate}");

                foreach (var rate in ratesTable.Rates)
                {
                    Console.WriteLine($"{rate.Code,-5}{rate.Currency,-45}{rate.Mid}\n");
                }
            }
        }
    }

    public class MyInfo
    {
        public MyScheduleStatus ScheduleStatus { get; set; }

        public bool IsPastDue { get; set; }
    }

    public class MyScheduleStatus
    {
        public DateTime Last { get; set; }

        public DateTime Next { get; set; }

        public DateTime LastUpdated { get; set; }
    }
}
