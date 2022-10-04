namespace Kata09.Orders;

public record OrderItem(Product Product, decimal Quantity, Money UnitPrice, Money TotalPrice, IEnumerable<string>? AppliedDiscounts = null);