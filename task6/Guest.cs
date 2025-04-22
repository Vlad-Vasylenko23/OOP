namespace LibraryManagementSystem
{
    public class Guest : Reader
    {
        public string Organization { get; private set; }

        public Guest(string firstName, string lastName, int readerId, string organization)
            : base(firstName, lastName, readerId)
        {
            Organization = organization;
            MaxBorrowedBooks = 2;
            BorrowDurationDays = 14;
        }

        public override string GetPrivilegesInfo()
        {
            return $"Тип: Гість, Макс. книг: {MaxBorrowedBooks}, Термін позики: {BorrowDurationDays} днів, Організація: {Organization}";
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