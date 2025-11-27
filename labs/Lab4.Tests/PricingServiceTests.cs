using Xunit;
using Lab4.Services;
using Lab4.Models;
using Lab4.Patterns.Factory;
using System.Collections.Generic;

namespace Lab4.Tests
{
    public class PricingServiceTests
    {
        [Fact]
        public void Calculator_SmallOrder_NoDiscount_PaysDeliveryAndTax()
        {
            // Arrange
            var calc = new OrderPriceCalculator(taxRate: 0.22m);
            var customer = new Customer("C", "A", "P");
            var items = new List<Product> { new Product("A", 300m, ""), new Product("B", 200m, "") }; // subtotal 500
            var order = OrderFactory.Create(OrderType.Standard, "p1", customer, items);

            // Act
            var total = calc.CalculateTotal(order);

            // Assert
            // subtotal 500, discount 0, taxedBase 500, tax 110, delivery fallback 250 -> total = 500 + 110 + 250 = 860
            Assert.Equal(860.00m, total);
        }

        [Fact]
        public void Calculator_Order_AtDiscountThreshold_AppliesDiscount()
        {
            // Arrange
            var calc = new OrderPriceCalculator(taxRate: 0.22m);
            var customer = new Customer("C", "A", "P");
            var items = new List<Product> { new Product("A", 1200m, "") }; // subtotal 1200
            var order = OrderFactory.Create(OrderType.Standard, "p2", customer, items);

            // Act
            var total = calc.CalculateTotal(order);

            // Assert
            // subtotal 1200, discount 120 (10%), taxedBase 1080, tax 237.60, delivery fallback 250 -> total = 1080 + 237.60 + 250 = 1567.60
            Assert.Equal(1567.60m, total);
        }

        [Fact]
        public void Calculator_Order_AboveFreeDeliveryThreshold_NoDeliveryCharge()
        {
            // Arrange
            var calc = new OrderPriceCalculator(taxRate: 0.22m);
            var customer = new Customer("C", "A", "P");
            var items = new List<Product> { new Product("A", 1600m, "") }; // subtotal 1600
            var order = OrderFactory.Create(OrderType.Standard, "p3", customer, items);

            // Act
            var total = calc.CalculateTotal(order);

            // Assert
            // subtotal 1600, discount 160, taxedBase 1440, tax 316.80, delivery 0 => total = 1440 + 316.80 + 0 = 1756.80
            Assert.Equal(2006.80m, total);
        }
    }
}
