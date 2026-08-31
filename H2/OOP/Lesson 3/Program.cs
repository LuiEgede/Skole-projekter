namespace Lesson03Examples;

using System;

class Program
{
    static void Main()
    {
        Library library = new Library();

        bool running = true;

        while (running)
        {
            Console.WriteLine("\n======================");
            Console.WriteLine("       BIBLIOTEK");
            Console.WriteLine("======================");
            Console.WriteLine("1. Vis alle bøger");
            Console.WriteLine("2. Søg efter en bog");
            Console.WriteLine("3. Lån en bog");
            Console.WriteLine("4. Aflever en bog");
            Console.WriteLine("5. Afslut");
            Console.WriteLine("======================");

            try
            {
                Console.Write("Vælg et menupunkt (1-5): ");
                string? input = Console.ReadLine();

                if (!int.TryParse(input, out int choice))
                {
                    throw new FormatException(
                        "Du skal indtaste et tal mellem 1 og 5."
                    );
                }

                if (choice < 1 || choice > 5)
                {
                    throw new ArgumentOutOfRangeException(
                        "choice",
                        "Du skal vælge et tal mellem 1 og 5."
                    );
                }

                switch (choice)
                {
                    case 1:
                        library.ShowAllBooks();
                        break;

                    case 2:
                        Console.Write("Indtast titel eller del af titel: ");
                        string? searchText = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(searchText))
                        {
                            Console.WriteLine("Du skal indtaste noget at søge efter.");
                            break;
                        }

                        library.SearchBook(searchText);
                        break;

                    case 3:
                        Console.Write("Indtast ISBN på den bog, du vil låne: ");
                        string? borrowISBN = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(borrowISBN))
                        {
                            Console.WriteLine("ISBN må ikke være tomt.");
                            break;
                        }

                        try
                        {
                            library.BorrowBook(borrowISBN);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Fejl: {ex.Message}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Fejl: {ex.Message}");
                        }

                        break;

                    case 4:
                        Console.Write("Indtast ISBN på den bog, du vil aflevere: ");
                        string? returnISBN = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(returnISBN))
                        {
                            Console.WriteLine("ISBN må ikke være tomt.");
                            break;
                        }

                        try
                        {
                            library.ReturnBook(returnISBN);
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Fejl: {ex.Message}");
                        }
                        catch (InvalidOperationException ex)
                        {
                            Console.WriteLine($"Fejl: {ex.Message}");
                        }

                        break;

                    case 5:
                        running = false;
                        Console.WriteLine("Programmet afsluttes. Farvel!");
                        break;
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            catch (ArgumentOutOfRangeException ex)
            {
                Console.WriteLine($"Fejl: {ex.Message}");
            }
            finally
            {
                if (running)
                {
                    Console.WriteLine("\nTryk Enter for at fortsætte...");
                    Console.ReadLine();
                }
            }
        }
    }
}