using System.Text.Json.Serialization;

namespace Data.ExchangeRates
{
    /// <summary>
    /// DTO which reflects table of exchange rates as read from NBP Web API.
    /// </summary>
    public class ExchangeRatesTableDto
    {
        [JsonPropertyName("table")]
        public string Table { get; set; }

        [JsonPropertyName("no")]
        public string No { get; set; }

        [JsonPropertyName("effectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonPropertyName("rates")]
        public List<ExchangeRateDto> Rates { get; set; }
    }
}
