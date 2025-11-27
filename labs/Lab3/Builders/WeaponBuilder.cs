using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Builders;

public class WeaponBuilder : IItemBuilder<Weapon>
{
    private Weapon _weapon = new Weapon("Default", 0);

    public void Reset() => _weapon = new Weapon("Default", 0);
    public void SetName(string name) => _weapon.Name = name;
    public void SetSpecificProperty(int damage) => _weapon.Damage = damage;
    public Weapon GetResult() => _weapon;
}