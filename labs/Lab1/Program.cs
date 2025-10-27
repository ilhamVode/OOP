using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("Lab1.Tests")]

namespace Lab1;


/// <summary>
/// Модель продукта: хранит имя, цену и количество.
/// </summary>
public class Product
{
    /// <summary>Имя продукта.</summary>
    public string Name { get; set; }

    /// <summary>Цена продукта.</summary>
    public decimal Price { get; set; }

    /// <summary>Количество продукта.</summary>
    public int Count { get; set; }

    /// <summary>
    /// Создаёт продукт.
    /// </summary>
    /// <param name="name">Имя продукта.</param>
    /// <param name="price">Цена продукта.</param>
    /// <param name="quantity">Количество продукта.</param>
    public Product(string name, decimal price, int quantity)
    {
        Name = name.Trim();
        Price = price;
        Count = quantity;
    }
}
/// <summary>
/// Хранилище продуктов: добавление, удаление,поиск по имени и формат списка продуктов.
/// </summary>
public class ProductStorage
{
    private List<Product> products = new();

    /// <summary>
    /// Добавляет продукт в хранилище. Если уже есть — увеличивает количество.
    /// Цена не учитывается, если добавляется продукт с существующим названием.
    /// </summary>
    /// <param name="product">Продукт для добавления.</param>
    /// <returns>Всегда true.</returns>
    public bool AddProduct(Product product)
    {
        Product? curr_product = GetProductByName(product.Name);
        if (curr_product == null)
        {
            products.Add(product);
        }
        else
        {
            curr_product.Count += product.Count;
        }
        
        return true;
    }

