using System;
using System.Collections.Generic;

// Базовий клас Transport
public class Transport
{
    public string brand;
    public string model;
    public int year;

    public Transport(string brand, string model, int year)
    {
        this.brand = brand;
        this.model = model;
        this.year = year;
    }

    public virtual string get_info()
    {
        return $"Марка: {brand}, Модель: {model}, Рік: {year}";
    }

    // Розширене завдання: Базовий метод move()
    public virtual void move()
    {
        Console.WriteLine("Транспортний засіб рухається.");
    }
}

// Похідний клас Car
public class Car : Transport
{
    public int passenger_count;

    public Car(string brand, string model, int year, int passenger_count) : base(brand, model, year)
    {
        this.passenger_count = passenger_count;
    }

    public int get_passenger_capacity()
    {
        return passenger_count;
    }

    public override string get_info()
    {
        return $"{base.get_info()}, Кількість пасажирів: {passenger_count}";
    }

    // Розширене завдання: Перевизначений метод move()
    public override void move()
    {
        Console.WriteLine($"Автомобіль {brand} {model} їде по дорозі.");
    }
}

// Похідний клас Truck
public class Truck : Transport
{
    public double cargo_capacity; // у тоннах

    public Truck(string brand, string model, int year, double cargo_capacity) : base(brand, model, year)
    {
        this.cargo_capacity = cargo_capacity;
    }

    public double get_cargo_capacity()
    {
        return cargo_capacity;
    }

    public override string get_info()
    {
        return $"{base.get_info()}, Вантажопідйомність: {cargo_capacity} т.";
    }

    // Розширене завдання: Перевизначений метод move()
    public override void move()
    {
        Console.WriteLine($"Вантажівка {brand} {model} перевозить вантаж.");
    }
}

// Похідний клас Bike
public class Bike : Transport
{
    public int engine_volume; // у куб. см

    public Bike(string brand, string model, int year, int engine_volume) : base(brand, model, year)
    {
        this.engine_volume = engine_volume;
    }

    public int get_engine_volume()
    {
        return engine_volume;
    }

    public override string get_info()
    {
        return $"{base.get_info()}, Об'єм двигуна: {engine_volume} куб. см";
    }

    // Розширене завдання: Перевизначений метод move()
    public override void move()
    {
        Console.WriteLine($"Мотоцикл {brand} {model} мчить трасою.");
    }
}

public class Task4
{
    public static void Main(string[] args)
    {
        // Створення об’єктів для кожного типу транспортного засобу
        Car myCar = new Car("Toyota", "Camry", 2022, 4);
        Truck myTruck = new Truck("Volvo", "FH16", 2020, 20.5);
        Bike myBike = new Bike("Harley-Davidson", "Sportster", 2021, 1200);

        Console.WriteLine("Інформація про транспортні засоби:");
        Console.WriteLine(myCar.get_info());
        Console.WriteLine($"Місткість автомобіля: {myCar.get_passenger_capacity()} пасажири");
        Console.WriteLine(myTruck.get_info());
        Console.WriteLine($"Вантажопідйомність вантажівки: {myTruck.get_cargo_capacity()} т.");
        Console.WriteLine(myBike.get_info());
        Console.WriteLine($"Об'єм двигуна мотоцикла: {myBike.get_engine_volume()} куб. см");
        Console.WriteLine();

        // Реалізація поліморфного виклику get_info()
        List<Transport> vehicles = new List<Transport> { myCar, myTruck, myBike };
        Console.WriteLine("Поліморфний виклик get_info():");
        foreach (Transport vehicle in vehicles)
        {
            Console.WriteLine(vehicle.get_info());
        }
        Console.WriteLine();

        // Розширене завдання: Поліморфний виклик move()
        Console.WriteLine("Поліморфний виклик move():");
        foreach (Transport vehicle in vehicles)
        {
            vehicle.move();
        }
        Console.WriteLine();

        // Розширене завдання: Перевірка типу транспортного засобу
        Console.WriteLine("Перевірка типу транспортного засобу:");
        foreach (Transport vehicle in vehicles)
        {
            if (vehicle is Car car)
            {
                Console.WriteLine($"{car.brand} {car.model} є автомобілем.");
            }
            else if (vehicle is Truck truck)
            {
                Console.WriteLine($"{truck.brand} {truck.model} є вантажівкою.");
            }
            else if (vehicle is Bike bike)
            {
                Console.WriteLine($"{bike.brand} {bike.model} є мотоциклом.");
            }
            else
            {
                Console.WriteLine($"{vehicle.brand} {vehicle.model} є невідомим транспортним засобом.");
            }
        }
        Console.WriteLine();

        // Розширене завдання: Додаткові похідні класи
        Bus myBus = new Bus("Mercedes-Benz", "Sprinter", 2018, 20);
        ElectricScooter myScooter = new ElectricScooter("Xiaomi", "Mi Pro 2", 2023, 25);

        Console.WriteLine("Додаткові транспортні засоби:");
        Console.WriteLine(myBus.get_info());
        myBus.move();
        Console.WriteLine(myScooter.get_info());
        myScooter.move();
    }
}

// Розширене завдання: Додатковий похідний клас Bus
public class Bus : Transport
{
    public int seatingCapacity;

    public Bus(string brand, string model, int year, int seatingCapacity) : base(brand, model, year)
    {
        this.seatingCapacity = seatingCapacity;
    }

    public string get_bus_capacity()
    {
        return $"Кількість сидячих місць: {seatingCapacity}";
    }

    public override string get_info()
    {
        return $"{base.get_info()}, Кількість сидячих місць: {seatingCapacity}";
    }

    public override void move()
    {
        Console.WriteLine($"Автобус {brand} {model} перевозить пасажирів містом.");
    }
}

// Розширене завдання: Додатковий похідний клас ElectricScooter
public class ElectricScooter : Transport
{
    public int maxSpeed; // км/год

    public ElectricScooter(string brand, string model, int year, int maxSpeed) : base(brand, model, year)
    {
        this.maxSpeed = maxSpeed;
    }

    public string get_max_speed()
    {
        return $"Максимальна швидкість: {maxSpeed} км/год";
    }

    public override string get_info()
    {
        return $"{base.get_info()}, Максимальна швидкість: {maxSpeed} км/год";
    }

    public override void move()
    {
        Console.WriteLine($"Електросамокат {brand} {model} безшумно ковзає тротуаром.");
    }
}