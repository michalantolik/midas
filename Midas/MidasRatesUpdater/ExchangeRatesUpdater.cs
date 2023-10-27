using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
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
