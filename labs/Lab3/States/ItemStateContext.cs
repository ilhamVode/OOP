using Lab3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.States;

public class ItemStateContext
{
    private IItemState _currentState;
    public ItemStateContext()
    {
        _currentState = new UnequippedState();
    }

    public string CurrentStateName => _currentState.Name;

    public void SetState(IItemState state)
    {
        _currentState = state;
    }

    public void Use(Item item)
    {
        var newState = _currentState.ProcessUse(item);
        _currentState = newState;
    }
}