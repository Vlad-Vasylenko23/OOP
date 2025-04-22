using System;

public class Car
{
    public string brand;
    public string model;
    public int year;
    public double mileage;

    // Стандартний конструктор
    public Car()
    {
        brand = "Unknown";
        model = "Unknown";
        year = 0;
        mileage = 0;
        Console.WriteLine($"Автомобіль {brand} {model} створено (стандартний конструктор).");
    }

    // Параметризований конструктор
    public Car(string brand, string model, int year, double mileage)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
        this.mileage = mileage;
        Console.WriteLine($"Автомобіль {this.brand} {this.model} ({this.year}) створено (параметризований конструктор).");
        // Розширене завдання: Перевірка року випуску
        if (this.year > DateTime.Now.Year)
        {
            Console.WriteLine($"Попередження: Рік випуску ({this.year}) більший за поточний рік ({DateTime.Now.Year}).");
        }
    }

    // Копіювальний конструктор
    public Car(Car other)
    {
        this.brand = other.brand;
        this.model = other.model;
        this.year = other.year;
        this.mileage = other.mileage;
        Console.WriteLine($"Автомобіль {this.brand} {this.model} ({this.year}) створено (копіювальний конструктор).");
    }

    // Деструктор
    ~Car()
    {
        Console.WriteLine($"Автомобіль {brand} {model} ({year}) видалено з пам'яті.");
    }

    // Метод для виводу інформації про автомобіль
    public string get_info()
    {
        return $"Марка: {brand}, Модель: {model}, Рік: {year}, Пробіг: {mileage} км";
    }

    // Розширене завдання: Метод для оновлення пробігу
    public void update_mileage(double new_mileage)
    {
        if (new_mileage > mileage)
        {
            mileage = new_mileage;
            Console.WriteLine($"Пробіг автомобіля {brand} {model} оновлено до {mileage} км.");
        }
        else
        {
            Console.WriteLine($"Помилка: Новий пробіг ({new_mileage}) не може бути меншим або рівним поточному ({mileage}).");
        }
    }

    // Розширене завдання: Метод для порівняння двох автомобілів за роком випуску
    public static int CompareByYear(Car car1, Car car2)
    {
        return car1.year.CompareTo(car2.year);
    }

    // Розширене завдання: Метод для порівняння двох автомобілів за пробігом
    public static int CompareByMileage(Car car1, Car car2)
    {
        return car1.mileage.CompareTo(car2.mileage);
    }
}

public class Task3
{
    public static void Main(string[] args)
    {
        // Створення об’єкта через стандартний конструктор
        Car car1 = new Car();
        Console.WriteLine($"Інформація про car1: {car1.get_info()}");
        Console.WriteLine();

        // Створення об’єкта через параметризований конструктор
        Car car2 = new Car("Toyota", "Camry", 2022, 55000.75);
        Console.WriteLine($"Інформація про car2: {car2.get_info()}");
        Console.WriteLine();

        // Створення об’єкта через копіювальний конструктор
        Car car3 = new Car(car2);
        Console.WriteLine($"Інформація про car3: {car3.get_info()}");
        Console.WriteLine();

        // Перевірка методу оновлення пробігу
        car1.update_mileage(1000);
        car2.update_mileage(60000);
        car3.update_mileage(50000); // Спроба встановити менший пробіг
        Console.WriteLine($"Інформація про car1 після оновлення пробігу: {car1.get_info()}");
        Console.WriteLine($"Інформація про car2 після оновлення пробігу: {car2.get_info()}");
        Console.WriteLine($"Інформація про car3 після оновлення пробігу: {car3.get_info()}");
        Console.WriteLine();

        // Перевірка методів порівняння
        Console.WriteLine($"Порівняння car2 та car3 за роком: {Car.CompareByYear(car2, car3)} (0 - однакові)");
        Car car4 = new Car("Honda", "Civic", 2020, 30000);
        Console.WriteLine($"Порівняння car2 та car4 за роком: {Car.CompareByYear(car2, car4)} (додатне - car2 пізніше)");
        Console.WriteLine($"Порівняння car4 та car2 за роком: {Car.CompareByYear(car4, car2)} (від'ємне - car4 раніше)");
        Console.WriteLine();

        Console.WriteLine($"Порівняння car2 та car3 за пробігом: {Car.CompareByMileage(car2, car3)} (0 - однакові)");
        Console.WriteLine($"Порівняння car2 та car4 за пробігом: {Car.CompareByMileage(car2, car4)} (додатне - пробіг car2 більший)");
        Console.WriteLine($"Порівняння car4 та car2 за пробігом: {Car.CompareByMileage(car4, car2)} (від'ємне - пробіг car4 менший)");
        Console.WriteLine();

        // Деструктори будуть викликані автоматично при збиранні сміття (Garbage Collection).
        // Щоб побачити їх роботу в консолі примусово, можна спробувати викликати GC:
        Console.WriteLine("Примусовий виклик збирача сміття...");
        car1 = null;
        car2 = null;
        car3 = null;
        car4 = null;
        GC.Collect();
        GC.WaitForPendingFinalizers(); // Дочекатися виконання всіх фіналізаторів
        Console.WriteLine("Завершення програми.");
    }
}