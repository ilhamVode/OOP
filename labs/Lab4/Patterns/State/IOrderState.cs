using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Lab4.Models.Orders;

namespace Lab4.Patterns.State;

public interface IOrderState
{
    string Name { get; }
    void Next(BaseOrder order);
}