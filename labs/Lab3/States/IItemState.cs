using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.States;

public interface IItemState
{
    string Name { get; }
    IItemState ProcessUse(Item item);
}
