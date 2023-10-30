using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MidasRatesUpdater.Mapping;
using MidasRatesUpdater.Services.Database;
using MidasRatesUpdater.Services.Database.Entities;
using MidasRatesUpdater.Services.NbpWebApi;

namespace MidasRatesUpdater
{
    public class ExchangeRatesUpdater
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public ExchangeRatesUpdater(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<ExchangeRatesUpdater>();
            _configuration = configuration;
        }

        [Function("ExchangeRatesUpdater")]
        public void Run([TimerTrigger("*/5 * * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function started execution at: {DateTime.Now}");

            // Create NBP Web API service
            var nbpService = new NbpWebApiService();

            // Get current exchange rates using NBP service
            var ratesTable = nbpService.GetCurrentExchangeRatesTable("B");

            // Map DTOs returned by NBP service into EF Core entities
            var ratesEntities = ratesTable.ToExchangeRates();

            // Using Database service
            using (IDatabaseService dbService = new DatabaseService(_configuration))
            {
                // Delete all existing exchange rates records from DB (we only want to keep the latest ones from Web API)
                var entitiesToRemove = dbService.ExchangeRates.ToList();
                dbService.ExchangeRates.RemoveRange(entitiesToRemove);
                dbService.Save();

                // Insert new exchange rates into DB
                dbService.ExchangeRates.AddRange(ratesEntities);
                dbService.Save();
            }

            Console.WriteLine($"{nameof(ExchangeRate.Id),-6}{nameof(ExchangeRate.Currency),-45}{nameof(ExchangeRate.Code),-8}{nameof(ExchangeRate.Mid),-15}{nameof(ExchangeRate.TableName),-12}{nameof(ExchangeRate.TableNo),-20}{nameof(ExchangeRate.EffectiveDate),-10}\n");
            foreach (var rate in ratesEntities)
            {
                Console.WriteLine($"{rate.Id,-6}{rate.Currency,-45}{rate.Code,-8}{rate.Mid,-15}{rate.TableName,-12}{rate.TableNo,-20}{rate.EffectiveDate,-12}\n");
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
