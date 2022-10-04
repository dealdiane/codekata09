namespace Kata09.Carts;

public record CartItem
{
    public int Id { get; set; }

    public Product Product { get; set; } = null!;

    public decimal Quantity { get; set; }
}