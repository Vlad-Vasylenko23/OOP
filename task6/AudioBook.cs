namespace LibraryManagementSystem
{
    using System;

    public class AudioBook : LibraryItem
    {
        public TimeSpan Duration { get; private set; }
        public string Narrator { get; private set; }

        public AudioBook(string title, Author? author, int year, string isbn, TimeSpan duration, string narrator)
            : base(title, author, year, isbn)
        {
            Duration = duration;
            Narrator = narrator;
        }

        public override string GetInfo()
        {
            string authorInfo = Author != null ? Author.GetFullName() : "Невідомий";
            return $"Аудіокнига - Назва: {Title}, Автор: {authorInfo}, Рік: {Year}, ISBN: {ISBN}, Тривалість: {Duration}, Озвучив: {Narrator}, Рейтинг: {GetAverageRating():F2}";
        }
    }
}