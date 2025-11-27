using System.Collections.Generic;
using Xunit;
using Lab4.Models;
using Lab4.Patterns.Factory;
using Lab4.Models.Orders;
using Lab4.Patterns.Strategy.Discount;
using Lab4.Patterns.Strategy.Delivery;

namespace Lab4.Tests
{
    public class FactoryTests
    {
        [Fact]
        public void CreateStandardOrder_AssignsConcreteTypeAndDefaultStrategies()
        {
            // Arrange
            var customer = new Customer("Ivan", "Addr", "123");
            var items = new List<Product> { new Product("P1", 100m, ""), new Product("P2", 400m, "") };

            // Act
            var order = OrderFactory.Create(OrderType.Standard, "o1", customer, items);

            // Assert
            Assert.NotNull(order);
            Assert.IsType<StandardOrder>(order);
            Assert.IsType<AmountDiscountStrategy>(order.DiscountStrategy);
            Assert.IsType<BigOrderFreeDeliveryStrategy>(order.DeliveryPriceStrategy);
        }

        [Fact]
        public void CreateExpressOrder_AssignsConcreteTypeAndDefaultStrategies()
        {
            // Arrange
            var customer = new Customer("Anna", "Addr", "456");
            var items = new List<Product> { new Product("P1", 50m, "") };

            // Act
            var order = OrderFactory.Create(OrderType.Express, "o2", customer, items);

            // Assert
            Assert.NotNull(order);
            Assert.IsType<ExpressOrder>(order);
            Assert.IsType<AmountDiscountStrategy>(order.DiscountStrategy);
            Assert.IsType<BigOrderFreeDeliveryStrategy>(order.DeliveryPriceStrategy);
        }
    }
}
