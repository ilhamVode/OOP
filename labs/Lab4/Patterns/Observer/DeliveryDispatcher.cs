using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Observer;

public class DeliveryDispatcher : IOrderObserver
{
    public void OnOrderStateChanged(BaseOrder order)
    {
        Console.WriteLine($"[Уведомление] Order {order.Id} is now {order.StateName}. Dispatcher may react accordingly.");
    }
}