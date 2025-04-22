using System;
using System.Collections.Generic;
using System.Linq;

// Клас Author
public class Author
{
    public string name;
    public int birth_year;

    public Author(string name, int birth_year)
    {
        this.name = name;
        this.birth_year = birth_year;
    }

    public string get_info()
    {
        return $"Ім'я: {name}, Рік народження: {birth_year}";
    }
}

// Клас Book
public class Book
{
    public string title;
    public Author? author; // Агрегація: книга має автора, але може існувати без нього (nullable)
    public int year;
    public string annotation;

    public Book(string title, Author? author, int year, string annotation = "")
    {
        this.title = title;
        this.author = author;
        this.year = year;
        this.annotation = annotation;
    }

    public string get_info()
    {
        string authorInfo = author != null ? author.name : "Невідомий";
        string output = $"Назва: {title}, Рік видання: {year}, Автор: {authorInfo}";
        if (!string.IsNullOrEmpty(annotation))
        {
            output += $"\n{annotation}";
        }
        return output;
    }
}

// Клас Library
public class Library
{
    public string name;
    private List<Book> books; // Композиція: бібліотека містить книги

    public Library(string name)
    {
        this.name = name;
        this.books = new List<Book>();
    }

    public void add_book(Book book)
    {
        // Розширене завдання: Перевірка на дублювання книги
        if (!books.Any(b => b.title.ToLower() == book.title.ToLower() &&
                            (b.author?.name?.ToLower() == book.author?.name?.ToLower())))
        {
            books.Add(book);
            Console.WriteLine($"Книга '{book.title}' додана до бібліотеки '{name}'.");
        }
        else
        {
            Console.WriteLine($"Помилка: Книга '{book.title}' автора '{book.author?.name ?? "Невідомий"}' вже є в бібліотеці.");
        }
    }

    public List<string> list_books()
    {
        List<string> bookInfos = new List<string>();
        if (books.Count == 0)
        {
            bookInfos.Add($"У бібліотеці '{name}' немає книг.");
        }
        else
        {
            bookInfos.Add($"Книги в бібліотеці '{name}':");
            foreach (var book in books)
            {
                bookInfos.Add(book.get_info());
            }
        }
        return bookInfos;
    }

    public List<Book> find_books_by_author(string author_name)
    {
        return books.Where(book => book.author != null && book.author.name.ToLower() == author_name.ToLower()).ToList();
    }

    // Розширене завдання: Пошук книг за роком видання
    public List<Book> find_books_by_year(int year)
    {
        return books.Where(book => book.year == year).ToList();
    }

    // Розширене завдання: Пошук книг за ключовими словами у назві
    public List<Book> find_books_by_keyword(string keyword)
    {
        return books.Where(book => book.title.ToLower().Contains(keyword.ToLower())).ToList();
    }

    // Розширене завдання: Метод для видалення книги
    public void remove_book(string title, string? authorName)
    {
        Book? bookToRemove = books.FirstOrDefault(b => b.title.ToLower() == title.ToLower() &&
                                                     (b.author?.name?.ToLower() == authorName?.ToLower()));
        if (bookToRemove != null)
        {
            books.Remove(bookToRemove);
            Console.WriteLine($"Книга '{title}' автора '{authorName ?? "Невідомий"}' видалена з бібліотеки '{name}'.");
        }
        else
        {
            Console.WriteLine($"Книга '{title}' автора '{authorName ?? "Невідомий"}' не знайдена в бібліотеці '{name}'.");
        }
    }
}

public class Task5
{
    public static void Main(string[] args)
    {
        // Створення об'єктів Author
        Author author1 = new Author("Джон Р. Р. Толкін", 1892);
        Author author2 = new Author("Агата Крісті", 1890);
        Author author3 = new Author("Джордж Орвелл", 1903);

        // Створення об'єктів Book
        Book book1 = new Book("Володар перснів: Братерство Персня", author1, 1954, "Перша частина епопеї про Середзем'я.");
        Book book2 = new Book("Десять негренят", author2, 1939, "Класичний детектив.");
        Book book3 = new Book("1984", author3, 1949, "Антиутопічний роман.");
        Book book4 = new Book("Гобіт, або Туди і Звідти", author1, 1937);
        Book book5 = new Book("Вбивство у Східному експресі", author2, 1934);

        // Створення об'єкта Library
        Library cityLibrary = new Library("Міська бібліотека");

        // Додавання книг до бібліотеки
        cityLibrary.add_book(book1);
        cityLibrary.add_book(book2);
        cityLibrary.add_book(book3);
        cityLibrary.add_book(book4);
        cityLibrary.add_book(book5);
        cityLibrary.add_book(new Book("Володар перснів: Братерство Персня", author1, 1954, "Спроба додати дублікат."));

        Console.WriteLine("\n--- Список усіх книг у бібліотеці ---");
        foreach (var info in cityLibrary.list_books())
        {
            Console.WriteLine(info);
        }

        Console.WriteLine("\n--- Пошук книг за автором 'Джон Р. Р. Толкін' ---");
        List<Book> tolkienBooks = cityLibrary.find_books_by_author("Джон Р. Р. Толкін");
        if (tolkienBooks.Any())
        {
            foreach (var book in tolkienBooks)
            {
                Console.WriteLine(book.get_info());
            }
        }
        else
        {
            Console.WriteLine("Книги цього автора не знайдено.");
        }

        Console.WriteLine("\n--- Пошук книг за роком видання 1939 ---");
        List<Book> year1939Books = cityLibrary.find_books_by_year(1939);
        if (year1939Books.Any())
        {
            foreach (var book in year1939Books)
            {
                Console.WriteLine(book.get_info());
            }
        }
        else
        {
            Console.WriteLine("Книг за цей рік не знайдено.");
        }

        Console.WriteLine("\n--- Пошук книг за ключовим словом 'вбивство' ---");
        List<Book> keywordBooks = cityLibrary.find_books_by_keyword("вбивство");
        if (keywordBooks.Any())
        {
            foreach (var book in keywordBooks)
            {
                Console.WriteLine(book.get_info());
            }
        }
        else
        {
            Console.WriteLine("Книг з таким ключовим словом не знайдено.");
        }

        Console.WriteLine("\n--- Видалення книги 'Гобіт, або Туди і Звідти' автора 'Джон Р. Р. Толкін' ---");
        cityLibrary.remove_book("Гобіт, або Туди і Звідти", "Джон Р. Р. Толкін");

        Console.WriteLine("\n--- Список книг після видалення ---");
        foreach (var info in cityLibrary.list_books())
        {
            Console.WriteLine(info);
        }

        Console.WriteLine("\n--- Спроба видалити неіснуючу книгу ---");
        cityLibrary.remove_book("Неіснуюча книга", "Невідомий автор");
    }
}