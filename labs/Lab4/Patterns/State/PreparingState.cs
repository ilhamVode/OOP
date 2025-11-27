using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.State;

public class PreparingState : IOrderState
{
    public string Name => "Preparing";
    public void Next(BaseOrder order)
    {
        order.SetState(new DeliveringState());
    }
}
