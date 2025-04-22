using System;
using System.Collections.Generic;
using System.Linq;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int Year { get; set; }
    public int Pages { get; set; }
    public string Genre { get; set; }

    public Book(string title, string author, int year, int pages, string genre = "")
    {
        Title = title;
        Author = author;
        Year = year;
        Pages = pages;
        Genre = genre;
    }

    public string GetInfo()
    {
        return $"Назва: {Title}, Автор: {Author}, Рік видання: {Year}, Сторінок: {Pages}, Жанр: {Genre}";
    }

    public bool IsModern()
    {
        return Year > 2010;
    }

    public bool BelongsToGenre(string genreToCheck)
    {
        return Genre.ToLower() == genreToCheck.ToLower();
    }

    public int CompareByPages(Book otherBook)
    {
        return Pages.CompareTo(otherBook.Pages);
    }
}

public class Library
{
    private List<Book> books;

    public Library()
    {
        books = new List<Book>();
    }

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public Book FindBookByTitle(string title)
    {
        return books.FirstOrDefault(book => book.Title.ToLower() == title.ToLower());
    }

    public List<Book> GetAllBooks()
    {
        return books;
    }
}

public class Program
{
    public static void Main(string[] args)
    {
        // Створення початкових об'єктів класу Book
        Book book1 = new Book("Маленький принц", "Антуан де Сент-Екзюпері", 1943, 96);
        Book book2 = new Book("Гаррі Поттер і філософський камінь", "Джоан Роулінг", 1997, 328, "Фантастика");
        Book book3 = new Book("Sapiens. Людина розумна", "Юваль Ной Харарі", 2011, 512, "Науково-популярна");
        Book book4 = new Book("Кобзар", "Тарас Шевченко", 1840, 80);
        Book book5 = new Book("Дюна", "Френк Герберт", 1965, 604, "Фантастика");

        // Створення об'єкта бібліотеки та додавання початкових книг
        Library library = new Library();
        library.AddBook(book1);
        library.AddBook(book2);
        library.AddBook(book3);
        library.AddBook(book4);
        library.AddBook(book5);

        Console.WriteLine("Початковий список книг у бібліотеці:");
        int index = 1;
        foreach (Book book in library.GetAllBooks())
        {
            Console.WriteLine($"{index}. {book.GetInfo()}");
            index++;
        }
        Console.WriteLine();

        // Додавання нової книги від користувача
        Console.WriteLine("Бажаєте додати нову книгу? (так/ні)");
        string answer = Console.ReadLine().ToLower();

        if (answer == "так")
        {
            Console.WriteLine("\nВведіть інформацію про нову книгу:");
            Console.Write("Назва: ");
            string newTitle = Console.ReadLine();
            Console.Write("Автор: ");
            string newAuthor = Console.ReadLine();
            Console.Write("Рік видання: ");
            if (!int.TryParse(Console.ReadLine(), out int newYear))
            {
                Console.WriteLine("Некоректний формат року. Нову книгу не додано.");
            }
            else
            {
                Console.Write("Кількість сторінок: ");
                if (!int.TryParse(Console.ReadLine(), out int newPages))
                {
                    Console.WriteLine("Некоректний формат кількості сторінок. Нову книгу не додано.");
                }
                else
                {
                    Console.Write("Жанр: ");
                    string newGenre = Console.ReadLine();

                    Book newBook = new Book(newTitle, newAuthor, newYear, newPages, newGenre);
                    library.AddBook(newBook);

                    Console.WriteLine("\nНову книгу додано до бібліотеки:");
                    Console.WriteLine(newBook.GetInfo());

                    Console.WriteLine("\nОновлений список книг у бібліотеці:");
                    index = 1;
                    foreach (Book book in library.GetAllBooks())
                    {
                        Console.WriteLine($"{index}. {book.GetInfo()}");
                        index++;
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Дякую. Список книг залишається без змін.");
        }

        // Інтерактивне меню тестування з вибором книги
        bool testing = true;
        while (testing)
        {
            Console.WriteLine("\nОберіть дію:");
            Console.WriteLine("1. Вивести інформацію про книгу");
            Console.WriteLine("2. Перевірити, чи книга сучасна");
            Console.WriteLine("3. Перевірити жанр книги");
            Console.WriteLine("4. Порівняти дві книги за кількістю сторінок");
            Console.WriteLine("5. Пошукати книгу за назвою");
            Console.WriteLine("6. Завершити тестування");
            Console.Write("Ваш вибір: ");
            string testChoice = Console.ReadLine();

            switch (testChoice)
            {
                case "1":
                case "2":
                case "3":
                    Console.Write("Введіть номер книги зі списку: ");
                    if (int.TryParse(Console.ReadLine(), out int bookIndex) && bookIndex > 0 && bookIndex <= library.GetAllBooks().Count)
                    {
                        Book selectedBook = library.GetAllBooks()[bookIndex - 1];
                        if (testChoice == "1")
                        {
                            Console.WriteLine("\nІнформація про обрану книгу:");
                            Console.WriteLine(selectedBook.GetInfo());
                        }
                        else if (testChoice == "2")
                        {
                            Console.WriteLine($"\n{selectedBook.Title} - сучасна книга? {selectedBook.IsModern()}");
                        }
                        else if (testChoice == "3")
                        {
                            Console.Write($"Введіть жанр для перевірки книги \"{selectedBook.Title}\": ");
                            string genreToCheck = Console.ReadLine();
                            Console.WriteLine($"{selectedBook.Title} - це {genreToCheck}? {selectedBook.BelongsToGenre(genreToCheck)}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Некоректний номер книги.");
                    }
                    break;
                case "4":
                    if (library.GetAllBooks().Count < 2)
                    {
                        Console.WriteLine("У бібліотеці недостатньо книг для порівняння.");
                    }
                    else
                    {
                        Console.Write("Введіть номер першої книги для порівняння: ");
                        if (int.TryParse(Console.ReadLine(), out int bookIndex1) && bookIndex1 > 0 && bookIndex1 <= library.GetAllBooks().Count)
                        {
                            Console.Write("Введіть номер другої книги для порівняння: ");
                            if (int.TryParse(Console.ReadLine(), out int bookIndex2) && bookIndex2 > 0 && bookIndex2 <= library.GetAllBooks().Count)
                            {
                                Book bookToCompare1 = library.GetAllBooks()[bookIndex1 - 1];
                                Book bookToCompare2 = library.GetAllBooks()[bookIndex2 - 1];
                                Console.WriteLine($"\nПорівняння книг \"{bookToCompare1.Title}\" та \"{bookToCompare2.Title}\":");
                                int comparisonResult = bookToCompare1.CompareByPages(bookToCompare2);
                                if (comparisonResult < 0)
                                {
                                    Console.WriteLine($"{bookToCompare1.Title} має менше сторінок, ніж {bookToCompare2.Title}.");
                                }
                                else if (comparisonResult > 0)
                                {
                                    Console.WriteLine($"{bookToCompare1.Title} має більше сторінок, ніж {bookToCompare2.Title}.");
                                }
                                else
                                {
                                    Console.WriteLine($"{bookToCompare1.Title} та {bookToCompare2.Title} мають однакову кількість сторінок.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Некоректний номер другої книги.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Некоректний номер першої книги.");
                        }
                    }
                    break;
                case "5":
                    Console.Write("\nВведіть назву книги для пошуку: ");
                    string searchTitle = Console.ReadLine();
                    Book foundBook = library.FindBookByTitle(searchTitle);
                    if (foundBook != null)
                    {
                        Console.WriteLine($"Знайдено книгу: {foundBook.GetInfo()}");
                    }
                    else
                    {
                        Console.WriteLine("Книгу не знайдено.");
                    }
                    break;
                case "6":
                    testing = false;
                    Console.WriteLine("Завершення тестування.");
                    break;
                default:
                    Console.WriteLine("Некоректний вибір. Спробуйте ще раз.");
                    break;
            }
        }
    }
}