namespace Lesson03Examples;

using System;
using System.Collections.Generic;

public class Library
{
    private List<Book> books = new List<Book>();

    public Library()
    {
        books.Add(new Book("Harry Potter og De Vises Sten", "J.K. Rowling", "9780747532699"));
        books.Add(new Book("Ringenes Herre", "J.R.R. Tolkien", "9780261102385"));
        books.Add(new Book("Da Vinci Mysteriet", "Dan Brown", "9780552149518"));
        books.Add(new Book("The Hobbit", "J.R.R. Tolkien", "9780261102217"));
        books.Add(new Book("1984", "George Orwell", "9780451524935"));
        books.Add(new Book("To Kill a Mockingbird", "Harper Lee", "9780061120084"));
    }

    public void ShowAllBooks()
    {
        Console.WriteLine("\n--- Alle bøger ---");

        foreach (Book book in books)
        {
            string status = book.IsBorrowed ? "Udlånt" : "Ledig";

            Console.WriteLine(
                $"Titel: {book.Title} | Forfatter: {book.Author} | ISBN: {book.ISBN} | Status: {status}"
            );
        }
    }

    public void SearchBook(string searchText)
    {
        Console.WriteLine("\n--- Søgeresultat ---");

        bool found = false;

        foreach (Book book in books)
        {
            if (book.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            {
                string status = book.IsBorrowed ? "Udlånt" : "Ledig";

                Console.WriteLine(
                    $"Titel: {book.Title} | Forfatter: {book.Author} | ISBN: {book.ISBN} | Status: {status}"
                );

                found = true;
            }
        }

        if (!found)
        {
            Console.WriteLine("Ingen bøger blev fundet.");
        }
    }

    public void BorrowBook(string isbn)
    {
        Book? book = FindBookByISBN(isbn);

        if (book == null)
        {
            throw new ArgumentException("ISBN findes ikke i biblioteket.");
        }

        if (book.IsBorrowed)
        {
            throw new InvalidOperationException(
                $"Bogen \"{book.Title}\" er allerede udlånt."
            );
        }

        book.IsBorrowed = true;

        Console.WriteLine($"Du har lånt \"{book.Title}\".");
    }

    public void ReturnBook(string isbn)
    {
        Book? book = FindBookByISBN(isbn);

        if (book == null)
        {
            throw new ArgumentException("ISBN findes ikke i biblioteket.");
        }

        if (!book.IsBorrowed)
        {
            throw new InvalidOperationException(
                $"Bogen \"{book.Title}\" er ikke udlånt."
            );
        }

        book.IsBorrowed = false;

        Console.WriteLine($"Du har afleveret \"{book.Title}\".");
    }

    private Book? FindBookByISBN(string isbn)
    {
        foreach (Book book in books)
        {
            if (book.ISBN == isbn)
            {
                return book;
            }
        }

        return null;
    }
}