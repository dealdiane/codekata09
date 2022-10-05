namespace Kata09.Discounts;

public sealed class StackableHalfPriceDiscountRule : IStackableDiscountRule
{
    public bool IsStackable { get; } = true;

    public int Priority { get; } = 500;

    public decimal CalculateCartItemPrice(CartItem item, decimal currentPrice) => currentPrice * 0.5m;

    public bool IsDiscountValid(CartItem item) => true;  // Currently valid for all products. Customise/validate as per requirement.
}