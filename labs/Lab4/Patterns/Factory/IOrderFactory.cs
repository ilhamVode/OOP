using Lab4.Models;
using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Factory;

public interface IOrderFactory
{
    BaseOrder Create(OrderType type, string id, Customer customer, IEnumerable<Product> items);
}