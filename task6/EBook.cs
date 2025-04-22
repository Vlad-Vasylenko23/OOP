namespace LibraryManagementSystem
{
    public class EBook : Book
    {
        public string FileFormat { get; private set; }
        public double FileSizeMB { get; private set; }

        public EBook(string title, Author? author, int year, string isbn, string fileFormat, double fileSizeMB)
            : base(title, author, year, isbn)
        {
            FileFormat = fileFormat;
            FileSizeMB = fileSizeMB;
        }

        public override string GetInfo()
        {
            return $"{base.GetInfo()}, Формат: {FileFormat}, Розмір: {FileSizeMB} МБ";
        }
    }
}