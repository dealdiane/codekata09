using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

const int SkuWithDiscount = 1;

var hostBuilder = Host.CreateDefaultBuilder(args)
        .ConfigureAppConfiguration((context, builder) =>
        {
            builder.SetBasePath(Directory.GetCurrentDirectory());
        })
        .ConfigureServices((context, services) =>
        {
            // If unit testing, this will be the "setup" method

            services.Configure<BuyXGetXFreeDiscountRuleOptions>(options =>
            {
                options.Multiplier = 2; // Get 2
                options.Divisor = 3; // Buy 3
                options.DiscountedSkus = new[] { SkuWithDiscount };
            });

            services.AddScoped<PriceCalculatorService>();
            services.AddScoped<CurrencyService>();
            services.AddScoped<CartService>();
            services.AddScoped<OrderService>();
            services.AddScoped<INonStackableDiscountRule, BuyXGetXFreeDiscountRule>();

            //services.AddScoped<INonStackableDiscountRule, BuyTwoGetOneFreeDiscountRule>();

            // More discount rules
            //services.AddScoped<INonStackableDiscountRule, HalfPriceDiscountRule>();
            //services.AddScoped<IStackableDiscountRule, StackableHalfPriceDiscountRule>();

            services.AddScoped<Runner>();
        });

var host = hostBuilder.Build();
var services = host.Services;

using var scope = services.CreateScope();

using var cts = new CancellationTokenSource();

Console.CancelKeyPress += (_, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

try
{
    await scope.ServiceProvider.GetService<Runner>()!.RunAsync(cts.Token);
}
catch (TaskCanceledException)
{
}