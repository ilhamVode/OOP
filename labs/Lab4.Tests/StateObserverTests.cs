using System.Collections.Generic;
using Xunit;
using Lab4.Models;
using Lab4.Models.Orders;
using Lab4.Patterns.Observer;
using Lab4.Patterns.Factory;

namespace Lab4.Tests
{
    public class StateObserverTests
    {
        private class TestObserver : IOrderObserver
        {
            public readonly List<string> States = new();
            public void OnOrderStateChanged(BaseOrder order) => States.Add(order.StateName);
        }

        [Fact]
        public void Order_NextState_NotifiesObservers_AndTransitions()
        {
            // Arrange
            var customer = new Customer("C", "A", "P");
            var items = new[] { new Product("X", 10m, "") };
            var order = OrderFactory.Create(OrderType.Standard, "o-st", customer, items);

            var obs = new TestObserver();
            order.AddObserver(obs);

            // Act
            order.NextState(); // Preparing -> Delivering
            order.NextState(); // Delivering -> Completed

            // Assert
            Assert.Equal("Completed", order.StateName);
            Assert.Equal(2, obs.States.Count);
            Assert.Equal("Delivering", obs.States[0]);
            Assert.Equal("Completed", obs.States[1]);
        }
    }
}
