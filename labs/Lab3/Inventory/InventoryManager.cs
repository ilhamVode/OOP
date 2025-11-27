using Lab3.Builders;
using Lab3.Factories;
using Lab3.Models;
using Lab3.States;
using Lab3.Strategies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Inventory;

public class InventoryManager : IInventory
{
    private readonly List<Item> _items;
    private readonly IUpgradeStrategy _upgradeStrategy;
    private readonly Dictionary<int, ItemStateContext> _itemStates;


    private readonly IItemFactory _itemFactory;

    public InventoryManager(IUpgradeStrategy upgradeStrategy, IItemFactory itemFactory)
    {
        _items = new List<Item>();
        _upgradeStrategy = upgradeStrategy;
        _itemStates = new Dictionary<int, ItemStateContext>();

        _itemFactory = itemFactory;
    }

    public int Count => _items.Count;

    public void AddItem(Item item)
    {
        if (item == null) return;

        _items.Add(item);
        _itemStates[item.Id] = new ItemStateContext();
        Console.WriteLine($"Добавлен предмет: {item.Name} (ID: {item.Id})");
    }

    public Weapon CreateWeapon(string name, int damage)
    {
        var weapon = _itemFactory.CreateWeapon(name, damage);
        AddItem(weapon);
        return weapon;
    }

    public Potion CreatePotion(string name, PotionType type, int value)
    {
        var potion = _itemFactory.CreatePotion(name, type, value);
        AddItem(potion);
        return potion;
    }

    public Armour CreateArmour(string name, int value)
    {
        var armour = _itemFactory.CreateArmor(name, value);
        AddItem(armour);
        return armour;
    }

    public QuestItem CreateQuestItem(string name, string description)
    {
        var questitem = _itemFactory.CreateQuestItem(name, description);
        AddItem(questitem);
        return questitem;
    }

    public bool RemoveItem(int itemId)
    {

        var item = _items.FirstOrDefault(i => i.Id == itemId);

        if (item == null) return false;

        _items.Remove(item);
        _itemStates.Remove(itemId);
        Console.WriteLine($"Удален предмет: {item.Name} (ID: {itemId})");
        return true;
    }

    public Item GetItem(int itemId)
    {
        var item = _items.FirstOrDefault(i => i.Id == itemId);
        return item;
    }

    public void ShowAllItems()
    {
        Console.WriteLine("- / - Инвентарь - / -");
        foreach(var item in _items)
        {
            switch (item)
            {
                case Armour a:
                    Console.WriteLine($"{a.Type}: ID:{a.Id} | Name:{a.Name} | Защита: {a.Defense} | Состояние: {_itemStates[a.Id].CurrentStateName} | Экипировка: {a.IsEquipped}");
                    break;

                case Weapon w:
                    Console.WriteLine($"{w.Type}: ID:{w.Id} | Name: {w.Name} | Урон: {w.Damage} | Состояние: {_itemStates[w.Id].CurrentStateName} | Экипировка: {w.IsEquipped}");
                    break;

                case Potion p:
                    Console.WriteLine($"{p.Type}: ID:{p.Id} | Name: {p.Name} | {p.EffectType}: {p.Value} | Состояние: {_itemStates[p.Id].CurrentStateName}");
                    break;

                case QuestItem q:
                    Console.WriteLine($"{q.Type}: ID:{q.Id} | Name: {q.Name} |  {q.Description} | Состояние: {_itemStates[q.Id].CurrentStateName} |");
                    break;

                default:
                    Console.WriteLine($"{item.Type}: ID:{item.Id} | Name:{item.Name} | Состояние: {_itemStates[item.Id].CurrentStateName}");
                    break;
            }
        }
        Console.WriteLine("/ - / Инвентарь / - /");
    }

    public void ShowEquippedItems()
    {
        var equippedItems = _items.Where(item => item is IEquippable equippable && equippable.IsEquipped);
        Console.WriteLine("Экипированные предметы");

        foreach (var item in equippedItems)
        {
            string itemType = item switch
            {
                Weapon _ => "Оружие",
                Armour _ => "Броня",
                _ => "Предмет"
            };
            Console.WriteLine($"   {itemType}: {item.Name}");
        }
    }

    public void UseItem(int itemId)
    {
        var item = GetItem(itemId);
        if (item == null) return;

        if (item is IConsumable consumable)
        {
            consumable.Consume();
            RemoveItem(itemId);
            return;
        }

        if (_itemStates.TryGetValue(itemId, out var state))
            state.Use(item);
    }

    public bool UpgradeItem(int targetItemId, int usedItemId)
    {
        var target = GetItem(targetItemId);
        var usedItem = GetItem(usedItemId);

        if (target == null || usedItem == null)
        {
            Console.WriteLine("Один из предметов для улучшения не найден");
            return false;
        }

        Console.WriteLine($"Улучшаем {target.Name} с помощью {usedItem.Name}");

        bool success = _upgradeStrategy.Upgrade(target, usedItem);
        if (success)
        {
            RemoveItem(usedItemId);
            Console.WriteLine($"Успешно улучшен: {target.Name}");
        }
        else
        {
            Console.WriteLine($"Не удалось улучшить предмет");
        }

        return success;
    }
}