using Xunit;
using Lab3.Factories;
using Lab3.Strategies;
using Lab3.Models;

namespace Lab3.Tests;
public class StrategiesTests
{
    [Fact]
    public void UpgradeStrategy_Weapon_IncreasesByThirtyPercent()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();

        var source = factory.CreateWeapon("Src", 12);
        var target = factory.CreateWeapon("Tgt", 3);

        bool ok = strat.Upgrade(target, source);
        Assert.True(ok);

        int expected = (int)System.Math.Round(12 * 0.3);
        Assert.Equal(3 + expected, target.Damage);
    }

    [Fact]
    public void UpgradeStrategy_Armor_IncreasesByFortyPercent()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();

        var source = factory.CreateArmor("SrcArmor", 10);
        var target = factory.CreateArmor("TgtArmor", 4);

        bool ok = strat.Upgrade(target, source);
        Assert.True(ok);

        int expected = (int)System.Math.Round(10 * 0.4);
        Assert.Equal(4 + expected, target.Defense);
    }

    [Fact]
    public void UpgradeStrategy_Incompatible_ReturnsFalse()
    {
        var factory = new BasicItemFactory();
        var strat = new UpgradeStrategy();

        var source = factory.CreatePotion("P", PotionType.Health, 10);
        var target = factory.CreateWeapon("W", 3);

        bool ok = strat.Upgrade(target, source);
        Assert.False(ok);
    }
}

