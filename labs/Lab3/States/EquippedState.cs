using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.States;

public class EquippedState : IItemState
{
    public string Name => "Экипирован";

    public IItemState ProcessUse(Item item)
    {
        if (item is IEquippable equippable)
        {
            equippable.Unequip();
            return new UnequippedState();
        }
        return this;
    }
}
