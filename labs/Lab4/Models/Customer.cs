using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models;
public class Customer
{
    private static int _id;
    public int Id { get; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }

    public Customer(string name, string address, string phone)
    {
        Id = _id++;
        Name = name;
        Address = address;
        Phone = phone;
    }
}

