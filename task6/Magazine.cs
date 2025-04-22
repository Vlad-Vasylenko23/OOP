namespace LibraryManagementSystem
{
    using System;

    public class Magazine : Periodical
    {
        public string Genre { get; private set; }

        public Magazine(string title, Author? publisher, int year, string isbn, int issueNumber, DateTime issueDate, string genre)
            : base(title, publisher, year, isbn, issueNumber, issueDate)
        {
            Genre = genre;
        }

        public override string GetInfo()
        {
            string publisherInfo = Author != null ? Author.GetFullName() : "Невідомий";
            return $"Журнал - Назва: {Title}, Видавець: {publisherInfo}, Рік: {Year}, ISBN: {ISBN}, № випуску: {IssueNumber}, Дата випуску: {IssueDate.ToShortDateString()}, Жанр: {Genre}, Рейтинг: {GetAverageRating():F2}";
        }
    }
}