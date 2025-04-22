namespace LibraryManagementSystem
{
    using System;

    public class DVD : LibraryItem
    {
        public string Format { get; private set; }
        public TimeSpan Duration { get; private set; }

        public DVD(string title, Author? director, int year, string isbn, string format, TimeSpan duration)
            : base(title, director, year, isbn)
        {
            Format = format;
            Duration = duration;
        }

        public override string GetInfo()
        {
            string directorInfo = Author != null ? Author.GetFullName() : "Невідомий";
            return $"DVD - Назва: {Title}, Режисер: {directorInfo}, Рік: {Year}, ISBN: {ISBN}, Формат: {Format}, Тривалість: {Duration}, Рейтинг: {GetAverageRating():F2}";
        }
    }
}