using System;
using System.Collections.Generic;
using System.Linq;

// Інтерфейс для всіх компонентів організаційної структури
public interface IEmployee
{
    string Name { get; }
    string Position { get; }

    void Add(IEmployee employee);
    void Remove(IEmployee employee);
    List<IEmployee> GetSubordinates();
    void DisplayDetails(int depth);
    int CountEmployees();
    IEmployee SearchEmployee(string name);
}

// Клас, що представляє окремого співробітника
public class IndividualEmployee : IEmployee
{
    public string Name { get; }
    public string Position { get; }

    public IndividualEmployee(string name, string position)
    {
        Name = name;
        Position = position;
    }

    public void Add(IEmployee employee)
    {
        // Окремий співробітник не може мати підлеглих
        Console.WriteLine($"Помилка: Співробітник {Name} не може мати підлеглих.");
    }

    public void Remove(IEmployee employee)
    {
        // Окремий співробітник не має підлеглих для видалення
        Console.WriteLine($"Помилка: Співробітник {Name} не має підлеглих для видалення.");
    }

    public List<IEmployee> GetSubordinates()
    {
        return new List<IEmployee>();
    }

    public void DisplayDetails(int depth)
    {
        Console.WriteLine($"{new string('-', depth * 2)}Співробітник: {Name} ({Position})");
    }

    public int CountEmployees()
    {
        return 1;
    }

    public IEmployee SearchEmployee(string name)
    {
        return Name.Equals(name, StringComparison.OrdinalIgnoreCase) ? this : null;
    }
}

// Клас, що представляє відділ
public class Department : IEmployee
{
    public string Name { get; }
    public string Position { get; } // Посада керівника відділу
    private readonly List<IEmployee> _subordinates = new List<IEmployee>();

    public Department(string name, string position)
    {
        Name = name;
        Position = position;
    }

    public void Add(IEmployee employee)
    {
        if (!_subordinates.Contains(employee))
        {
            _subordinates.Add(employee);
        }
        else
        {
            Console.WriteLine($"Попередження: Співробітник або підрозділ '{employee.Name}' вже є у відділі '{Name}'.");
        }
    }

    public void Remove(IEmployee employee)
    {
        _subordinates.Remove(employee);
    }

    public List<IEmployee> GetSubordinates()
    {
        return _subordinates;
    }

    public void DisplayDetails(int depth)
    {
        Console.WriteLine($"{new string('-', depth * 2)}Відділ: {Name} (Керівник: {Position})");
        foreach (var subordinate in _subordinates)
        {
            subordinate.DisplayDetails(depth + 1);
        }
    }

    public int CountEmployees()
    {
        int count = 0;
        foreach (var subordinate in _subordinates)
        {
            count += subordinate.CountEmployees();
        }
        return count;
    }

    public IEmployee SearchEmployee(string name)
    {
        if (Name.Equals(name, StringComparison.OrdinalIgnoreCase) || Position.Equals(name, StringComparison.OrdinalIgnoreCase))
        {
            return this;
        }
        foreach (var subordinate in _subordinates)
        {
            IEmployee found = subordinate.SearchEmployee(name);
            if (found != null)
            {
                return found;
            }
        }
        return null;
    }
}

// Клієнтський код
public class Client
{
    public static void Main(string[] args)
    {
        // Створення співробітників
        IndividualEmployee john = new IndividualEmployee("John Doe", "Програміст");
        IndividualEmployee jane = new IndividualEmployee("Jane Smith", "Дизайнер");
        IndividualEmployee peter = new IndividualEmployee("Peter Jones", "Тестувальник");
        IndividualEmployee mary = new IndividualEmployee("Mary Williams", "Аналітик");
        IndividualEmployee david = new IndividualEmployee("David Brown", "Менеджер з маркетингу");
        IndividualEmployee sarah = new IndividualEmployee("Sarah Miller", "HR-менеджер");

        // Створення відділів
        Department developmentDepartment = new Department("Відділ розробки", "Керівник розробки");
        Department designDepartment = new Department("Відділ дизайну", "Керівник дизайну");
        Department marketingDepartment = new Department("Відділ маркетингу", "Директор з маркетингу");
        Department hrDepartment = new Department("Відділ кадрів", "Директор з персоналу");

        // Додавання співробітників до відділу розробки
        developmentDepartment.Add(john);
        developmentDepartment.Add(jane);
        developmentDepartment.Add(peter);

        // Додавання співробітника до відділу дизайну
        designDepartment.Add(jane); // Спробуємо додати Jane ще раз, щоб побачити перевірку на унікальність

        // Додавання співробітника до відділу маркетингу
        marketingDepartment.Add(david);

        // Додавання співробітника до відділу кадрів
        hrDepartment.Add(sarah);

        // Створення головного офісу
        Department headOffice = new Department("Головний офіс", "Генеральний директор");

        // Додавання відділів до головного офісу
        headOffice.Add(developmentDepartment);
        headOffice.Add(designDepartment);
        headOffice.Add(marketingDepartment);
        headOffice.Add(hrDepartment);
        headOffice.Add(mary); // Додаємо окремого співробітника до головного офісу

        Console.WriteLine("Організаційна структура:");
        headOffice.DisplayDetails(0);
        Console.WriteLine("\n");

        Console.WriteLine($"Загальна кількість співробітників у відділі розробки: {developmentDepartment.CountEmployees()}");
        Console.WriteLine($"Загальна кількість співробітників у головному офісі: {headOffice.CountEmployees()}");
        Console.WriteLine("\n");

        // Пошук співробітника за ім'ям
        string searchName = "Jane Smith";
        IEmployee foundEmployee = headOffice.SearchEmployee(searchName);
        if (foundEmployee != null)
        {
            Console.WriteLine($"Знайдено співробітника '{foundEmployee.Name}' на посаді '{foundEmployee.Position}'.");
        }
        else
        {
            Console.WriteLine($"Співробітника з ім'ям '{searchName}' не знайдено.");
        }

        searchName = "Відділ розробки";
        IEmployee foundDepartment = headOffice.SearchEmployee(searchName);
        if (foundDepartment != null)
        {
            Console.WriteLine($"Знайдено відділ '{foundDepartment.Name}' (Керівник: {foundDepartment.Position}).");
        }
        else
        {
            Console.WriteLine($"Відділ з назвою '{searchName}' не знайдено.");
        }
    }
}