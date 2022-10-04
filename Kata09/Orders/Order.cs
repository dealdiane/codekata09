namespace Kata09.Orders;

public record Order(Guid Id, IEnumerable<OrderItem> Items, DateTimeOffset CreatedOn);