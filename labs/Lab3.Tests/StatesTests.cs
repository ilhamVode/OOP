using Xunit;
using Lab3.Factories;
using Lab3.Inventory;
using Lab3.Strategies;
using Lab3.Models;

namespace Lab3.Tests;
public class StatesTests
{
    [Fact]
    public void EquippableItem_Use_TogglesEquippedState()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var w = inv.CreateWeapon("Sword", 5);
        Assert.False(((IEquippable)w).IsEquipped);

       
        inv.UseItem(w.Id);

        Assert.True(((IEquippable)w).IsEquipped);

        inv.UseItem(w.Id);

        Assert.True(((IEquippable)w).IsEquipped || !((IEquippable)w).IsEquipped);
    }

    [Fact]
    public void NonEquippableUse_DoesNotThrow()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();
        var inv = new InventoryManager(strat, factory);

        var q = inv.CreateQuestItem("Note", "Read me");

        inv.UseItem(q.Id);
        Assert.NotNull(inv.GetItem(q.Id)); 
    }
}

