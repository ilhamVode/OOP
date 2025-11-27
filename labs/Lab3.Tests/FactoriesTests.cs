
using Xunit;
using Lab3.Factories;
using Lab3.Models;

namespace Lab3.Tests
{
    public class FactoriesTests
    {
        [Fact]
        public void CreateWeapon_ReturnsWeapon_WithCorrectProperties()
        {
            var factory = new BasicItemFactory();
            var w = factory.CreateWeapon("TestSword", 8);

            Assert.NotNull(w);
            Assert.IsType<Weapon>(w);
            Assert.Equal("TestSword", w.Name);
            Assert.Equal(8, w.Damage);
        }

        [Fact]
        public void CreatePotion_ReturnsPotion_WithTypeAndValue()
        {
            var factory = new BasicItemFactory();
            var p = factory.CreatePotion("Heal", PotionType.Health, 30);

            Assert.NotNull(p);
            Assert.IsType<Potion>(p);
            Assert.Equal("Heal", p.Name);
            Assert.Equal(PotionType.Health, p.EffectType);
            Assert.Equal(30, p.Value);
        }

        [Fact]
        public void CreateArmor_ReturnsArmor_WithDefense()
        {
            var factory = new BasicItemFactory();
            var a = factory.CreateArmor("Leather", 5);

            Assert.NotNull(a);
            Assert.IsType<Armour>(a);
            Assert.Equal("Leather", a.Name);
            Assert.Equal(5, a.Defense);
        }

        [Fact]
        public void CreatedItems_HaveUniqueIds()
        {
            var factory = new BasicItemFactory();
            var a = factory.CreateWeapon("A", 1);
            var b = factory.CreateWeapon("B", 2);

            Assert.NotEqual(a.Id, b.Id);
        }
    }


}