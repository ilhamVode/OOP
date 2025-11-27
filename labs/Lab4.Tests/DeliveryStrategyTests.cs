using Xunit;
using Lab4.Patterns.Strategy.Delivery;
using Lab4.Models;
using Lab4.Models.Orders;

namespace Lab4.Tests
{
    public class DeliveryStrategyTests
    {
        [Fact]
        public void StandartDeliveryStrategy_DefaultFee_Returns250()
        {
            // Arrange
            var strat = new StandartDeliveryStrategy();
            var customer = new Customer("C", "A", "P");
            var order = new StandardOrder(customer, new[] { new Product("X", 100m, "") });

            // Act
            var fee = strat.CalculateDeliveryPrice(order, subtotal: 500m);

            // Assert
            Assert.Equal(250m, fee);
        }

        [Fact]
        public void ExpressDeliveryStrategy_MinimumOrPercentBehavior()
        {
            // Arrange
            var strat = new ExpressDeliveryStrategy();
            var customer = new Customer("C", "A", "P");

            // small subtotal -> min 400
            var order1 = new ExpressOrder(customer, new[] { new Product("X", 100m, "") });
            var fee1 = strat.CalculateDeliveryPrice(order1, subtotal: 100m);
            Assert.Equal(400m, fee1);

            // large subtotal -> percent 0.7 * subtotal (as implemented)
            var order2 = new ExpressOrder(customer, new[] { new Product("Y", 2000m, "") });
            var fee2 = strat.CalculateDeliveryPrice(order2, subtotal: 2000m);
            Assert.Equal(1400.00m, fee2); // 2000 * 0.7
        }

        [Fact]
        public void BigOrderFreeDeliveryStrategy_DelegatesOrReturnsZero()
        {
            // Arrange
            var fallback = new StandartDeliveryStrategy(250m);
            var strat = new BigOrderFreeDeliveryStrategy(1500m, fallback);
            var customer = new Customer("C", "A", "P");
            var order = new StandardOrder(customer, new[] { new Product("X", 100m, "") });

            // small subtotal -> fallback
            var feeSmall = strat.CalculateDeliveryPrice(order, subtotal: 1000m);
            Assert.Equal(250m, feeSmall);

            // large subtotal -> free
            var feeLarge = strat.CalculateDeliveryPrice(order, subtotal: 1600m);
            Assert.Equal(0m, feeLarge);
        }
    }
}
