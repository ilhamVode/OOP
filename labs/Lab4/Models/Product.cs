using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models;

public class Product
{
    private static int _id;
    public int Id { get; }
    public string Name { get; private set; }
    public decimal Price { get; private set; }
    public string Description { get; private set; }

    public Product(string name, decimal price, string description)
    {
        Id = _id++;
        Name = name;
        Price = price;
        Description = description;
    }
}
