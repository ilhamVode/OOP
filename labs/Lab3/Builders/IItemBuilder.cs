using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Builders;

public interface IItemBuilder<T> where T : Item
{
    void Reset();
    void SetName(string name);
    void SetSpecificProperty(int value);
    T GetResult();
}