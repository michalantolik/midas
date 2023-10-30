using System.Text.Json.Serialization;

namespace MidasRatesUpdater.Services.NbpWebApi.Data
{
    /// <summary>
    /// DTO which reflects single exchange rate object as read from NBP Web API.
    /// </summary>
    public class RateDto
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("mid")]
        public double Mid { get; set; }
    }
}
