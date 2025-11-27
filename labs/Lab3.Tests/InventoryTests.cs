using Xunit;
using Lab3.Factories;
using Lab3.Inventory;
using Lab3.Strategies;
using Lab3.Models;

namespace Lab3.Tests;

public class InventoryTests
{
    [Fact]
    public void Add_Get_Remove_WorkCorrectly()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var w = inv.CreateWeapon("Dagger", 3);
        Assert.Equal(1, inv.Count);
        var fetched = inv.GetItem(w.Id);
        Assert.NotNull(fetched);
        Assert.Equal(w.Id, fetched.Id);

        // remove
        var removed = inv.RemoveItem(w.Id);
        Assert.True(removed);
        Assert.Equal(0, inv.Count);
        Assert.Null(inv.GetItem(w.Id));
    }

    [Fact]
    public void UsePotion_ConsumesAndRemovesFromInventory()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var p = inv.CreatePotion("HP", PotionType.Health, 50);
        Assert.Equal(1, inv.Count);

        inv.UseItem(p.Id);
        Assert.Equal(0, inv.Count);
        Assert.Null(inv.GetItem(p.Id));
    }

    [Fact]
    public void UseWeapon_EquipsItem_StateReflected()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var w = inv.CreateWeapon("Sword", 5);

 
        Assert.False(((IEquippable)w).IsEquipped);

 
        inv.UseItem(w.Id);

        var fetched = inv.GetItem(w.Id);
        Assert.NotNull(fetched);
        Assert.True(((IEquippable)fetched).IsEquipped);
    }

    [Fact]
    public void UpgradeItem_Success_RemovesSourceAndIncreasesTarget()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var src = inv.CreateWeapon("Source", 10);
        var tgt = inv.CreateWeapon("Target", 2);

        bool ok = inv.UpgradeItem(tgt.Id, src.Id);
        Assert.True(ok);

        var updated = (Weapon)inv.GetItem(tgt.Id);
        int expected = (int)System.Math.Round(10 * 0.3);
        Assert.Equal(2 + expected, updated.Damage);

        Assert.Null(inv.GetItem(src.Id));
    }

    [Fact]
    public void UpgradeItem_Incompatible_ReturnsFalseAndKeepsItems()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var w = inv.CreateWeapon("Sword", 5);
        var p = inv.CreatePotion("HP", PotionType.Health, 20);

        bool ok = inv.UpgradeItem(w.Id, p.Id);
        Assert.False(ok);

        Assert.NotNull(inv.GetItem(w.Id));
        Assert.NotNull(inv.GetItem(p.Id));
    }
}
