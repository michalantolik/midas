using Application.ExchangeRates.Commands.UpdateExchangeRates;
using Infrastructure;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Persistence;

namespace ExchangeRatesUpdater
{
    public class UpdateExchangeRates
    {
        private readonly ILogger _logger;
        private readonly IConfiguration _configuration;

        public UpdateExchangeRates(ILoggerFactory loggerFactory, IConfiguration configuration)
        {
            _logger = loggerFactory.CreateLogger<UpdateExchangeRates>();
            _configuration = configuration;
        }

        [Function("UpdateExchangeRates")]
        public void Run([TimerTrigger("*/30 * * * * *")] MyInfo myTimer)
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
