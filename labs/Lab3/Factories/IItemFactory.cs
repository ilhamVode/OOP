using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Factories;

public interface IItemFactory
{
    Weapon CreateWeapon(string name = null, int damage = 0);
    Armour CreateArmor(string name = null, int defense = 0);
    QuestItem CreateQuestItem(string name = null, string description = null);
    Potion CreatePotion(string? name = null, PotionType type = PotionType.None, int value = 0);
}
