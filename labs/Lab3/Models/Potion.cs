using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public class Potion : Item, IConsumable
{
    public PotionType EffectType { get; }
    public int Value { get; set; }

    public Potion(string name, PotionType effectType, int value): base(name, ItemType.Potion)
    {
        EffectType = effectType;
        Value = value;
    }

    public void Consume()
    {
        switch (EffectType)
        {
            case PotionType.Health:
                Console.WriteLine($"Восстановлено {Value} HP");
                break;
            
            case PotionType.Speed:
                Console.WriteLine($"Скорость увеличена на {Value}%");
                break;
        }
    }

    public override void Use()
    {
        Consume();
    }
}