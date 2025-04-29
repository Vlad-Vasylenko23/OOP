using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SolidCafe
{
    // --- Моделі даних ---

    // Представляє страву в меню
    public class MenuItem
    {
        public string Name { get; }
        public decimal Price { get; }

        public MenuItem(string name, decimal price)
        {
            Name = name;
            Price = price;
        }
    }

    // Представляє позицію в замовленні
    public class OrderItem
    {
        public MenuItem Item { get; }
        public int Quantity { get; set; } // Дозволяємо змінювати кількість

        public OrderItem(MenuItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public decimal GetTotalPrice() => Item.Price * Quantity;
    }

    // Представляє замовлення
    // Відповідає за зберігання позицій та розрахунок базової суми
    // Відповідає SRP
    public class Order
    {
        private readonly List<OrderItem> _items = new List<OrderItem>();
        public IReadOnlyList<OrderItem> Items => _items.AsReadOnly(); // Надаємо доступ тільки для читання

        public void AddItem(MenuItem item, int quantity)
        {
            var existingItem = _items.FirstOrDefault(i => i.Item.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.Quantity += quantity;
            }
            else
            {
                _items.Add(new OrderItem(item, quantity));
            }
            Console.WriteLine($"Додано: {item.Name} x{quantity}");
        }

        public decimal CalculateTotalAmount()
        {
            return _items.Sum(item => item.GetTotalPrice());
        }
    }

    // --- Стратегії Знижок (OCP, LSP) ---

    // Інтерфейс для стратегій знижок (DIP)
    public interface IDiscountStrategy
    {
        // Розраховує суму знижки для даного замовлення
        decimal CalculateDiscount(Order order);
        string GetDescription(); // Опис знижки для чека
    }

    // Конкретна стратегія: Знижка у відсотках
    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percentage;
        private readonly decimal _minAmountForDiscount;

        public PercentageDiscountStrategy(decimal percentage, decimal minAmountForDiscount = 0)
        {
            _percentage = percentage / 100m; // Зберігаємо як частку (0.10 для 10%)
            _minAmountForDiscount = minAmountForDiscount;
        }

        public decimal CalculateDiscount(Order order)
        {
            decimal totalAmount = order.CalculateTotalAmount();
            if (totalAmount >= _minAmountForDiscount)
            {
                return totalAmount * _percentage;
            }
            return 0m;
        }

        public string GetDescription() => $"Знижка {(_percentage * 100):F0}%" + (_minAmountForDiscount > 0 ? $" (при сумі від {_minAmountForDiscount:C})" : "");
    }

    // Конкретна стратегія: Фіксована знижка
    public class FixedAmountDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _discountAmount;
        private readonly decimal _minAmountForDiscount;

        public FixedAmountDiscountStrategy(decimal discountAmount, decimal minAmountForDiscount = 0)
        {
            _discountAmount = discountAmount;
            _minAmountForDiscount = minAmountForDiscount;
        }

        public decimal CalculateDiscount(Order order)
        {
             decimal totalAmount = order.CalculateTotalAmount();
             if (totalAmount >= _minAmountForDiscount)
             {
                 // Переконуємося, що знижка не більша за суму замовлення
                 return Math.Min(_discountAmount, totalAmount);
             }
             return 0m;
        }
         public string GetDescription() => $"Фіксована знижка {_discountAmount:C}" + (_minAmountForDiscount > 0 ? $" (при сумі від {_minAmountForDiscount:C})" : "");
    }

     // Конкретна стратегія: "Купи 1, отримай 1 безкоштовно" для конкретної страви
    public class BuyOneGetOneFreeStrategy : IDiscountStrategy
    {
        private readonly string _itemName;

        public BuyOneGetOneFreeStrategy(string itemName)
        {
            _itemName = itemName;
        }

        public decimal CalculateDiscount(Order order)
        {
            var targetItem = order.Items.FirstOrDefault(i => i.Item.Name == _itemName);
            if (targetItem != null && targetItem.Quantity >= 2)
            {
                // Кількість безкоштовних товарів = ціла частина від кількості / 2
                int freeQuantity = targetItem.Quantity / 2;
                return freeQuantity * targetItem.Item.Price;
            }
            return 0m;
        }
         public string GetDescription() => $"Акція '1+1' для \"{_itemName}\"";
    }

    // --- Калькулятор Знижок (SRP) ---
    // Відповідає тільки за розрахунок знижки, використовуючи стратегію

    public class DiscountCalculator
    {
        // Приймає стратегію через конструктор (DIP)
        // Можна також передавати стратегію в метод Calculate
        public decimal Calculate(Order order, IDiscountStrategy discountStrategy)
        {
            if (discountStrategy == null) return 0m; // Якщо стратегія не задана

            decimal discount = discountStrategy.CalculateDiscount(order);
            Console.WriteLine($"Розраховано знижку ({discountStrategy.GetDescription()}): {discount:C}");
            return discount;
        }
    }


    // --- Генератори Чека (OCP, ISP) ---

    // Інтерфейс для генераторів чеків (DIP)
    public interface IReceiptGenerator
    {
        // Генерує представлення чека
        string GenerateReceipt(Order order, decimal discountAmount, IDiscountStrategy appliedDiscount); // Додаємо інформацію про знижку
    }

    // Конкретний генератор: Чек для консолі
    public class ConsoleReceiptGenerator : IReceiptGenerator
    {
        public string GenerateReceipt(Order order, decimal discountAmount, IDiscountStrategy? appliedDiscount) // Nullable, якщо знижки не було
        {
             var receiptBuilder = new StringBuilder();
            receiptBuilder.AppendLine("------- КАФЕ \"SOLID\" -------");
            receiptBuilder.AppendLine("Ваше замовлення:");
            receiptBuilder.AppendLine("------------------------------");

            foreach (var item in order.Items)
            {
                 receiptBuilder.AppendLine($"{item.Item.Name} x{item.Quantity} --- {item.GetTotalPrice():C}");
            }

            receiptBuilder.AppendLine("------------------------------");
            decimal totalAmount = order.CalculateTotalAmount();
            receiptBuilder.AppendLine($"Разом: {totalAmount:C}");

            if (discountAmount > 0 && appliedDiscount != null)
            {
                receiptBuilder.AppendLine($"Знижка ({appliedDiscount.GetDescription()}): -{discountAmount:C}");
                receiptBuilder.AppendLine($"До сплати: {(totalAmount - discountAmount):C}");
            } else {
                 receiptBuilder.AppendLine($"До сплати: {totalAmount:C}");
            }

            receiptBuilder.AppendLine("------------------------------");
            receiptBuilder.AppendLine($"Дата: {DateTime.Now}");
            receiptBuilder.AppendLine("      Дякуємо за замовлення!     ");
            receiptBuilder.AppendLine("------------------------------");

            return receiptBuilder.ToString();
        }
    }

     // Конкретний генератор: Чек у форматі PDF (Заглушка)
     // Потрібна бібліотека, напр., iTextSharp або QuestPDF
    public class PdfReceiptGenerator : IReceiptGenerator
    {
         public string GenerateReceipt(Order order, decimal discountAmount, IDiscountStrategy? appliedDiscount)
        {
            string fileName = $"Receipt_{DateTime.Now:yyyyMMddHHmmss}.pdf";
            // Тут була б логіка генерації PDF за допомогою бібліотеки
            Console.WriteLine($"--- (Симуляція) Генерація PDF чека: {fileName} ---");
            Console.WriteLine($"--- Замовлення: {order.CalculateTotalAmount():C}, Знижка: {discountAmount:C} ---");
             // Повертаємо рядок, що імітує результат
            return $"PDF чек збережено як {fileName}";
        }
    }


    // --- Менеджер Замовлень (SRP, DIP) ---
    // Координує процес: створення замовлення, розрахунок знижки, генерація чека

    public class OrderManager
    {
        private readonly DiscountCalculator _discountCalculator;
        private readonly IReceiptGenerator _receiptGenerator; // Залежність від інтерфейсу (DIP)
        private readonly Dictionary<string, MenuItem> _menu; // Меню тепер тут

        // Впровадження залежностей через конструктор (DI)
        public OrderManager(IReceiptGenerator receiptGenerator, DiscountCalculator discountCalculator, Dictionary<string, MenuItem> menu)
        {
            _receiptGenerator = receiptGenerator ?? throw new ArgumentNullException(nameof(receiptGenerator));
            _discountCalculator = discountCalculator ?? throw new ArgumentNullException(nameof(discountCalculator));
             _menu = menu ?? throw new ArgumentNullException(nameof(menu));
             Console.OutputEncoding = Encoding.UTF8;
        }

        // Метод для обробки повного циклу замовлення
        public void ProcessOrder(Order order, IDiscountStrategy? discountStrategy = null) // Знижка необов'язкова
        {
            Console.WriteLine("\n--- Обробка Замовлення ---");
            decimal totalAmount = order.CalculateTotalAmount();
            Console.WriteLine($"Загальна сума замовлення: {totalAmount:C}");

            decimal discountAmount = 0m;
             if(discountStrategy != null) // Перевіряємо, чи є стратегія для застосування
             {
                discountAmount = _discountCalculator.Calculate(order, discountStrategy);
             } else {
                 Console.WriteLine("Знижка не застосовується.");
             }


            // Генерація чека за допомогою впровадженого генератора
            string receipt = _receiptGenerator.GenerateReceipt(order, discountAmount, discountStrategy);

            // Виведення результату (у реальній системі це може бути відправка на друк, збереження тощо)
            Console.WriteLine("\n--- Згенерований Чек ---");
            Console.WriteLine(receipt);
            Console.WriteLine("-------------------------");
        }

         // Допоміжний метод для отримання страви з меню
        public MenuItem? GetMenuItem(string name)
        {
            _menu.TryGetValue(name, out MenuItem? item);
            if(item == null) {
                Console.WriteLine($"Помилка: Страва '{name}' відсутня в меню.");
            }
            return item;
        }
    }

    // --- Головний клас програми (Композиція та запуск) ---

    class Program
    {
        static void Main(string[] args)
        {
            // 1. Створюємо меню
             var menu = new Dictionary<string, MenuItem>
            {
                { "Кава", new MenuItem("Кава", 50.00m) },
                { "Чай", new MenuItem("Чай", 40.00m) },
                { "Тістечко", new MenuItem("Тістечко", 75.50m) },
                { "Сік", new MenuItem("Сік", 45.00m) }
            };

            // 2. Створюємо необхідні компоненти (генератор чеків, калькулятор знижок)
            IReceiptGenerator consoleReceiptGenerator = new ConsoleReceiptGenerator();
            IReceiptGenerator pdfReceiptGenerator = new PdfReceiptGenerator(); // Інший генератор
            DiscountCalculator discountCalculator = new DiscountCalculator();

            // 3. Створюємо менеджера замовлень, впроваджуючи залежності
            // Використовуємо консольний генератор чеків
            OrderManager orderManagerConsole = new OrderManager(consoleReceiptGenerator, discountCalculator, menu);
            // Можна створити іншого менеджера з PDF генератором
             OrderManager orderManagerPdf = new OrderManager(pdfReceiptGenerator, discountCalculator, menu);

            // 4. Створюємо стратегії знижок
            IDiscountStrategy percentageDiscount = new PercentageDiscountStrategy(10, 100m); // 10% при сумі від 100
            IDiscountStrategy fixedDiscount = new FixedAmountDiscountStrategy(50, 200m); // 50 грн при сумі від 200
            IDiscountStrategy bogoDiscount = new BuyOneGetOneFreeStrategy("Кава"); // 1+1 на каву

            // --- Демонстрація 1: Замовлення зі знижкою 10% (консольний чек) ---
            Console.WriteLine("===== Демо 1: Знижка 10% (Консоль) =====");
            var order1 = new Order();
             var coffee = orderManagerConsole.GetMenuItem("Кава");
             var cake = orderManagerConsole.GetMenuItem("Тістечко");
             if(coffee != null) order1.AddItem(coffee, 2); // 100 грн
             if(cake != null) order1.AddItem(cake, 1);   // 75.50 грн -> Разом 175.50 > 100

             orderManagerConsole.ProcessOrder(order1, percentageDiscount);

             // --- Демонстрація 2: Замовлення з фіксованою знижкою (консольний чек) ---
            Console.WriteLine("\n===== Демо 2: Фіксована знижка (Консоль) =====");
             var order2 = new Order();
              if(coffee != null) order2.AddItem(coffee, 3); // 150 грн
              if(cake != null) order2.AddItem(cake, 1);   // 75.50 грн -> Разом 225.50 > 200

             orderManagerConsole.ProcessOrder(order2, fixedDiscount);

            // --- Демонстрація 3: Замовлення з акцією "1+1" (консольний чек) ---
             Console.WriteLine("\n===== Демо 3: Акція '1+1' на Каву (Консоль) =====");
             var order3 = new Order();
             var tea = orderManagerConsole.GetMenuItem("Чай");
             if(coffee != null) order3.AddItem(coffee, 3); // Купуємо 3 кави, 1 буде безкоштовно (знижка 50 грн)
             if(tea != null) order3.AddItem(tea, 1);    // 40 грн

             orderManagerConsole.ProcessOrder(order3, bogoDiscount);

             // --- Демонстрація 4: Замовлення без знижки (консольний чек) ---
             Console.WriteLine("\n===== Демо 4: Без знижки (Консоль) =====");
             var order4 = new Order();
             if(tea != null) order4.AddItem(tea, 1); // 40 грн
             orderManagerConsole.ProcessOrder(order4); // Не передаємо стратегію

             // --- Демонстрація 5: Замовлення зі знижкою 10% (PDF чек - симуляція) ---
            Console.WriteLine("\n===== Демо 5: Знижка 10% (PDF Симуляція) =====");
            var order5 = new Order();
             if(coffee != null) order5.AddItem(coffee, 2);
             if(cake != null) order5.AddItem(cake, 1);
             // Використовуємо іншого менеджера з PDF генератором
             orderManagerPdf.ProcessOrder(order5, percentageDiscount);


            Console.WriteLine("\nНатисніть будь-яку клавішу для виходу...");
            Console.ReadKey();
        }
    }
}