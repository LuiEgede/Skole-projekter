namespace Lesson03Examples;

public class Book
{
    // Properties med private set, kan læses udefra, men ikke set'es udefra klassen
    public string Title { get; private set; }
    public string Author { get; private set; }
    public string ISBN { get; private set; }
    public bool IsBorrowed { get; private set; }

    public Book(string title, string author, string isbn)
    {
        Title = title;
        Author = author;
        ISBN = isbn;
        IsBorrowed = false;
    }

    public void Borrow()
    {
        if (IsBorrowed)
        {
            throw new InvalidOperationException(
                $"Bogen \"{Title}\" er allerede udlånt.");
        }

        IsBorrowed = true;
    }
    
    public void Return()
    {
        if (!IsBorrowed)
        {
            throw new InvalidOperationException(
                $"Bogen \"{Title}\" er ikke udlånt.");
        }

        IsBorrowed = false;
    }
}
