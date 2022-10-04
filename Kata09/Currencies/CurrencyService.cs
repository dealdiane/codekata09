namespace Kata09.Currencies;

public class CurrencyService
{
    private const string BaseCurrencyName = "USD";

    private static readonly IDictionary<string, Currency> _currencies = new[]
            {
        new Currency(BaseCurrencyName, 1.00m),
        new Currency("NZD", 1.74m),
        new Currency("SGD", 1.45m),
        new Currency("KRW", 1_420m, 0),
    }
    .ToDictionary(currency => currency.Name, StringComparer.OrdinalIgnoreCase);

    public decimal Convert(decimal value, Currency sourceCurrency, Currency targetCurrency)
    {
        if (sourceCurrency == targetCurrency)
        {
            return value;
        }

        var baseCurrency = GetCurrencyByName(BaseCurrencyName);

        if (baseCurrency != sourceCurrency)
        {
            value /= sourceCurrency.Rate;
        }

        return value * targetCurrency.Rate;
    }

    public Money Convert(Money price, Currency targetCurrency)
    {
        var adjustedValue = Convert(price.Value, price.Currency, targetCurrency);

        return new Money(adjustedValue, targetCurrency);
    }

    public Currency GetCurrencyByName(string name) => _currencies[name];

    public Money RoundOff(Money price)
    {
        var value = Math.Round(price.Value, price.Currency.Precision);

        return price with
        {
            Value = value
        };
    }
}