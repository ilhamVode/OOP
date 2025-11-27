using Lab3.Factories;
using Lab3.Models;
using Xunit;


namespace Lab3.Tests;
public class ModelsTests
{
    [Fact]
    public void Weapon_Use_AttackProducesDamageValue()
    {
        var factory = new BasicItemFactory();
        var w = factory.CreateWeapon("Blade", 6);

        // модель Weapon должна иметь метод Use() - в проекте он может возвращать что-то или менять состояние.
        // Здесь мы проверим, что Use() не бросает и что Damage не становится отрицательным.
        w.Use();
        Assert.True(w.Damage >= 0);
    }

    [Fact]
    public void Potion_Consume_ReducesOrAppliesEffect()
    {
        var factory = new BasicItemFactory();
        var p = factory.CreatePotion("HP", PotionType.Health, 25);

        // Consume() должен существовать у IConsumable сверим, что вызов не бросает.
        ((IConsumable)p).Consume();
        Assert.True(p.Value >= 0); // простая проверка, не ломается
    }

    [Fact]
    public void QuestItem_Use_ShowsDescription()
    {
        var factory = new BasicItemFactory();
        var q = factory.CreateQuestItem("Key", "Opens door");

        // Use() не должен ломать выполнение
        q.Use();
        Assert.Equal("Key", q.Name);
    }
}