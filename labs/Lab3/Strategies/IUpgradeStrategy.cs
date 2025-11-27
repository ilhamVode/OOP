using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Strategies;
public interface IUpgradeStrategy
{
    bool Upgrade(Item target, Item item);
}
