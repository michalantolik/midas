namespace Application.ExchangeRates.Commands.Queries
{
    public class ExchangeRateModel
    {
        public int Id { get; set; }

        public string Currency { get; set; }

        public string Code { get; set; }

        public decimal Mid { get; set; }

        public string TableName { get; set; }

        public string TableNo { get; set; }

        public string EffectiveDate { get; set; }

        public string CreatedDate { get; set; }
    }
}
