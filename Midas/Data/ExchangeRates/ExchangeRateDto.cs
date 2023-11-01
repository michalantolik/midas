using System.Text.Json.Serialization;

namespace Data.ExchangeRates
{
    /// <summary>
    /// DTO which reflects single exchange rate object as read from NBP Web API.
    /// </summary>
    public class ExchangeRateDto
    {
        [JsonPropertyName("currency")]
        public string Currency { get; set; }

        [JsonPropertyName("code")]
        public string Code { get; set; }

        [JsonPropertyName("mid")]
        public double Mid { get; set; }
    }
}
