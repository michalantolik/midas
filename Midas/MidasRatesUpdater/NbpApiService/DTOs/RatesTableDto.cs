using System.Text.Json.Serialization;

namespace MidasRatesUpdater.Services.NbpApi.Data
{
    /// <summary>
    /// DTO which reflects table of exchange rates as read from NBP Web API.
    /// </summary>
    public class RatesTableDto
    {
        [JsonPropertyName("table")]
        public string Table { get; set; }

        [JsonPropertyName("no")]
        public string No { get; set; }

        [JsonPropertyName("effectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonPropertyName("rates")]
        public List<RateDto> Rates { get; set; }
    }
}
