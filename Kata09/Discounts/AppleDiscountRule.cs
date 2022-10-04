// Unused. Added during interview.

//using Kata09.Carts;

//namespace Kata09.Discounts
//{
//    public sealed class AppleDiscountRule : INonStackableDiscountRule
//    {
//        public int Priority { get; } = 100;

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