    /// <summary>
    /// Удаляет указанное количество продуктов из хранилища.
    /// </summary>
    /// <param name="product">Ссылка на экземпляр продукта.</param>
    /// <param name="count">Количество для удаления.</param>
    /// <returns>True при успешном удалении, false при ошибке.</returns>
    public bool RemoveProduct(Product? product, int count)
    {
        if (product != null && products.Contains(product))
        {
            if (product.Count >= count)
            {
                product.Count -= count;
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Возвращает количество разных наименований продуктов.
    /// </summary>
    /// <returns>Количество наименований.</returns>
    public int CountProducts()
    {
        return products.Count;
    }

    /// <summary>
    /// Проверяет наличие продукта по имени, регистронезависимо.
    /// </summary>
    /// <param name="name">Имя продукта.</param>
    /// <returns>True если продукт найден.</returns>
    public bool HasProductByName(string name)
    {
        return products.Any(product => product.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Получает продукт по имени, регистронезависимо.
    /// </summary>
    /// <param name="name">Имя продукта для поиска.</param>
    /// <returns>Экземпляр продукта или null, если не найден.</returns>
    public Product? GetProductByName(string name)
    {
        return products.FirstOrDefault(product => product.Name.Equals(name.Trim(), StringComparison.OrdinalIgnoreCase));
    }

    /// <summary>
    /// Строковое представление всех продуктов.
    /// </summary>
    /// <returns>Список продуктов в виде строки.</returns>
    public string GetProducts()
    {
        string info = string.Empty;
        for (int i = 0; i < products.Count; i++)
        {
            info += $"Продукт: {products[i].Name} | Количество: {products[i].Count} | Цена: {products[i].Price}\n";
        }
        return info;
    }
}

/// <summary>
/// Хранилище монет: номинал -> количество.
/// </summary>
public interface ICoinStorage
{
    /// <summary>
    /// Добавляет монеты заданного номинала.
    /// </summary>
    /// <param name="nominal">Номинал монеты.</param>
    /// <param name="count">Количество для добавления.</param>
    void AddCoins(decimal coin, int count);

    /// <summary>
    /// Удаляет указанное количество монет данного номинала, если возможно.
    /// </summary>
    /// <param name="nominal">Номинал монеты.</param>
    /// <param name="count">Количество для удаления.</param>
    /// <returns>True при успешном удалении, false при ошибке.</returns>
    bool RemoveCoins(decimal coin, int count);

    /// <summary>
    /// Возвращает количество монет указанного номинала.
    /// </summary>
    /// <param name="nominal">Номинал монеты.</param>
    /// <returns>Количество (0 если номинал отсутствует).</returns>
    int GetCoinCount(decimal coin);

    /// <summary>
    /// Возвращает список доступных номиналов.
    /// </summary>
    /// <returns>Список номиналов.</returns>
    List<decimal> GetCoinNominals();

    /// <summary>
    /// Строковое представление состояния кошелька.
    /// </summary>
    /// <returns>Текст с номиналами и количествами.</returns>
    string GetStorage();

    /// <summary>
    /// Сумма всех монет в хранилище.
    /// </summary>
    /// <returns>Сумма всех монет в хранилище.</returns>
    decimal GetSumAllCoins();
}

/// <summary>
/// Хранилище монет: номинал -> количество.
/// </summary>
public class CoinStorage : ICoinStorage
{
    /// <summary>Словарь номиналов и их количества.</summary>
    public Dictionary<decimal, int> Coins { get; set; } = new();
    public void AddCoins(decimal nominal, int count)
    {
        if (!Coins.TryAdd(nominal, count))
        {
            Coins[nominal] += count;
        }
    }
    public int GetCoinCount(decimal nominal)
    {
        if (Coins.TryGetValue(nominal, out int value))
        {
            return value;
        }
        else
        {
            return 0;
        }
    }
    public bool RemoveCoins(decimal nominal, int count)
    {
        if (Coins.TryGetValue(nominal, out _))
        {
            if (Coins[nominal] >= count)
            {
                Coins[nominal] -= count;
                return true;
            }
        }
        return false;
    }
    public List<decimal> GetCoinNominals()
    {
    return [.. Coins.Keys];
    }
    public string GetStorage()
    {
        string storage = "CoinStorage:\n";

        foreach (var kvp in Coins)
        {
        storage += $"Номинал: {kvp.Key} | Количество: {kvp.Value}\n";
        }
        return storage;
    }
    public decimal GetSumAllCoins()
    {
        decimal summ_coins = 0;
        foreach(var kvp in Coins)
        {
            summ_coins += kvp.Key* kvp.Value;
        }
        return summ_coins;
    }
}

/// <summary>
/// Основная логика вендинговой машины: операции покупки, возврата и администрирования монет и продуктов.
/// </summary>
public interface IVendingMachine
{
    /// <summary>
    /// Выполняет попытку покупки товара по имени.
    /// </summary>
    /// <param name="productname">Имя товара.</param>
    /// <returns>Словарь сдачи (номинал -> количество) или null при ошибке/недостатке средств/товара или невозможности сдачи.</returns>
    Dictionary<decimal, int>? BuyProduct(string productName);
    
    /// <summary>
    /// Вставляет монеты во временный буфер.
    /// </summary>
    /// <param name="nominal">Номинал.</param>
    /// <param name="count">Количество.</param>
    void InsertCoinsToBuffer(decimal coin, int count);

    /// <summary>
    /// Отменяет текущую операцию: возвращает монеты из буфера.
    /// </summary>
    /// <returns>Словарь номиналов и количеств из буфера.</returns>
    Dictionary<decimal, int> CancelOperation();

    /// <summary>
    /// [ADM] Снимает все монеты из кошелька автомата.
    /// </summary>
    /// <returns>Словарь собранных монет.</returns>
    Dictionary<decimal, int> CollectMoney();

    /// <summary>
    /// Добавляет продукт в автомат (публичный метод-обёртка).
    /// </summary>
    /// <param name="product">Продукт для добавления.</param>
    void AddProduct(Product product);

    /// <summary>
    /// [ADM] Добавляет монеты в кошелёк автомата.
    /// </summary>
    /// <param name="nominal">Номинал.</param>
    /// <param name="count">Количество.</param>
    void AddCoinsToVM(decimal nominal, int count);

    /// <summary>
    /// Строковое представление списка продуктов автомата (обертка).
    /// </summary>
    /// <returns>Список продуктов.</returns>
    string GetProducts();
}

/// <summary>
/// Основная логика вендинговой машины: операции покупки, возврата и администрирования монет и продуктов.
/// </summary>
public class VendingMachine : IVendingMachine
{
    private CoinStorage VendingWallet { get; set; } = new();
    private ProductStorage VendingProducts { get; set; } = new();
    private CoinStorage VendingBuffer { get; set; } = new();
    public void AddProduct(Product product)
    {
        VendingProducts.AddProduct(product);
    }
    public Dictionary<decimal, int>? BuyProduct(string productname)
    { 
        // Проверка на существование товара по заданному имени
        if (!VendingProducts.HasProductByName(productname)) return null;

        Product? product = VendingProducts.GetProductByName(productname);

        // Очень странная проверка, написана чтобы visual studio не жаловалась
        if (product == null) return null;

        decimal paidSumm = VendingBuffer.GetSumAllCoins();

        // Проверка на наличие товара и достаточно ли внесенных средств для оплаты
        if (product.Price > paidSumm || product.Count == 0) return null; 

        decimal change = paidSumm - product.Price;

        Dictionary<decimal, int> dict_change = GetChange(change);

        if (change > 0)
        {
            
            // Если не удалось сформировать сдачу, сдача обязательно должна быть т.к. change > 0
            if (dict_change.Count == 0) return null;
            
            foreach (var coin in dict_change)
            {
                // формирование количества монет, которая будет снята с буффера
                int changeFromBuffer = Math.Min(VendingBuffer.GetCoinCount(coin.Key), coin.Value);


                VendingBuffer.RemoveCoins(coin.Key, changeFromBuffer);

                // остаток сдачи снимаем с кошелька автомата
                VendingWallet.RemoveCoins(coin.Key, coin.Value - changeFromBuffer);
            }
        }

        // Перенос всех монет с буффера в кошелек автомата
        TransferAllCoinsFromBufferToVendingWallet();

        // Уменьшаем количество продукта
        VendingProducts.RemoveProduct(product, 1);

        return dict_change;
    }
    public Dictionary<decimal, int> CancelOperation()
    {
        Dictionary<decimal, int> bufferCoins = new Dictionary<decimal, int>(VendingBuffer.Coins);

        // Монеты возвращаем пользователю в другом классе сервис
        VendingBuffer.Coins.Clear();

        return bufferCoins;
    }
    public Dictionary<decimal, int> CollectMoney()
    {
        Dictionary<decimal, int> collectedCoins = new(VendingWallet.Coins);

        // Через сервис начисляем собранные монету пользователю под админкой
        VendingWallet.Coins.Clear();

        return collectedCoins;
    }
    public string GetProducts()
    {
        return VendingProducts.GetProducts();
    }
    public void AddCoinsToVM(decimal nominal, int count)
    {
        VendingWallet.AddCoins(nominal, count);
    }
    public void InsertCoinsToBuffer(decimal nominal, int count)
    {
        VendingBuffer.AddCoins(nominal, count);
    }

    /// <summary>
    /// Перенос всех монет из буфера в кошелек автомата.
    /// </summary>
    internal void TransferAllCoinsFromBufferToVendingWallet()
    {
        // Копия нужна чтобы итерироваться по всем элеменетам и не столкунться с ошибкой в foreach
        var copy_buffer = new Dictionary<decimal, int>(VendingBuffer.Coins);
        foreach(var kvp in copy_buffer)
        {
            AddCoinsToVM(kvp.Key, kvp.Value);
            VendingBuffer.RemoveCoins(kvp.Key, kvp.Value);
        }
    }

    /// <summary>
    /// Попытка составить сдачу для заданной суммы из кошелька и буффера взятых вместе.
    /// </summary>
    /// <param name="change">Необходимая сумма сдачи (>= 0).</param>
    /// <returns>Словарь (номинал -> количество) требуемой сдачи или пустой словарь если собрать нельзя.</returns>
    internal Dictionary<decimal, int> GetChange(decimal change)
    {
        // Словарь всех монет из буффера и кошелька. При таком подходе сдача с большим шансом сформируется т.к. в диапазон добавляется буффер
        Dictionary<decimal, int> temp_wallet = new();

        foreach (var kvp in VendingBuffer.Coins)
        {
            temp_wallet[kvp.Key] = kvp.Value;
        }
        foreach (var kvp in VendingWallet.Coins)
        {
            if (temp_wallet.ContainsKey(kvp.Key)) temp_wallet[kvp.Key] += kvp.Value;
            else temp_wallet[kvp.Key] = kvp.Value;
        }

        Dictionary<decimal, int> dict_change = new();


        decimal decimal_change = change;

        // Словарь для жадного алгоритма, ключ в сортировке значение номинала
        List<decimal> sorted_wallet = [.. temp_wallet.Keys.OrderByDescending(v => v)];

        foreach (var coin in sorted_wallet)
        {
            // Защита от ошибок
            if (decimal_change <= 0)
            {
                break;
            }


            if (temp_wallet[coin] > 0 && coin <= decimal_change)
            {
                // число максимального возможного количества монет данного номинала
                int count_curr_coin = temp_wallet[coin] > (int)(decimal_change / coin) ? (int)(decimal_change / coin) : temp_wallet[coin];

                if (count_curr_coin > 0)
                {
                    if (!dict_change.ContainsKey(coin)) dict_change[coin] = count_curr_coin;
                    else dict_change[coin] += count_curr_coin;

                    decimal_change -= count_curr_coin * coin;

                    temp_wallet[coin] -= count_curr_coin;
                }
            }
        }
        // При неудачной формировке сдачи. Возвращаем пустой словарь.
        if (decimal_change > 0)
        {
            return new Dictionary<decimal, int>();
        }
        return dict_change;
    }

}
/// <summary>
/// Сервис для взаимодействия пользователя и автомата (кошелёк пользователя + автомат).
/// </summary>
public class VendingMachineService
{
    private readonly IVendingMachine _vendingMachine;
    private readonly ICoinStorage _userWallet;
    private Dictionary<decimal, int> _change;

    /// <summary>
    /// Создаёт сервис для указанного автомата и кошелька пользователя.
    /// </summary>
    /// <param name="vendingMachine">Экземпляр автомата.</param>
    /// <param name="userWallet">Кошелёк пользователя.</param>
    public VendingMachineService(IVendingMachine vendingMachine, ICoinStorage userWallet)
    {
        _vendingMachine = vendingMachine;
        _userWallet = userWallet;
        _change = new Dictionary<decimal, int>();
    }
    /// <summary>
    /// Возвращает текст меню операций.
    /// </summary>
    /// <returns>Строка меню.</returns>
    public string InfoBar()
    {
        string info = "Введите номер операции:\n" +
            "1.Посмотреть список товаров\n" +
            "2.Вставить монеты\n" +
            "3.Выбрать товар и получить\n" +
            "4.Получить сдачу.\n" +
            "5.Отменить операцию.\n" +
            "6.[ADMIN] Пополнить ассортимент\n" +
            "7.[ADMIN] Сбор собранных средств\n";

        return info;
    }

    /// <summary>
    /// Переносит монеты из кошелька пользователя в буфер автомата.
    /// </summary>
    /// <param name="nominal">Номинал монеты.</param>
    /// <param name="count">Количество монет.</param>
    /// <returns>True при успешной вставке, false если номинал отсутствует или недостаточно монет у пользователя.</returns>
    public bool InsertCoins(decimal nominal, int count)
    {
        if (!_userWallet.GetCoinNominals().Contains(nominal)) return false;

        if (_userWallet.GetCoinCount(nominal) < count) return false;

        _userWallet.RemoveCoins(nominal, count);
        _vendingMachine.InsertCoinsToBuffer(nominal, count);

        return true;
    }

    /// <summary>
    /// Попытка купить товар по имени. Сформированную сдачу складывает во внутренний буфер _change.
    /// </summary>
    /// <param name="productName">Имя товара.</param>
    /// <returns>True при успешной покупке.</returns>
    public bool BuyProduct(string productName)
    {
        Dictionary<decimal, int>? newChange = _vendingMachine.BuyProduct(productName);

        if (newChange == null) return false;

        foreach (var kvp in newChange)
        {

            if(_change.ContainsKey(kvp.Key))
            {
                _change[kvp.Key] += kvp.Value;
            }
            else
            {
                _change.Add(kvp.Key, kvp.Value);
            }
        }

        return true;
    }

    /// <summary>
    /// Возвращает накопленную сдачу пользователю (перенос в кошелек пользователя).
    /// </summary>
    /// <returns>True если была и успешно возвращена сдача.</returns>
    public bool ReturnChange()
    {
        if (_change == null || _change.Count == 0) return false;

        foreach (var kvp in _change)
        {
            _userWallet.AddCoins(kvp.Key, kvp.Value);
        }

        _change.Clear();

        return true;
    }

    /// <summary>
    /// Отмена операции, пользователь получает монеты из буффера автомата.
    /// </summary>
    /// <returns>True при успешном возврате, False при отсутствии монет в буфере.</returns>
    public bool CancelOperation()
    {
        Dictionary<decimal, int> refund = _vendingMachine.CancelOperation();

        if (refund.Count == 0) return false;

        foreach (var kvp in refund)
        {
            _userWallet.AddCoins(kvp.Key, kvp.Value);
        }

        return true;
    }

    /// <summary>
    /// [ADM] Добавляет продукты в автомат.
    /// </summary>
    /// <param name="productName">Имя товара.</param>
    /// <param name="price">Цена товара (учитывается при создании нового имени).</param>
    /// <param name="count">Количество для добавления.</param>
    public void AddProducts(string productName,decimal price, int count)
    {
        _vendingMachine.AddProduct(new Product(productName, price, count));
    }

    /// <summary>
    /// [ADM] Пополняет внутренний кошелёк автомата монетами, не из кошелька.
    /// </summary>
    /// <param name="nominal">Номинал.</param>
    /// <param name="count">Количество.</param>
    public void AddCoins(decimal nominal, int count)
    {
        _vendingMachine.AddCoinsToVM(nominal, count);
    }

    /// <summary>
    /// [ADM] Собирает деньги из автомата и добавляет их в кошелёк пользователя.
    /// </summary>
    public void CollectMoney()
    {
        Dictionary<decimal, int> collectedMoney = _vendingMachine.CollectMoney();

        foreach (var kvp in collectedMoney)
        {
            _userWallet.AddCoins(kvp.Key, kvp.Value);
        }
    }
}

/// <summary>
/// Консольное приложение: input и output система и работа с сервисом.
/// </summary>
public static class ConsoleApp
{
    private static IVendingMachine _vendingMachine = new VendingMachine();
    private static ICoinStorage _userWallet = new CoinStorage();
    private static VendingMachineService _VMS = new (_vendingMachine, _userWallet);
    private static readonly int adminPass = 123132;

    /// <summary>
    /// Запуск консольного приложения
    /// </summary>
    public static void Run()
    {
        Init_Data();
        while (true)
        {
            Console.WriteLine("\n\n"+_VMS.InfoBar());
            string? input = Console.ReadLine();
            ConsoleInput(input);
        }
    }

    /// <summary>
    /// Инициализация данных для приложения.
    /// </summary>
    private static void Init_Data()
    {

        // Добавление продуктов в автомат
        _VMS.AddProducts("Egg", 1m, 5);
        _VMS.AddProducts("Sprunk", 0.7m, 4);
        _VMS.AddProducts("Bread", 2.5m, 3);
        _VMS.AddProducts("Meat", 4m, 2);
        _VMS.AddProducts("Orange", 2m, 1);

        // Добавление денег в автомат
        _VMS.AddCoins(0.2m, 100);
        _VMS.AddCoins(0.5m, 100);
        _VMS.AddCoins(1m, 50);
        _VMS.AddCoins(2m, 40);
        _VMS.AddCoins(5m, 30);

        //Добавление денег в кошелек пользователя
        _userWallet.AddCoins(0.2m, 5);
        _userWallet.AddCoins(0.5m, 4);
        _userWallet.AddCoins(1m, 2);
        _userWallet.AddCoins(2m, 1);
        _userWallet.AddCoins(5m, 1);
    }

    /// <summary>
    /// Выбор команд через консоль.
    /// </summary>
    /// <param name="input">Ввод пользователя</param>
    private static void ConsoleInput(string? input)
    {
        switch (input)
        {
            case "1": ShowProducts(); break;
            case "2": InsertCoins(); break;
            case "3": BuyProduct(); break;
            case "4": ReturnChange(); break;
            case "5": CancelOperation(); break;
            case "6": AddProduct(); break;
            case "7": CollectMoney(); break;
            default: Console.WriteLine("Неверный выбор"); break;
        }
    }

    /// <summary>
    /// Показывает список продуктов.
    /// </summary>
    private static void ShowProducts()
    {
        Console.WriteLine("Список продуктов:\n");
        Console.WriteLine(_vendingMachine.GetProducts());
    }

    /// <summary>
    /// Вставка монет пользователем.
    /// </summary>
    private static void InsertCoins()
    {
        Console.WriteLine("Внесение монет\n" + "Кошелек:\n" + $"{_userWallet.GetStorage()}");
        Console.WriteLine("Введите номинал монеты (например 0.2, 0.5, 1, 2, 5): ");

        string? inputNominal = Console.ReadLine();

        if (!decimal.TryParse(inputNominal, CultureInfo.InvariantCulture, out decimal nominal)
            || !_userWallet.GetCoinNominals().Contains(nominal))
        {
            Console.WriteLine("[Ошибка] Неверный формат номинала");
            return;
        }

        Console.WriteLine($"Введите количество монет номинала {inputNominal}, которые хотите внести:");
        string? inputCount = Console.ReadLine();
        if (!int.TryParse(inputCount, out int count) || count <= 0)
        {
            Console.WriteLine("[Ошибка] Неверный формат количества монет");
            return;
        }
        if (!_VMS.InsertCoins(nominal, count))
        {
            Console.WriteLine("[Ошибка] Введенный номинал не найден либо у вас отсутсвует данное количетсво монет");
            return;
        }
        Console.WriteLine($"Вы успешно пополнили автомат на {count} монет номинала {nominal}");
    }

    /// <summary>
    /// Покупка товара
    /// </summary>
    public static void BuyProduct()
    {
        Console.WriteLine("Покупка товара\n" + "Список товаров:\n" + $"{_vendingMachine.GetProducts()}");
        Console.WriteLine("Введите название товара:");

        string? inputName = Console.ReadLine();

        if (inputName != null && _VMS.BuyProduct(inputName))
        {
            Console.WriteLine($"Вы успешно купили товар {inputName}");
            Console.WriteLine("Сдача сформирована, для получения введите 4");
        }
        else
        {
            Console.WriteLine("[Ошибка] Вы не смогли купить товар");
        }
    }

    /// <summary>
    /// Получение сдачи пользователем.
    /// </summary>
    public static void ReturnChange()
    {
        Console.WriteLine("Получение сдачи");

        if(_VMS.ReturnChange())
        {
            Console.WriteLine("Вы успешно получили сдачу");
            Console.WriteLine("Текущее состояние кошелька:\n" + _userWallet.GetStorage());
        }
        else 
        {
            Console.WriteLine("[Критическая ошибка] Что-то пошло не так"); 
        }
    }

    /// <summary>
    /// Отмена операции, получаем вставленные монеты (из буффера).
    /// </summary>
    public static void CancelOperation()
    {
        Console.WriteLine("Отмена операции");

        if( _VMS.CancelOperation())
        {
            Console.WriteLine("Операция успешно отменена. Деньги возвращены.");
            Console.WriteLine("Текущее состояние кошелька:\n" + _userWallet.GetStorage());
        }
        else
        {
            Console.WriteLine("[Ошибка] Отсутсвует операция для отмены");
        }
    }

    /// <summary>
    /// [ADM] Добавление товара
    /// </summary>
    public static void AddProduct()
    {
        Console.WriteLine("Добавление товара");
        Console.WriteLine("Введите админ-пароль:");

        string? inputAdminPass = Console.ReadLine();

        if (inputAdminPass == null || !int.TryParse(inputAdminPass, out int AdminPass) || AdminPass != adminPass)
        {
            Console.WriteLine("Вы ввели неверный админ-пароль");
            return;
        }

        Console.Write("Введите название товара:");
        string? inputName = Console.ReadLine();
        if (string.IsNullOrEmpty(inputName)) { Console.WriteLine("[Ошибка] Название не может быть пустым"); return; }

        Console.Write("Введите цену товара(цена будет учитываться только в случае нового товара):");
        string? inputPrice = Console.ReadLine();
        if (!decimal.TryParse(inputPrice, CultureInfo.InvariantCulture, out decimal price))
        {
            Console.WriteLine("[Ошибка] Неверный формат цены");
            return;
        }

        Console.WriteLine("Введите количество товара:");
        string? inputCount = Console.ReadLine();
        if (!int.TryParse(inputCount, out int count))
        {
            Console.WriteLine("[Ошибка] Неверный формат количества товара");
            return;
        }

        _VMS.AddProducts(inputName, price, count);
        Console.WriteLine($"Товар {inputName} в количестве {count} успешно добавлен в автомат");
    }

    /// <summary>
    /// [ADM] Сбор денег из автомата в кошелек.
    /// </summary>
    public static void CollectMoney()
    {
        Console.WriteLine("Введите админ-пароль:");

        string? inputAdminPass = Console.ReadLine();

        if (inputAdminPass == null || !int.TryParse(inputAdminPass, out int AdminPass) || AdminPass != adminPass)
        {
            Console.WriteLine("Вы ввели неверный админ-пароль");
            return;
        }

        Console.WriteLine("Сбор денег");
        _VMS.CollectMoney();
        Console.WriteLine("Вы успешно собрали деньги с автомата и перенесли в кошелек");

        Console.WriteLine("Текущее состояние кошелька:\n" + _userWallet.GetStorage());
    }

}



class Program
{
    static void Main()
    {
        ConsoleApp.Run();
    }
}
