using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.States;

public class UnequippedState : IItemState
{
    public string Name => "Не экипирован";

    public IItemState ProcessUse(Item item)
    {
        if (item is IEquippable equippable)
        {
            equippable.Equip();                    // Экипируем
            return new EquippedState();           // Меняем состояние
        }

        return this;
    }
}