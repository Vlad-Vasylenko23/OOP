namespace LibraryManagementSystem
{
    public class Employee : Reader
    {
        public string Department { get; private set; }

        public Employee(string firstName, string lastName, int readerId, string department)
            : base(firstName, lastName, readerId)
        {
            Department = department;
            MaxBorrowedBooks = 10;
            BorrowDurationDays = 30;
        }

        public override string GetPrivilegesInfo()
        {
            return $"Тип: Працівник, Макс. книг: {MaxBorrowedBooks}, Термін позики: {BorrowDurationDays} днів, Відділ: {Department}";
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