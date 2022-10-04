namespace Kata09.Discounts;

public interface IDiscountRule
{
    string Name { get => GetType().ToString(); }

    int Priority { get; }

    bool IsDiscountValid(CartItem item);
}

public interface INonStackableDiscountRule : IDiscountRule
{
    decimal CalculateCartItemPrice(CartItem item);
}

public interface IStackableDiscountRule : IDiscountRule
{
    decimal CalculateCartItemPrice(CartItem item, decimal currentPrice);
}