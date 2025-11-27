using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4.Models.Orders;

namespace Lab4.Patterns.Observer;

public class CustomerNotifier : IOrderObserver
{
    public void OnOrderStateChanged(BaseOrder order)
    {
        Console.WriteLine($"[Уведомление] Order {order.Id} changed state to {order.StateName} for customer {order.Customer.Name}");
    }
}