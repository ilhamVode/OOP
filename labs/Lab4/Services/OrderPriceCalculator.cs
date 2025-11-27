using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Services;

public class OrderPriceCalculator
{
    private readonly decimal _taxRate;

    public OrderPriceCalculator(decimal taxRate = 0.22m)
    {
        if (taxRate < 0m || taxRate > 1m) throw new ArgumentOutOfRangeException(nameof(taxRate));
        _taxRate = taxRate;
    }

    public decimal CalculateTotal(BaseOrder order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));

        // Subtotal — сумма цен продуктов
        decimal subtotal = order.Items.Sum(p => p.Price);

        // Скидка — через стратегию (если null то 0)
        decimal discount = order.DiscountStrategy?.CalculateDiscount(order, subtotal) ?? 0m;
        decimal taxedBase = Math.Round(subtotal - discount, 2);

        // Налог
        decimal tax = Math.Round(taxedBase * _taxRate, 2);

        // Доставка — через стратегию (если null то 0)
        decimal delivery = order.DeliveryPriceStrategy?.CalculateDeliveryPrice(order, taxedBase) ?? 0m;

        decimal total = Math.Round(taxedBase + tax + delivery, 2);
        return total;
    }
}