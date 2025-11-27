using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using Lab4.Models.Orders;

namespace Lab4.Patterns.Singleton;

public sealed class InMemoryOrderRepository
{
    private static readonly InMemoryOrderRepository _instance = new InMemoryOrderRepository();

    public static InMemoryOrderRepository Instance => _instance;

    private readonly Dictionary<int, BaseOrder> _store = new();

    private InMemoryOrderRepository() { }

    public void Save(BaseOrder order)
    {
        if (order == null) throw new ArgumentNullException(nameof(order));
        _store[order.Id] = order;
    }

    public BaseOrder? Get(int id)
    {
        return _store.TryGetValue(id, out var order) ? order : null;
    }

    public IEnumerable<BaseOrder> GetAll()
    {
        return _store.Values;
    }

    public bool Delete(int id)
    {
        return _store.Remove(id);
    }

    public void Clear()
    {
        _store.Clear();
    }
}
