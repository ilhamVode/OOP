using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Strategies;

public class UpgradeStrategy : IUpgradeStrategy
{
    public bool Upgrade(Item target, Item item)
    {
        switch (target)
        {
            case Weapon w:
                if (item is Weapon uw)
                {
                    int increase = (int)Math.Round(uw.Damage * 0.3);
                    w.Damage += increase;
                    return true;
                }
                break;

            case Armour a:
                if (item is Armour ua)
                {
                    int increase = (int)Math.Round(ua.Defense * 0.4);
                    a.Defense += increase;
                    return true;
                }
                break;

            case Potion p:
                if (item is Potion up && p.EffectType == up.EffectType)
                {
                    int increase = (int)Math.Round(up.Value * 0.6);
                    p.Value += increase;
                    return true;
                }
                break;
        }
        return false;
    }
}
