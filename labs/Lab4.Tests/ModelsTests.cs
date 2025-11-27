using System.Collections.ObjectModel;
using Xunit;
using Lab4.Models;
using Lab4.Models.Orders;
using System.Linq;

namespace Lab4.Tests
{
    public class ModelsTests
    {
        [Fact]
        public void BaseOrder_Items_Are_ReadOnlyCollection()
        {
            // Arrange
            var customer = new Customer("C", "A", "P");
            var items = new[] { new Product("X", 10m, ""), new Product("Y", 20m, "") };
            var order = new StandardOrder(customer, items);

            // Act
            var itemsType = order.Items.GetType();

            // Assert
            Assert.IsAssignableFrom<IReadOnlyList<Product>>(order.Items);
            Assert.IsType<ReadOnlyCollection<Product>>(order.Items);
            Assert.Equal(2, order.Items.Count);
            Assert.Equal(10m + 20m, order.Items.Sum(i => i.Price));
        }
    }
}
