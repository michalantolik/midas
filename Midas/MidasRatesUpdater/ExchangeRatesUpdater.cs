using Application.ExchangeRates.Commands;
using Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence;

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

            // Get current exchange rates using NBP API service
            var nbpApiService = NbpApiServiceFactory.Create();
            var ratesTable = nbpApiService.GetCurrentExchangeRatesTable("B");

            // Overwrite existing exchange rates in the database
            var databaseService = DatabaseServiceFactory.Create(_configuration);
            var updateRatesCommand = UpdateExchangeRatesCommandFactory.Create(databaseService);
            updateRatesCommand.Execute(ratesTable);
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
