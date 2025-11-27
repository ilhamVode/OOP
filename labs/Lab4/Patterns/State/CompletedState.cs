using Lab4.Models.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.State;

public class CompletedState : IOrderState
{
    public string Name => "Completed";
    public void Next(BaseOrder order)
    {
        Console.WriteLine("[Ошибка] Заказ находится в конечном состоянии!");
    }
}