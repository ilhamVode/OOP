using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Strategy.Discount;

public class NoDiscountStrategy : IDiscountStrategy
{
    public decimal CalculateDiscount(BaseOrder order, decimal subtotal) => 0m;
}
