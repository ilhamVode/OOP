using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab4.Models.Orders;

namespace Lab4.Patterns.Strategy.Discount;

public interface IDiscountStrategy
{
    decimal CalculateDiscount(BaseOrder order, decimal subtotal);
}