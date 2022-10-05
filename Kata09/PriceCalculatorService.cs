namespace Kata09;

public class PriceCalculatorService
{
    private readonly CurrencyService _currencyService;
    private readonly IEnumerable<INonStackableDiscountRule> _nonStackableDiscountRules;
    private readonly IEnumerable<IStackableDiscountRule> _stackableDiscountRules;

    public PriceCalculatorService(
        CurrencyService currencyService,
        IEnumerable<IStackableDiscountRule> stackableDiscountRules,
        IEnumerable<INonStackableDiscountRule> nonStackableDiscountRules)
    {
        _currencyService = currencyService;
        _stackableDiscountRules = stackableDiscountRules;
        _nonStackableDiscountRules = nonStackableDiscountRules;
    }

    public CalculatedPrice GetBestPrice(CartItem item)
    {
        // TODO: Reduce duplicate code.

        var nonStackableDiscountRules = _nonStackableDiscountRules
            .OrderByDescending(rule => rule.Priority)
            .ToList();

        var currentPrice = item.Product.UnitCost * item.Quantity;
        var appliedDiscountRules = new List<string>();

        foreach (var discountRule in nonStackableDiscountRules)
        {
            var isValid = discountRule.IsDiscountValid(item);

            if (!isValid)
            {
                continue;
            }

            var discountedPrice = discountRule.CalculateCartItemPrice(item);

            if (discountedPrice < currentPrice)
            {
                currentPrice = discountedPrice;
                appliedDiscountRules.Add(discountRule.Name);
            }
        }

        var stackableDiscountRules = _stackableDiscountRules
            .OrderByDescending(rule => rule.Priority)
            .ToList();

        foreach (var discountRule in stackableDiscountRules)
        {
            var isValid = discountRule.IsDiscountValid(item);

            if (!isValid)
            {
                continue;
            }

            var discountedPrice = discountRule.CalculateCartItemPrice(item, currentPrice);

            if (discountedPrice < currentPrice)
            {
                currentPrice = discountedPrice;
                appliedDiscountRules.Add(discountRule.Name);
            }
        }

        return new CalculatedPrice(new Money(currentPrice, item.Product.UnitCost.Currency), appliedDiscountRules);
    }
}

public record CalculatedPrice(Money Value, IEnumerable<string> AppliedDiscounts);