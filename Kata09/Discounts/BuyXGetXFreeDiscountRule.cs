using Microsoft.Extensions.Options;

namespace Kata09.Discounts;

public sealed class BuyXGetXFreeDiscountRule : INonStackableDiscountRule
{
    private readonly BuyXGetXFreeDiscountRuleOptions _options;

    public BuyXGetXFreeDiscountRule(IOptions<BuyXGetXFreeDiscountRuleOptions> options)
    {
        _options = options.Value;
    }

    public bool IsStackable { get; } = false;

    public string Name => $"Buy {_options.Multiplier}, get {_options.Divisor}";

    public int Priority { get; } = 100;

    public decimal CalculateCartItemPrice(CartItem item)
    {
        var quantity = item.Quantity;
        var totalItemsToCharge = (quantity / _options.Divisor) * _options.Multiplier;

        totalItemsToCharge += quantity % _options.Divisor;

        return item.Product.UnitCost * totalItemsToCharge;
    }

    public bool IsDiscountValid(CartItem item) => _options.DiscountedSkus.Contains(item.Product.Sku);
}

public class BuyXGetXFreeDiscountRuleOptions
{
    public IEnumerable<int> DiscountedSkus { get; set; } = default!;

    public int Divisor { get; set; }

    public int Multiplier { get; set; }
}