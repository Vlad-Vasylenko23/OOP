namespace LibraryManagementSystem
{
    public class Book : LibraryItem
    {
        public Book(string title, Author? author, int year, string isbn)
            : base(title, author, year, isbn) { }

        public override string GetInfo()
        {
            string authorInfo = Author != null ? Author.GetFullName() : "Невідомий";
            return $"Книга - Назва: {Title}, Автор: {authorInfo}, Рік: {Year}, ISBN: {ISBN}, Рейтинг: {GetAverageRating():F2}";
        }
    }
}