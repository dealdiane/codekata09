namespace Kata09.Currencies;

public record Money(decimal Value, Currency Currency)
{
    public override string ToString() => $"{Value.ToString("N" + Currency.Precision)} {Currency.Name}";

    public static implicit operator decimal(Money instance) => instance.Value;
}