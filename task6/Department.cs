namespace LibraryManagementSystem
{
    using System.Collections.Generic;

    public class Department
    {
        public string Name { get; private set; }
        public List<LibraryItem> Items { get; private set; } = new List<LibraryItem>(); // Зроблено public get

        public Department(string name)
        {
            Name = name;
        }

        public void AddItem(LibraryItem item)
        {
            Items.Add(item);
        }

        public void RemoveItem(LibraryItem item)
        {
            Items.Remove(item);
        }

        public List<LibraryItem> GetItems()
        {
            return new List<LibraryItem>(Items);
        }

        public string GetInfo()
        {
            return $"Відділ: {Name}, Кількість елементів: {Items.Count}";
        }
    }
}