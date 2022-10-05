using Kata09.Carts;
using Kata09.Currencies;
using Kata09.Discounts;
using Kata09.Orders;
using Kata09.Users;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace Kata09
{
    internal sealed class Runner
    {
        private readonly CartService _cartService;
        private readonly CurrencyService _currencyService;
        private readonly OrderService _orderService;
        private readonly PriceCalculatorService _priceCalculatorService;
        private readonly BuyXGetXFreeDiscountRuleOptions _buyDiscountOptions;

        public Runner(
            CartService cartService,
            OrderService orderService,
            CurrencyService currencyService,
            PriceCalculatorService priceCalculatorService,
            IOptions<BuyXGetXFreeDiscountRuleOptions> buyDiscountOptions)
        {
            _cartService = cartService;
            _orderService = orderService;
            _currencyService = currencyService;
            _priceCalculatorService = priceCalculatorService;
            _buyDiscountOptions = buyDiscountOptions.Value;
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            // Setup mock data
            // Note: Mock currencies are setup in CurrencyService
            var nzd = _currencyService.GetCurrencyByName("NZD");
            var sgd = _currencyService.GetCurrencyByName("SGD");
            var krw = _currencyService.GetCurrencyByName("KRW");

            var customer = new Customer("John Doe", nzd);
            var cart = _cartService.GetCartByCustomer(customer);

            Debug.Assert(_buyDiscountOptions.DiscountedSkus?.Count() > 0);

            var productA = new Product(_buyDiscountOptions.DiscountedSkus.First(), "Apple (Discounted)", new Money(1m, sgd));
            var productB = new Product(2, "Apple (Rejects)", new Money(725m, krw));

            _cartService.AddProduct(cart, productA, 2);
            _cartService.AddProduct(cart, productA, 1);
            _cartService.AddProduct(cart, productB, 2);
            _cartService.AddProduct(cart, productB, 1);

            var order = _orderService.Checkout(cart);

            Console.WriteLine($"Order: {order.Id}");
            Console.WriteLine($"Date: {order.CreatedOn}" + Environment.NewLine);

            foreach (var orderItem in order.Items)
            {
                var unitPriceInCustomerCurrency = _currencyService.Convert(orderItem.UnitPrice, customer.Currency);
                var totalPriceInCustomerCurrency = _currencyService.Convert(orderItem.TotalPrice, customer.Currency);

                Console.WriteLine($"{orderItem.Quantity}x {orderItem.Product.Name}\t@{unitPriceInCustomerCurrency}\t {totalPriceInCustomerCurrency}");

                var appliedDiscounts = orderItem.AppliedDiscounts?.ToList();

                if (appliedDiscounts?.Count > 0)
                {
                    Console.Write("\tApplied Discount(s): ");
                    Console.WriteLine("\t" + String.Join(Environment.NewLine + '\t', appliedDiscounts));
                }
                else
                {
                    Console.Write("\tNo discount applied");
                }

                Console.WriteLine();
            }

            return Task.CompletedTask;
        }
    }
}