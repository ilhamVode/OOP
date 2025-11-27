using Lab4.Patterns.Observer;
using Lab4.Patterns.State;
using Lab4.Patterns.Strategy.Delivery;
using Lab4.Patterns.Strategy.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Models.Orders;

public abstract class BaseOrder
{
    private static int _id;
    public int Id { get; }
    public Customer Customer { get; set; }
    public IReadOnlyList<Product> Items { get; }

    private IOrderState _state;
    private readonly List<IOrderObserver> _observers = new();

    public IDiscountStrategy DiscountStrategy { get; set; }
    public IDeliveryPriceStrategy DeliveryPriceStrategy { get; set; }

    protected BaseOrder(Customer customer, IEnumerable<Product> items,
            IDiscountStrategy? discountStrategy, IDeliveryPriceStrategy? deliveryPriceStrategy)
    {
        Id = _id++;
        Customer = customer;

        Items = items.ToList().AsReadOnly();

        DeliveryPriceStrategy = deliveryPriceStrategy ?? new StandartDeliveryStrategy();
        DiscountStrategy = discountStrategy ?? new NoDiscountStrategy();

        _state = new PreparingState();
    }

    public string StateName => _state.Name;

    public void NextState()
    {
        _state.Next(this);
        NotifyObservers();
    }

    protected void NotifyObservers()
    {
        var snapshot = _observers.ToArray();

        foreach (var obs in snapshot)
        {
            try
            {
                obs.OnOrderStateChanged(this);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Ошибка Observe] {obs.GetType().Name} {ex.Message}");
            }
        }
    }

    public void AddObserver(IOrderObserver observer)
    {
        if (observer == null) return;
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void DetachObserver(IOrderObserver observer)
    {
        if (observer == null) return;
        _observers.Remove(observer);
    }

    public void SetState(IOrderState newState)
    {
        _state = newState ?? throw new ArgumentNullException(nameof(newState));
    }
}