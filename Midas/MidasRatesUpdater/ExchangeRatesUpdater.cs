using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using MidasRatesUpdater.Services.NbpWebApi;

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
        public async Task Run([TimerTrigger("*/5 * * * * *")] MyInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function started execution at: {DateTime.Now}");

            var nbpService = new NbpWebApiService();
            var response = await nbpService.GetCurrentExchangeRatesAsync("B");
            
            _logger.LogInformation($"{nameof(response.Success), -15}:     {response.Success}");
            _logger.LogInformation($"{nameof(response.StatusCode),-15}:   {response.StatusCode}");
            _logger.LogInformation($"{nameof(response.ReasonPhrase),-15}: {response.ReasonPhrase}");
            _logger.LogInformation($"{nameof(response.Content),-15}:      {response.Content}");
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
