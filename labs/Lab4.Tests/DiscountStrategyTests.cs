using Xunit;
using Lab4.Patterns.Strategy.Discount;
using Lab4.Models;
using Lab4.Models.Orders;

namespace Lab4.Tests
{
    public class DiscountStrategyTests
    {
        [Fact]
        public void AmountDiscountStrategy_BelowThreshold_ReturnsZero()
        {
            // Arrange
            var strat = new AmountDiscountStrategy(minimumAmount: 1000m, percent: 0.10m);
            var customer = new Customer("C", "A", "P");
            var items = new[] { new Product("X", 100m, "") };

            var order = new StandardOrder(customer, items);

            // Act
            var discount = strat.CalculateDiscount(order, subtotal: 500m);

            // Assert
            Assert.Equal(0m, discount);
        }

        [Fact]
        public void AmountDiscountStrategy_AtOrAboveThreshold_ReturnsPercent()
        {
            // Arrange
            var strat = new AmountDiscountStrategy(minimumAmount: 1000m, percent: 0.10m);
            var customer = new Customer("C", "A", "P");
            var items = new[] { new Product("X", 1200m, "") };
            var order = new StandardOrder(customer, items);

            // Act
            var discount = strat.CalculateDiscount(order, subtotal: 1200m);

            // Assert
            Assert.Equal(120.00m, discount); // 10% of 1200
        }
    }
}
