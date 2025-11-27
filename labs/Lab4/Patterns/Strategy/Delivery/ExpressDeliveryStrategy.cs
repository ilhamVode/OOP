using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Strategy.Delivery;

public class ExpressDeliveryStrategy : IDeliveryPriceStrategy
{
    public decimal CalculateDeliveryPrice(BaseOrder order, decimal subtotal)
    {
        return Math.Max(400m, Math.Round(subtotal * 0.7m, 2));
    }
}
