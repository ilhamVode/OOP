using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4.Models.Orders;

namespace Lab4.Patterns.Strategy.Discount;

public class AmountDiscountStrategy : IDiscountStrategy
{
    private readonly decimal _minimumAmount;
    private readonly decimal _percent;

    public AmountDiscountStrategy(decimal minimumAmount, decimal percent)
    {
        if (minimumAmount < 0) throw new ArgumentOutOfRangeException(nameof(minimumAmount));
        if (percent < 0 || percent > 1) throw new ArgumentOutOfRangeException(nameof(percent));

        _minimumAmount = minimumAmount;
        _percent = percent;
    }

    public decimal CalculateDiscount(BaseOrder order, decimal subtotal)
    {
        if (subtotal >= _minimumAmount)
            return Math.Round(subtotal * _percent, 2);

        return 0m;
    }
}