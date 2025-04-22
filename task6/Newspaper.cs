namespace LibraryManagementSystem
{
    using System;

    public class Newspaper : Periodical
    {
        public Newspaper(string title, Author? publisher, int year, string isbn, int issueNumber, DateTime issueDate)
            : base(title, publisher, year, isbn, issueNumber, issueDate) { }

        public override string GetInfo()
        {
            string publisherInfo = Author != null ? Author.GetFullName() : "Невідомий";
            return $"Газета - Назва: {Title}, Видавець: {publisherInfo}, Рік: {Year}, ISBN: {ISBN}, № випуску: {IssueNumber}, Дата випуску: {IssueDate.ToShortDateString()}, Рейтинг: {GetAverageRating():F2}";
        }
    }
}