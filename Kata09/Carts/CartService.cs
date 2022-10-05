namespace Kata09.Carts
{
    public class CartService
    {
        private readonly IDictionary<Cart, IList<CartItem>> _cartItems = new Dictionary<Cart, IList<CartItem>>();
        private readonly IDictionary<Customer, Cart> _carts = new Dictionary<Customer, Cart>();

        public void AddProduct(Cart cart, Product product, decimal quantity)
        {
            // TODO: Fix call to ValidateCartItem();

            var cartItems = _cartItems[cart];

            cartItems.Add(new CartItem
            {
                Product = product,
                Quantity = quantity,
            });

            _cartItems[cart] = ConsolidateCartItems(cartItems).ToList();
        }

        public IEnumerable<CartItem> ConsolidateCartItems(IEnumerable<CartItem> cartItems)
        {
            return cartItems
                .GroupBy(item => item.Product)
                .Select(groupedItem => new CartItem
                {
                    Product = groupedItem.Key,
                    Quantity = groupedItem.Sum(item => item.Quantity)
                });
        }

        public Cart GetCartByCustomer(Customer customer)
        {
            if (_carts.TryGetValue(customer, out var cart) && cart is not null)
            {
                return cart;
            }

            cart = new Cart();

            _carts[customer] = cart;
            _cartItems[cart] = new List<CartItem>();

            return cart;
        }

        public IEnumerable<CartItem> GetCartItems(Cart cart)
        {
            _cartItems.TryGetValue(cart, out var cartItems);

            return cartItems ?? Enumerable.Empty<CartItem>();
        }

        private void ValidateCartItem(CartItem item)
        {
            // TODO: Assert ACL, check inventory, availability, etc.
        }
    }
}