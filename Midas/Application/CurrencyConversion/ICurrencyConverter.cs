namespace Application.CurrencyConversion
{
    public interface ICurrencyConverter
    {
        bool TryConvert(string sourceCurrencyCode, string targetCurrencyCode, decimal sourceAmount, out decimal targetAmount);
    }
}