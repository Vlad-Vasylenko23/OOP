namespace LibraryManagementSystem
{
    public abstract class Reader
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int ReaderId { get; private set; }
        protected int MaxBorrowedBooks { get; set; }
        protected int BorrowDurationDays { get; set; }

        protected Reader(string firstName, string lastName, int readerId)
        {
            FirstName = firstName;
            LastName = lastName;
            ReaderId = readerId;
        }

        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        public abstract string GetPrivilegesInfo();
        public abstract int GetMaxBorrowedBooks();
        public abstract int GetBorrowDurationDays();
    }
}