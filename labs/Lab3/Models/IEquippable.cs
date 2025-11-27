using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Models;

public interface IEquippable
{
    bool IsEquipped { get; set; }
    void Equip();
    void Unequip();
}
