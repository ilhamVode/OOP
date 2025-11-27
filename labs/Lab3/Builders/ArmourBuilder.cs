using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Builders;

public class ArmourBuilder : IItemBuilder<Armour>
{
    private Armour _armour = new Armour("Unnamed", 0);

    public void Reset() => _armour = new Armour("Unnamed", 0);
    public void SetName(string name) => _armour.Name = name;
    public void SetSpecificProperty(int defense) => _armour.Defense = defense;
    public Armour GetResult() => _armour ;
}