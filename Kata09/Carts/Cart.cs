namespace Kata09.Carts;

public record Cart
{
    public Customer Customer { get; set; } = null!;

    //public IList<CartItem> Items { get; } = new List<CartItem>();
}