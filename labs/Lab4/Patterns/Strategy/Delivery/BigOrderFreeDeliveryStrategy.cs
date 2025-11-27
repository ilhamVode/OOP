using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Strategy.Delivery;

public class BigOrderFreeDeliveryStrategy : IDeliveryPriceStrategy
{
    private readonly decimal _standardDeliveryCost;
    private readonly IDeliveryPriceStrategy _fallback;

    public BigOrderFreeDeliveryStrategy(decimal standardDeliveryCost, IDeliveryPriceStrategy fallback)
    {
        _standardDeliveryCost = standardDeliveryCost;
        _fallback = fallback;
    }

    public decimal CalculateDeliveryPrice(BaseOrder order, decimal subtotal)
    {
        if (subtotal >= _standardDeliveryCost)
            return 0m;

        return _fallback.CalculateDeliveryPrice(order, subtotal);
    }
}