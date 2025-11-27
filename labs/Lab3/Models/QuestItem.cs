using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public class QuestItem : Item
{
    public string Description { get; }

    public QuestItem(string name, string description) : base(name, ItemType.Quest)
    {
        Description = description;
    }

    public override void Use()
    {
        Console.WriteLine(Description);
    }
}
