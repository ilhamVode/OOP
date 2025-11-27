using Lab4.Models;
using Lab4.Models.Orders;
using Lab4.Patterns.Strategy.Delivery;
using Lab4.Patterns.Strategy.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Patterns.Factory;

public static class OrderFactory
{
    public static BaseOrder Create(OrderType type, string id, Customer customer, IEnumerable<Product> items)
    {
        if (customer == null) throw new ArgumentNullException(nameof(customer));
        if (items == null) throw new ArgumentNullException(nameof(items));

        // Создаём базовый объект заказа (конкретный тип)
        BaseOrder order = type switch
        {
            OrderType.Standard => new StandardOrder(customer, items),
            OrderType.Express => new ExpressOrder(customer, items),
            _ => throw new ArgumentOutOfRangeException(nameof(type), "Неизвестный тип заказа")
        };

        // Назначаем стратегии по умолчанию (можно заменить в коде вызывающем)
        // Скидка: 10% при сумме >= 1000 (ThresholdDiscountStrategy или любая твоя реализация)
        order.DiscountStrategy = new AmountDiscountStrategy(minimumAmount: 1000m, percent: 0.10m);

        // Доставка: бесплатная при сумме >=1500, иначе стандарт / экспресс в зависимости от типа
        if (type == OrderType.Standard)
        {
            order.DeliveryPriceStrategy = new BigOrderFreeDeliveryStrategy(
                standardDeliveryCost: 1500m,
                fallback: new StandartDeliveryStrategy(standardDeliveryCost: 250m));
        }
        else // Express
        {
            order.DeliveryPriceStrategy = new BigOrderFreeDeliveryStrategy(
                standardDeliveryCost: 1500m,
                fallback: new ExpressDeliveryStrategy());
        }

        return order;
    }
}
