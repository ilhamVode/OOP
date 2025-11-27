using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public class Armour: Item, IEquippable
{
    public int Defense { get; set; }
    public bool IsEquipped { get; set; }

    public Armour(string name, int defense) : base(name, ItemType.Armour)
    {
        Defense = defense; 
    }

    public void Equip()
    {
        if (!IsEquipped)
        {
            IsEquipped = true;
        }
    }

    public void Unequip()
    {
        if (IsEquipped)
        {
            IsEquipped = false;
        }
    }

    public override void Use()
    {
        if (IsEquipped) { Unequip(); }
        else { Equip(); }
    }
}


