// Not used. Replaced with implementation that takes options during interview

//using Kata09.Carts;

//namespace Kata09.Discounts
//{
//    public sealed class BuyTwoGetOneFreeDiscountRule : INonStackableDiscountRule
//    {
//        public bool IsStackable { get; } = false;

//        public int Priority { get; } = 100;

//        public string Name => "Buy 2 Get 1 Free";

//        public decimal CalculateCartItemPrice(CartItem item)
//        {
//            var quantity = item.Quantity;
//            var totalItemsToCharge = (quantity / 3) * 2;

//            totalItemsToCharge += quantity % 3;

//            return item.Product.UnitCost * totalItemsToCharge;
//        }

//        public bool IsDiscountValid(CartItem item) => true;
//    }
//}