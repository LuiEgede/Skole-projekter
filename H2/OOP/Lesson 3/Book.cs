namespace Lesson03Examples;

public class Book
{
    public string Title { get; set; }
    public string Author { get; set; }
    public string ISBN { get; set; }
    public bool IsBorrowed { get; set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsBorrowed = false;
    }
}