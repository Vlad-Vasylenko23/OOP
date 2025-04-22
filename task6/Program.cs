using System;
using LibraryManagementSystem;

namespace LibraryManagementSystem
{
    public class Task6Complete
    {
        public static void Main(string[] args)
        {
            // Створення авторів
            Author author1 = new Author("Джон", "Толкін", new DateTime(1892, 1, 3));
            Author author2 = new Author("Агата", "Крісті", new DateTime(1890, 9, 15));
            Author nationalGeographic = new Author("National", "Geographic", new DateTime(1888, 1, 1));
            Author markTwain = new Author("Марк", "Твен", new DateTime(1835, 11, 30));

            // Створення елементів бібліотеки
            Book book1 = new Book("Володар перснів", author1, 1954, "978-0618260274");
            EBook ebook1 = new EBook("Гобіт", author1, 1937, "978-0547928227", "EPUB", 2.5);
            DVD dvd1 = new DVD("Планета Земля", nationalGeographic, 2006, "978-1420793630", "DVD", new TimeSpan(11, 0, 0));
            AudioBook audiobook1 = new AudioBook("Вбивство у Східному експресі", author2, 1934, "978-0062327983", new TimeSpan(8, 30, 0), "Девід Суше");
            Magazine magazine1 = new Magazine("National Geographic", nationalGeographic, 2024, "ISSN 0027-9358", 10, new DateTime(2024, 10, 1), "Науково-популярний");
            Newspaper newspaper1 = new Newspaper("The New York Times", null, 2025, "ISSN 0362-4331", 150, new DateTime(2025, 4, 22));
            Book book2 = new Book("Пригоди Тома Сойєра", markTwain, 1876, "978-1598184855");

            // Створення читачів
            Student student1 = new Student("Іван", "Петренко", 12345, "Інформатика");
            Employee employee1 = new Employee("Марія", "Сидоренко", 67890, "Адміністрація");
            Guest guest1 = new Guest("Олег", "Іванов", 11223, "Гостьова організація");

            // Створення рейтингів з користувачами
            book1.AddRating(new Rating(5, "Чудова книга!", student1));
            book1.AddRating(new Rating(4, null, employee1));
            ebook1.AddRating(new Rating(5, "Легко читається на планшеті.", student1));
            dvd1.AddRating(new Rating(4, "Захоплюючі кадри.", guest1));
            magazine1.AddRating(new Rating(3, "Цікавий випуск.", guest1));

            // Створення відділів
            Department fictionDepartment = new Department("Художня література");
            Department documentaryDepartment = new Department("Документалістика");
            Department periodicalsDepartment = new Department("Періодичні видання");

            // Додавання елементів до відділів
            fictionDepartment.AddItem(book1);
            fictionDepartment.AddItem(ebook1);
            fictionDepartment.AddItem(audiobook1);
            fictionDepartment.AddItem(book2);
            documentaryDepartment.AddItem(dvd1);
            periodicalsDepartment.AddItem(magazine1);
            periodicalsDepartment.AddItem(newspaper1);

            // Створення бібліотеки
            Library cityLibrary = new Library("Центральна Бібліотека", "вул. Банкова, 10", 1980);

            // Додавання відділів до бібліотеки
            cityLibrary.AddDepartment(fictionDepartment);
            cityLibrary.AddDepartment(documentaryDepartment);
            cityLibrary.AddDepartment(periodicalsDepartment);

            // Виведення інформації про бібліотеку
            Console.WriteLine(cityLibrary.GetInfo());
            Console.WriteLine("\n--- Елементи бібліотеки ---");
            cityLibrary.ListItems();

            Console.WriteLine("\n--- Пошук елементів автора Джона Толкіна ---");
            List<LibraryItem> tolkienItems = cityLibrary.FindItemsByAuthor("Джон Толкін");
            foreach (var item in tolkienItems)
            {
                Console.WriteLine(item.GetInfo());
            }

            Console.WriteLine("\n--- Інформація про читачів та їхні привілеї ---");
            Console.WriteLine(student1.GetFullName() + ": " + student1.GetPrivilegesInfo());
            Console.WriteLine(employee1.GetFullName() + ": " + employee1.GetPrivilegesInfo());
            Console.WriteLine(guest1.GetFullName() + ": " + guest1.GetPrivilegesInfo());

            // Приклад отримання середнього рейтингу книги
            Console.WriteLine($"\nСередній рейтинг книги '{book1.Title}': {book1.GetAverageRating():F2}");
            Console.WriteLine($"Середній рейтинг DVD '{dvd1.Title}': {dvd1.GetAverageRating():F2}");
            Console.WriteLine($"Середній рейтинг журналу '{magazine1.Title}': {magazine1.GetAverageRating():F2}");
        }
    }
}