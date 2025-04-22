namespace LibraryManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public abstract class LibraryItem
    {
        public string Title { get; protected set; }
        public Author? Author { get; protected set; }
        public int Year { get; protected set; }
        public string ISBN { get; protected set; }
        public List<Rating> Ratings { get; private set; } = new List<Rating>();

        protected LibraryItem(string title, Author? author, int year, string isbn)
        {
            Title = title;
            Author = author;
            Year = year;
            ISBN = isbn;
            Author?.Items.Add(this);
        }

        public abstract string GetInfo();

        public void AddRating(Rating rating)
        {
            Ratings.Add(rating);
        }

        public double GetAverageRating()
        {
            if (Ratings.Count == 0)
            {
                return 0;
            }
            return Ratings.Average(r => r.Score);
        }
    }
}