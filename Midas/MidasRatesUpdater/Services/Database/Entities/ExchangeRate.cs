using MidasRatesUpdater.Services.Database.Common;

namespace MidasRatesUpdater.Services.Database.Entities
{
    /// <summary>
    /// DB entity class for a currency exchange rate.
    /// </summary>
    public class ExchangeRate : IEntity
    {
        public int Id { get; set; }

        public string Currency { get; set; }

        public string Code { get; set; }

        public double Mid { get; set; }

        public string TableName { get; set; }


        public string TableNo { get; set; }

        public string EffectiveDate { get; set; }
    }
}
