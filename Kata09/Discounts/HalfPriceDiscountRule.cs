using Kata09.Carts;

namespace Kata09.Discounts;

public sealed class HalfPriceDiscountRule : INonStackableDiscountRule
{
    public bool IsStackable { get; } = false;

    public string Name => "50% Off";

    public int Priority { get; } = 500;

    public decimal CalculateCartItemPrice(CartItem item) => (item.Product.UnitCost.Value * item.Quantity) * 0.5m;

    public bool IsDiscountValid(CartItem item) => true; // Currently valid for all products. Customise/validate as per requirement.
}