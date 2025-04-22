using System;
using System.Collections.Generic;
using System.Linq;

public class BankAccount
{
    private string accountNumber;
    private string ownerName;
    private decimal balance;

    // Конструктор класу
    public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
    {
        accountNumber = accountNumber;
        if (ownerName.Length < 3)
        {
            throw new ArgumentException("Ім'я власника повинно бути не коротше 3 символів.");
        }
        ownerName = ownerName;
        if (initialBalance < 0)
        {
            throw new ArgumentException("Початковий баланс не може бути від'ємним.");
        }
        balance = initialBalance;
    }

    // Гетер для отримання номера рахунку
    public string GetAccountNumber()
    {
        return accountNumber;
    }

    // Гетер для отримання імені власника
    public string GetOwnerName()
    {
        return ownerName;
    }

    // Сетер для оновлення імені власника
    public void SetOwnerName(string newOwnerName)
    {
        if (newOwnerName.Length < 3)
        {
            Console.WriteLine("Помилка: Ім'я власника повинно бути не коротше 3 символів.");
            return;
        }
        ownerName = newOwnerName;
        Console.WriteLine("Ім'я власника успішно оновлено.");
    }

    // Метод для поповнення рахунку
    public void Deposit(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Помилка: Сума поповнення повинна бути більшою за нуль.");
            return;
        }
        balance += amount;
        Console.WriteLine($"Рахунок поповнено на {amount}. Поточний баланс: {balance}");
    }

    // Метод для зняття коштів
    public void Withdraw(decimal amount)
    {
        if (amount <= 0)
        {
            Console.WriteLine("Помилка: Сума зняття повинна бути більшою за нуль.");
            return;
        }
        if (amount > balance)
        {
            Console.WriteLine("Помилка: Недостатньо коштів на рахунку.");
            return;
        }
        balance -= amount;
        Console.WriteLine($"З рахунку знято {amount}. Поточний баланс: {balance}");
    }

    // Гетер для отримання балансу (за потреби)
    public decimal GetBalance()
    {
        return balance;
    }
}

public class Bank
{
    private List<BankAccount> accounts;

    public Bank()
    {
        accounts = new List<BankAccount>();
    }

    public void AddAccount(BankAccount account)
    {
        accounts.Add(account);
    }

    public BankAccount? FindAccount(string? accountNumber)
    {
        if (accountNumber == null)
        {
            return null;
        }
        return accounts.FirstOrDefault(acc => acc.GetAccountNumber() == accountNumber);
    }

    private static int nextAccountNumber = 1000; // Початкове значення для генерації

    public static string GenerateAccountNumber()
    {
        return $"UA{nextAccountNumber++:D8}"; // Формат UA та 8-значний номер
    }

    public List<BankAccount> GetAllAccounts()
    {
        return accounts;
    }
}

