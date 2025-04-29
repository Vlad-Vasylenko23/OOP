using System;
using System.Collections.Generic;
using System.Linq;

// Інтерфейс Observer
public interface IObserver
{
    void Update(string stockName, double newPrice);
}

// Інтерфейс Subject
public interface ISubject
{
    void Attach(IObserver observer);
    void Detach(IObserver observer);
    void NotifyObservers(string stockName, double newPrice);
}

// Клас StockExchange (біржа)
public class StockExchange : ISubject
{
    protected readonly List<IObserver> observers = new List<IObserver>(); // Змінено на protected
    private readonly Dictionary<string, double> _stockPrices = new Dictionary<string, double>();

    public void SetStockPrice(string stockName, double newPrice)
    {
        if (_stockPrices.ContainsKey(stockName) && _stockPrices[stockName] == newPrice)
        {
            return; // Ціна не змінилася, не оповіщаємо
        }
        _stockPrices[stockName] = newPrice;
        NotifyObservers(stockName, newPrice);
    }

    public void Attach(IObserver observer)
    {
        if (!observers.Contains(observer))
        {
            observers.Add(observer);
        }
    }

    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string stockName, double newPrice)
    {
        foreach (var observer in observers.ToList()) // Щоб уникнути проблем при відписуванні під час сповіщення
        {
            observer.Update(stockName, newPrice);
        }
    }
}

// Клас Investor (інвестор)
public class Investor : IObserver
{
    private readonly string _name;
    private readonly List<string> _subscribedStocks = new List<string>();

    public Investor(string name)
    {
        _name = name;
    }

    public void SubscribeToStock(string stockName)
    {
        if (!_subscribedStocks.Contains(stockName))
        {
            _subscribedStocks.Add(stockName);
        }
    }

    public void UnsubscribeFromStock(string stockName)
    {
        _subscribedStocks.Remove(stockName);
    }

    public void Update(string stockName, double newPrice)
    {
        if (_subscribedStocks.Count == 0 || _subscribedStocks.Contains(stockName))
        {
            Console.WriteLine($"Інвестор {_name} отримав оновлення: Акція {stockName} змінила ціну на {newPrice:C2}");
        }
    }
}

// Клас Broker (брокер)
public class Broker : IObserver
{
    private readonly string _name;

    public Broker(string name)
    {
        _name = name;
    }

    public void Update(string stockName, double newPrice)
    {
        Console.WriteLine($"Брокер {_name} повідомлений: Акція {stockName} тепер коштує {newPrice:C2}");
    }
}

// Клас StockExchangeWithLimit (біржа з обмеженням на кількість підписників)
public class StockExchangeWithLimit : StockExchange
{
    private readonly int _maxObservers;

    public StockExchangeWithLimit(int maxObservers)
    {
        _maxObservers = maxObservers;
    }

    public new void Attach(IObserver observer)
    {
        if (observers.Count < _maxObservers) // Використовуємо protected поле observers
        {
            base.Attach(observer);
        }
        else
        {
            Console.WriteLine($"Помилка: Досягнуто максимальну кількість підписників ({_maxObservers}). Неможливо додати нового підписника.");
        }
    }
}

// Клієнтський код
public class Client
{
    public static void Main(string[] args)
    {
        // Створення біржі
        StockExchange stockExchange = new StockExchange();

        // Створення інвесторів
        Investor investor1 = new Investor("Олена");
        Investor investor2 = new Investor("Іван");
        Investor investor3 = new Investor("Софія");

        // Налаштування фільтрів для інвесторів
        investor1.SubscribeToStock("Apple");
        investor1.SubscribeToStock("Microsoft");
        investor2.SubscribeToStock("Google");

        // Створення брокера
        Broker broker1 = new Broker("Фінансова Група 'Альфа'");
        Broker broker2 = new Broker("Інвестиційний Дім 'Бета'");

        // Додавання спостерігачів до біржі
        stockExchange.Attach(investor1);
        stockExchange.Attach(investor2);
        stockExchange.Attach(broker1);
        stockExchange.Attach(investor3); // Інвестор без фільтрів

        Console.WriteLine("Перше оновлення цін:");
        stockExchange.SetStockPrice("Apple", 150.25);
        stockExchange.SetStockPrice("Google", 2800.50);
        stockExchange.SetStockPrice("Microsoft", 315.75);
        Console.WriteLine("\n");

        // Видалення одного інвестора
        stockExchange.Detach(investor2);
        Console.WriteLine("Після відписки Івана:");
        stockExchange.SetStockPrice("Google", 2810.00);
        Console.WriteLine("\n");

        // Додавання брокера
        stockExchange.Attach(broker2);
        Console.WriteLine("Після додавання нового брокера:");
        stockExchange.SetStockPrice("Amazon", 3300.10);
        Console.WriteLine("\n");

        // Демонстрація біржі з обмеженням підписників
        Console.WriteLine("Демонстрація біржі з обмеженням підписників:");
        StockExchangeWithLimit limitedStockExchange = new StockExchangeWithLimit(2);
        limitedStockExchange.Attach(new Investor("Підписник 1"));
        limitedStockExchange.Attach(new Broker("Підписник 2"));
        limitedStockExchange.Attach(new Investor("Підписник 3")); // Цей не буде доданий

        limitedStockExchange.SetStockPrice("Tesla", 850.50);
    }
}