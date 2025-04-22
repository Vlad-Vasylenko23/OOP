namespace LibraryManagementSystem
{
    using System;

    public abstract class Periodical : LibraryItem
    {
        public int IssueNumber { get; protected set; }
        public DateTime IssueDate { get; protected set; }

        protected Periodical(string title, Author? publisher, int year, string isbn, int issueNumber, DateTime issueDate)
            : base(title, publisher, year, isbn)
        {
            IssueNumber = issueNumber;
            IssueDate = issueDate;
        }

        public override abstract string GetInfo();
    }
}