public class Task2Interactive
{
    public static void Main(string[] args)
    {
        Bank myBank = new Bank();
        // Створення початкових рахунків
        myBank.AddAccount(new BankAccount(Bank.GenerateAccountNumber(), "Іван Петренко", 1000.50m));
        myBank.AddAccount(new BankAccount(Bank.GenerateAccountNumber(), "Марія Сидоренко", 500.00m));
        myBank.AddAccount(new BankAccount(Bank.GenerateAccountNumber(), "Олена Степаненко", 1200.00m));
        myBank.AddAccount(new BankAccount(Bank.GenerateAccountNumber(), "Петро Іванов", 800.00m));

        bool running = true;
        while (running)
        {
            Console.Clear();
            Console.WriteLine("Керування банківськими рахунками");
            Console.WriteLine("-----------------------------");
            Console.WriteLine("1. Переглянути всі рахунки");
            Console.WriteLine("2. Знайти рахунок за номером");
            Console.WriteLine("3. Змінити ім'я власника рахунку");
            Console.WriteLine("4. Поповнити рахунок");
            Console.WriteLine("5. Зняти кошти з рахунку");
            Console.WriteLine("6. Вийти");
            Console.Write("Оберіть дію: ");
            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    ViewAllAccounts(myBank);
                    break;
                case "2":
                    FindAccountInteractive(myBank);
                    break;
                case "3":
                    UpdateOwnerNameInteractive(myBank);
                    break;
                case "4":
                    DepositInteractive(myBank);
                    break;
                case "5":
                    WithdrawInteractive(myBank);
                    break;
                case "6":
                    running = false;
                    Console.WriteLine("Дякую за використання!");
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    Console.ReadKey();
                    break;
            }
        }
    }

    static void ViewAllAccounts(Bank bank)
    {
        Console.Clear();
        Console.WriteLine("Усі банківські рахунки:");
        List<BankAccount> accounts = bank.GetAllAccounts();
        if (accounts.Count == 0)
        {
            Console.WriteLine("Немає жодних рахунків.");
        }
        else
        {
            foreach (var account in accounts)
            {
                Console.WriteLine($"Номер рахунку: {account.GetAccountNumber()}, Власник: {account.GetOwnerName()}, Баланс: {account.GetBalance()}");
            }
        }
        Console.ReadKey();
    }

    static void FindAccountInteractive(Bank bank)
    {
        Console.Clear();
        Console.Write("Введіть номер рахунку для пошуку: ");
        string? accountNumber = Console.ReadLine();
        BankAccount? foundAccount = bank.FindAccount(accountNumber);
        if (foundAccount != null)
        {
            Console.WriteLine($"Знайдено рахунок: Номер - {foundAccount.GetAccountNumber()}, Власник - {foundAccount.GetOwnerName()}, Баланс - {foundAccount.GetBalance()}");
        }
        else
        {
            Console.WriteLine($"Рахунок з номером {accountNumber} не знайдено.");
        }
        Console.ReadKey();
    }

    static void UpdateOwnerNameInteractive(Bank bank)
    {
        Console.Clear();
        Console.Write("Введіть номер рахунку для оновлення імені власника: ");
        string? accountNumber = Console.ReadLine();
        BankAccount? accountToUpdate = bank.FindAccount(accountNumber);
        if (accountToUpdate != null)
        {
            Console.Write("Введіть нове ім'я власника: ");
            string? newOwnerName = Console.ReadLine();
            if (newOwnerName != null)
            {
                accountToUpdate.SetOwnerName(newOwnerName);
            }
        }
        else
        {
            Console.WriteLine($"Рахунок з номером {accountNumber} не знайдено.");
        }
        Console.ReadKey();
    }

    static void DepositInteractive(Bank bank)
    {
        Console.Clear();
        Console.Write("Введіть номер рахунку для поповнення: ");
        string? accountNumber = Console.ReadLine();
        BankAccount? accountToDeposit = bank.FindAccount(accountNumber);
        if (accountToDeposit != null)
        {
            Console.Write("Введіть суму для поповнення: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                accountToDeposit.Deposit(amount);
            }
            else
            {
                Console.WriteLine("Некоректний формат суми.");
            }
        }
        else
        {
            Console.WriteLine($"Рахунок з номером {accountNumber} не знайдено.");
        }
        Console.ReadKey();
    }

    static void WithdrawInteractive(Bank bank)
    {
        Console.Clear();
        Console.Write("Введіть номер рахунку для зняття коштів: ");
        string? accountNumber = Console.ReadLine();
        BankAccount? accountToWithdraw = bank.FindAccount(accountNumber);
        if (accountToWithdraw != null)
        {
            Console.Write("Введіть суму для зняття: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                accountToWithdraw.Withdraw(amount);
            }
            else
            {
                Console.WriteLine("Некоректний формат суми.");
            }
        }
        else
        {
            Console.WriteLine($"Рахунок з номером {accountNumber} не знайдено.");
        }
        Console.ReadKey();
    }
}