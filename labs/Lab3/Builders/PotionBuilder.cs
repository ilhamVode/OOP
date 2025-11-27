using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lab3.Models;

namespace Lab3.Builders;

public class PotionBuilder : IItemBuilder<Potion>
{
    private Potion _potion = new Potion("Unnamed", PotionType.None, 0);

    public void Reset() => _potion = new Potion("Unnamed", PotionType.None, 0);
    public void SetName(string name) => _potion.Name = name;
    public void SetSpecificProperty(int amount) => _potion.Value = amount;
    public Potion GetResult() => _potion;
}