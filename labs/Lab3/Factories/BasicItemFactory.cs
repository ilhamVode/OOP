using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Factories;

public class BasicItemFactory : IItemFactory
{
    public Weapon CreateWeapon(string? name = null, int damage = 0)
    {
        var finalName = !string.IsNullOrWhiteSpace(name) ? name : "Basic Weapon";
        return new Weapon(finalName, damage);
    }

    public Armour CreateArmor(string? name = null, int defense = 0)
    {
        var finalName = !string.IsNullOrWhiteSpace(name) ? name : "Basic Armor";
        return new Armour(finalName, defense);
    }

    public Potion CreatePotion(string? name = null, PotionType type = PotionType.None, int value = 0)
    {
        var finalName = !string.IsNullOrWhiteSpace(name) ? name : type switch
        {
            PotionType.Health => "Health Potion",
            PotionType.Speed => "Speed Potion",
            _ => "Basic Potion"
        };
        return new Potion(finalName, type, value);
    }

    public QuestItem CreateQuestItem(string? name = null, string? description = null)
    {
        var realName = !string.IsNullOrWhiteSpace(name) ? name : "Basic QuestItem";
        var realDescription = !string.IsNullOrWhiteSpace(description) ? description! : "It's QuestItem";
        return new QuestItem(realName, realDescription);
    }
}