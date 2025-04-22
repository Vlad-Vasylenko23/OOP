namespace LibraryManagementSystem
{
    public class Student : Reader
    {
        public string Faculty { get; private set; }

        public Student(string firstName, string lastName, int readerId, string faculty)
            : base(firstName, lastName, readerId)
        {
            Faculty = faculty;
            MaxBorrowedBooks = 5;
            BorrowDurationDays = 21;
        }

        public override string GetPrivilegesInfo()
        {
            return $"Тип: Студент, Макс. книг: {MaxBorrowedBooks}, Термін позики: {BorrowDurationDays} днів, Факультет: {Faculty}";
        }

        public override int GetMaxBorrowedBooks()
        {
            return MaxBorrowedBooks;
        }

        public override int GetBorrowDurationDays()
        {
            return BorrowDurationDays;
        }
    }
}