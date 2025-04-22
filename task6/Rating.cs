namespace LibraryManagementSystem
{
    using System;

    public class Rating
    {
        public int Score { get; private set; }
        public string? Review { get; private set; }
        public Reader? User { get; private set; }

        public Rating(int score, string? review = null, Reader? user = null)
        {
            if (score < 1 || score > 5)
            {
                throw new ArgumentException("Оцінка повинна бути від 1 до 5.");
            }
            Score = score;
            Review = review;
            User = user;
        }
    }
}