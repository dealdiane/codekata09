namespace Kata09.Orders;

public class OrderService
{
    private readonly CartService _cartService;
    private readonly PriceCalculatorService _priceCalculatorService;

    public OrderService(
        CartService cartService,
        PriceCalculatorService priceCalculatorService)
    {
        _cartService = cartService;
        _priceCalculatorService = priceCalculatorService;
    }

    public Order Checkout(Cart cart)
    {
        var cartItems = _cartService.GetCartItems(cart);
        var orderItems = CreateOrderItems(cartItems);

        return new Order(Guid.NewGuid(), orderItems, DateTimeOffset.Now);
    }

    public void ValidateCart(Cart cart)
    {
        // TODO:
        // Add cart validation login.
        // Throw exception when validation fails.
    }

    private IEnumerable<OrderItem> CreateOrderItems(IEnumerable<CartItem> cartItems)
    {
        foreach (var cartItem in cartItems)
        {
            var bestPrice = _priceCalculatorService.GetBestPrice(cartItem);

            yield return new OrderItem(cartItem.Product, cartItem.Quantity, cartItem.Product.UnitCost, bestPrice.Value, bestPrice.AppliedDiscounts);
        }
    }
}