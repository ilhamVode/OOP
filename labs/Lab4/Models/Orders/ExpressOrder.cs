using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models.Orders;

public class ExpressOrder : BaseOrder
{
    public ExpressOrder(Customer customer, IEnumerable<Product> items)
        : base(customer, items, null, null)
    {

    }
}