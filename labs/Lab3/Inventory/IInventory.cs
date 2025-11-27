using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Inventory;

public interface IInventory
{
    int Count {  get; }

    void AddItem(Item item);
    Weapon CreateWeapon(string name, int damage);
    Potion CreatePotion(string name, PotionType type, int value);
    Armour CreateArmour(string name, int value);
    bool RemoveItem(int itemId);
    Item GetItem(int itemId);
    void ShowAllItems();
    void ShowEquippedItems();
    void UseItem(int itemId);
    bool UpgradeItem(int targetItemId, int usedItemId);
}