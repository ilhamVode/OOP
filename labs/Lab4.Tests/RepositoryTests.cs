using Xunit;
using Lab4.Patterns.Singleton;
using Lab4.Models;
using Lab4.Models.Orders;
using Lab4.Patterns.Factory;
using System.Collections.Generic;

namespace Lab4.Tests
{
    public class RepositoryTests
    {
        [Fact]
        public void InMemoryOrderRepository_SaveGetDelete_Works()
        {
            // Arrange
            var repo = InMemoryOrderRepository.Instance;
            repo.Clear();
            var customer = new Customer("C", "A", "P");
            var items = new[] { new Product("X", 10m, "") };
            var order = OrderFactory.Create(OrderType.Standard, "repo1", customer, items);

            // Act
            repo.Save(order);
            var fetched = repo.Get(order.Id);
            var all = new List<BaseOrder>(repo.GetAll());

            // Assert
            Assert.NotNull(fetched);
            Assert.Equal(order.Id, fetched!.Id);
            Assert.Contains(order, all);

            // Act - delete
            var deleted = repo.Delete(order.Id);

            // Assert
            Assert.True(deleted);
            Assert.Null(repo.Get(order.Id));
        }
    }
}
