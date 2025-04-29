using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonolithicCafe
{
    // Монолітний клас, що відповідає за все
    public class CafeOrder
    {
        // Зберігання меню (страва -> ціна)
        private Dictionary<string, decimal> _menu;
        // Поточне замовлення (страва -> кількість)
        private Dictionary<string, int> _currentOrder;
        private decimal _totalAmount;
        private decimal _discountAmount;
        private string _receipt;

        // Конструктор для ініціалізації меню
        public CafeOrder()
        {
            _menu = new Dictionary<string, decimal>
            {
                { "Кава", 50.00m },
                { "Чай", 40.00m },
                { "Тістечко", 75.50m },
                { "Сік", 45.00m }
            };
            _currentOrder = new Dictionary<string, int>();
            _totalAmount = 0m;
            _discountAmount = 0m;
            _receipt = string.Empty;
            Console.OutputEncoding = Encoding.UTF8; // Для коректного відображення українських літер у консолі
        }

        // Метод для додавання страви до замовлення
        public void AddItem(string itemName, int quantity)
        {
            if (_menu.ContainsKey(itemName))
            {
                if (_currentOrder.ContainsKey(itemName))
                {
                    _currentOrder[itemName] += quantity;
                }
                else
                {
                    _currentOrder.Add(itemName, quantity);
                }
                Console.WriteLine($"Додано: {itemName} x{quantity}");
                CalculateTotal(); // Перерахунок суми після додавання
            }
            else
            {
                Console.WriteLine($"Помилка: Страва '{itemName}' відсутня в меню.");
            }
        }

        // Метод для розрахунку загальної суми замовлення (без знижки)
        // Порушення SRP: Клас відповідає і за управління замовленням, і за розрахунки
        private void CalculateTotal()
        {
            _totalAmount = 0m;
            foreach (var item in _currentOrder)
            {
                if (_menu.TryGetValue(item.Key, out decimal price))
                {
                    _totalAmount += price * item.Value;
                }
            }
            // Скидаємо знижку та чек при зміні замовлення
            _discountAmount = 0m;
            _receipt = string.Empty;
            Console.WriteLine($"Поточна сума: {_totalAmount:C}");
        }

        // Метод для застосування знижки
        // Порушення OCP: Щоб додати нову знижку, треба змінити цей метод
        // Порушення SRP: Цей клас не повинен займатися логікою знижок
        public void ApplyDiscount(string discountType)
        {
            _discountAmount = 0m; // Скидаємо попередню знижку
            if (discountType == "PERCENTAGE_10") // Жорстко закодована знижка
            {
                if (_totalAmount > 100)
                {
                    _discountAmount = _totalAmount * 0.10m;
                    Console.WriteLine("Застосовано знижку 10%");
                }
                else
                {
                     Console.WriteLine("Знижка 10% не застосована (сума < 100).");
                }
            }
            else if (discountType == "FIXED_50") // Ще одна жорстко закодована знижка
            {
                 if (_totalAmount > 200)
                 {
                    _discountAmount = 50m;
                     Console.WriteLine("Застосовано фіксовану знижку 50 грн.");
                 }
                 else
                 {
                    Console.WriteLine("Фіксована знижка 50 грн не застосована (сума < 200).");
                 }
            }
            else
            {
                Console.WriteLine("Невідомий тип знижки або знижка не застосована.");
            }
        }

        // Метод для генерації чека
        // Порушення SRP: Клас відповідає за генерацію чека
        // Порушення OCP: Зміна формату чека потребує зміни цього методу
        public void GenerateReceipt()
        {
            var receiptBuilder = new StringBuilder();
            receiptBuilder.AppendLine("------- КАФЕ \"МОНОЛІТ\" -------");
            receiptBuilder.AppendLine("Ваше замовлення:");
            receiptBuilder.AppendLine("------------------------------");

            foreach (var item in _currentOrder)
            {
                if (_menu.TryGetValue(item.Key, out decimal price))
                {
                    receiptBuilder.AppendLine($"{item.Key} x{item.Value} --- {price * item.Value:C}");
                }
            }

            receiptBuilder.AppendLine("------------------------------");
            receiptBuilder.AppendLine($"Разом: {_totalAmount:C}");

            if (_discountAmount > 0)
            {
                receiptBuilder.AppendLine($"Знижка: -{_discountAmount:C}");
                receiptBuilder.AppendLine($"До сплати: {(_totalAmount - _discountAmount):C}");
            } else {
                 receiptBuilder.AppendLine($"До сплати: {_totalAmount:C}");
            }

            receiptBuilder.AppendLine("------------------------------");
            receiptBuilder.AppendLine($"Дата: {DateTime.Now}");
            receiptBuilder.AppendLine("      Дякуємо за замовлення!     ");
            receiptBuilder.AppendLine("------------------------------");

            _receipt = receiptBuilder.ToString();
        }

        // Метод для виведення чека в консоль
        public void PrintReceipt()
        {
            if (string.IsNullOrEmpty(_receipt))
            {
                GenerateReceipt(); // Генеруємо, якщо ще не згенеровано
            }
            Console.WriteLine("\n--- ЧЕК ---");
            Console.WriteLine(_receipt);
            Console.WriteLine("-----------");
        }

        // Демонстрація роботи
        public static void Demo()
        {
            Console.WriteLine("--- Демонстрація Монолітної Системи ---");
            var order = new CafeOrder();
            order.AddItem("Кава", 2);
            order.AddItem("Тістечко", 1);
            order.AddItem("Сік", 1);

            // Спробуємо застосувати знижку 10% (сума > 100)
            order.ApplyDiscount("PERCENTAGE_10");
            order.PrintReceipt();

            Console.WriteLine("\n--- Нове замовлення ---");
            var order2 = new CafeOrder();
            order2.AddItem("Чай", 1);
            // Спробуємо застосувати знижку 10% (сума < 100)
            order2.ApplyDiscount("PERCENTAGE_10");
            order2.PrintReceipt();

            Console.WriteLine("\n--- Замовлення зі знижкою 50 грн ---");
             var order3 = new CafeOrder();
            order3.AddItem("Кава", 3);
            order3.AddItem("Тістечко", 2);
            // Спробуємо застосувати знижку 50 грн (сума > 200)
            order3.ApplyDiscount("FIXED_50");
            order3.PrintReceipt();

        }
    }

    // Головний клас програми для запуску демонстрації
    class Program
    {
        static void Main(string[] args)
        {
            CafeOrder.Demo();
            Console.ReadKey(); // Щоб консоль не закривалася одразу
        }
    }
}