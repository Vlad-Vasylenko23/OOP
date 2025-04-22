namespace LibraryManagementSystem
{
    using System;
    using System.Collections.Generic;

    public class Author
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public DateTime BirthDate { get; private set; }
        public List<LibraryItem> Items { get; private set; } = new List<LibraryItem>();

        public Author(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public string GetInfo()
        {
            return $"Ім'я: {FirstName}, Прізвище: {LastName}, Дата народження: {BirthDate.ToShortDateString()}";
        }
    }
}