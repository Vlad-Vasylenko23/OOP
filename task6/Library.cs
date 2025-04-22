namespace LibraryManagementSystem
{
    using System.Collections.Generic;
    using System.Linq;

    public class Library
    {
        public string Name { get; private set; }
        public string Address { get; private set; }
        public int FoundationYear { get; private set; }
        public List<Department> Departments { get; private set; } = new List<Department>(); // Зроблено public get

        public Library(string name, string address, int foundationYear)
        {
            Name = name;
            Address = address;
            FoundationYear = foundationYear;
        }

        public void AddDepartment(Department department)
        {
            Departments.Add(department);
        }

        public void RemoveDepartment(Department department)
        {
            Departments.Remove(department);
        }

        public void AddItem(string departmentName, LibraryItem item)
        {
            Department? department = Departments.FirstOrDefault(d => d.Name.ToLower() == departmentName.ToLower());
            if (department != null)
            {
                department.AddItem(item);
                Console.WriteLine($"'{item.Title}' додано до відділу '{departmentName}'.");
            }
            else
            {
                Console.WriteLine($"Відділ '{departmentName}' не знайдено.");
            }
        }

        public void RemoveItem(string departmentName, LibraryItem item)
        {
            Department? department = Departments.FirstOrDefault(d => d.Name.ToLower() == departmentName.ToLower());
            if (department != null)
            {
                department.RemoveItem(item);
                Console.WriteLine($"'{item.Title}' видалено з відділу '{departmentName}'.");
            }
            else
            {
                Console.WriteLine($"Відділ '{departmentName}' не знайдено.");
            }
        }

        public void ListItems()
        {
            Console.WriteLine($"Елементи бібліотеки '{Name}':");
            foreach (var department in Departments)
            {
                Console.WriteLine($"\n--- {department.GetInfo()} ---");
                foreach (var item in department.GetItems())
                {
                    Console.WriteLine(item.GetInfo());
                }
            }
        }

        public List<LibraryItem> FindItemsByAuthor(string authorFullName)
        {
            List<LibraryItem> foundItems = new List<LibraryItem>();
            foreach (var department in Departments)
            {
                foundItems.AddRange(department.GetItems().Where(item => item.Author?.GetFullName().ToLower() == authorFullName.ToLower()));
            }
            return foundItems;
        }

        public string GetInfo()
        {
            return $"Назва: {Name}, Адреса: {Address}, Рік заснування: {FoundationYear}, Кількість відділів: {Departments.Count}";
        }
    }
}