using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Strategy.Delivery;

public class StandartDeliveryStrategy : IDeliveryPriceStrategy
{
    private readonly decimal _standardDeliveryCost;

    public StandartDeliveryStrategy(decimal standardDeliveryCost = 250m)
    {
        if (standardDeliveryCost < 0) Console.WriteLine("[Ошибка] Цена доставки не может быть отрицательной");
        _standardDeliveryCost = standardDeliveryCost;
    }

    public decimal CalculateDeliveryPrice(BaseOrder order, decimal subtotal)
        => _standardDeliveryCost;
}