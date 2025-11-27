using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public class Weapon : Item, IEquippable
{
    public int Damage { get; set; }
    public bool IsEquipped { get; set; }
    public Weapon(string name, int damage) : base(name, ItemType.Weapon)
    {
        Damage = damage;
    }
    public override void Use()
    {
        if (IsEquipped) { Unequip(); }
        else { Equip(); }

    }

    public void Equip()
    {
        IsEquipped = true;
    }

    public void Unequip() => IsEquipped = false;
}