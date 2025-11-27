using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public abstract class Item
{
    private static int _lastId = 0; // для автоинкремента
    public int Id { get; }
    public string Name { get; set; }
    public ItemType Type { get; protected set; }

    protected Item(string name, ItemType type)
    {
        _lastId++;
        Id = _lastId;
        Name = name; 
        Type = type;
    }

    public abstract void Use();

}
