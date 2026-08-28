// ============================================================================
// 01_CollectionsBasics.cs
//
// Formål: Vise de fem collection-typer fra Lesson 3 i praksis: Array, List<T>,
// Dictionary<TKey,TValue>, Queue<T> og Stack<T> - samt foreach-iteration og
// et lille kig på LINQ (Where/Select).
//
// OBS: Denne fil er tænkt som et selvstændigt eksempel til gennemgang.
// Opret et nyt konsolprojekt og indsæt hele filens indhold i Program.cs
// (eller kald Kør() fra dit eget Main), hvis du vil køre den for dig selv.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq; // Nødvendig for at kunne bruge Where og Select

namespace Lesson03Examples
{
    public class CollectionsBasics
    {
        public static void Main(string[] args)
        {
            ShowArrayExample();
            Console.WriteLine();

            ShowListExample();
            Console.WriteLine();

            ShowDictionaryExample();
            Console.WriteLine();

            ShowQueueExample();
            Console.WriteLine();

            ShowStackExample();
            Console.WriteLine();

            ShowLinqExample();
        }

        // -------------------------------------------------------------
        // Array: fast størrelse, sat ved oprettelsen.
        // Velegnet når antallet af elementer er kendt og ikke ændrer sig.
        // -------------------------------------------------------------
        private static void ShowArrayExample()
        {
            Console.WriteLine("--- Array ---");

            // Ugens 5 hverdage - antallet ændrer sig aldrig, så et array giver god mening.
            string[] weekdays = { "Mandag", "Tirsdag", "Onsdag", "Torsdag", "Fredag" };

            Console.WriteLine($"Antal hverdage: {weekdays.Length}"); // Bemærk: Length, ikke Count

            // foreach kan bruges til at gennemløbe et array ligesom en List<T>.
            foreach (string day in weekdays)
            {
                Console.WriteLine($"- {day}");
            }

            // Direkte adgang via indeks er meget hurtig, uanset arrayets størrelse.
            Console.WriteLine($"Sidste hverdag: {weekdays[weekdays.Length - 1]}");
        }

        // -------------------------------------------------------------
        // List<T>: dynamisk størrelse - kan vokse og skrumpe under kørslen.
        // Standardvalget, når du "bare" skal holde styr på en samling af noget.
        // -------------------------------------------------------------
        private static void ShowListExample()
        {
            Console.WriteLine("--- List<T> ---");

            List<string> shoppingList = new List<string>();
            shoppingList.Add("Mælk");
            shoppingList.Add("Brød");
            shoppingList.Add("Æg");
            shoppingList.Add("Smør");

            Console.WriteLine($"Antal varer: {shoppingList.Count}"); // Count, ikke Length

            shoppingList.Remove("Brød"); // Fjerner den første forekomst af "Brød"

            foreach (string item in shoppingList)
            {
                Console.WriteLine($"- {item}");
            }

            // Contains gennemløber listen og kigger efter en match - fint til få elementer,
            // men bliver langsommere jo flere elementer listen indeholder.
            bool hasButter = shoppingList.Contains("Smør");
            Console.WriteLine($"Har vi smør på listen? {hasButter}");
        }

        // -------------------------------------------------------------
        // Dictionary<TKey,TValue>: nøgle/værdi-par med hurtigt opslag på nøglen.
        // Velegnet når du ofte skal slå noget bestemt op (fx et kundenummer).
        // -------------------------------------------------------------
        private static void ShowDictionaryExample()
        {
            Console.WriteLine("--- Dictionary<TKey,TValue> ---");

            Dictionary<string, double> exchangeRates = new Dictionary<string, double>();
            exchangeRates.Add("USD", 6.95);
            exchangeRates.Add("EUR", 7.46);
            exchangeRates["GBP"] = 8.72; // Indeksering kan både tilføje og opdatere

            // TryGetValue er ofte det sikreste valg: ét opslag, og du undgår en
            // KeyNotFoundException, hvis nøglen ikke findes.
            if (exchangeRates.TryGetValue("EUR", out double eurRate))
            {
                Console.WriteLine($"1 EUR = {eurRate} kr.");
            }

            // ContainsKey er alternativet, hvis du vil tjekke først og slå op bagefter.
            if (!exchangeRates.ContainsKey("DKK"))
            {
                Console.WriteLine("Vi har ikke en kurs for DKK (giver god mening - det er jo vores egen valuta).");
            }

            // foreach på en Dictionary giver et KeyValuePair<TKey,TValue> per element.
            // Bemærk: der er ingen garanteret rækkefølge.
            foreach (KeyValuePair<string, double> pair in exchangeRates)
            {
                Console.WriteLine($"{pair.Key}: {pair.Value} kr.");
            }
        }

        // -------------------------------------------------------------
        // Queue<T>: FIFO (First In, First Out) - som en kø i en butik.
        // -------------------------------------------------------------
        private static void ShowQueueExample()
        {
            Console.WriteLine("--- Queue<T> ---");

            Queue<string> supportTickets = new Queue<string>();
            supportTickets.Enqueue("Sag #1: Kan ikke logge ind");
            supportTickets.Enqueue("Sag #2: Printer virker ikke");
            supportTickets.Enqueue("Sag #3: Ønsker nyt password");

            Console.WriteLine($"Sager i køen: {supportTickets.Count}");

            // Peek: se forrest i køen uden at fjerne den.
            Console.WriteLine($"Næste sag (uden at fjerne): {supportTickets.Peek()}");

            // Dequeue: behandl og fjern den forreste sag - først ind, først ud.
            while (supportTickets.Count > 0)
            {
                string ticket = supportTickets.Dequeue();
                Console.WriteLine($"Behandler: {ticket}");
            }
        }

        // -------------------------------------------------------------
        // Stack<T>: LIFO (Last In, First Out) - som en bunke tallerkener,
        // eller en fortryd-funktion (Undo).
        // -------------------------------------------------------------
        private static void ShowStackExample()
        {
            Console.WriteLine("--- Stack<T> ---");

            Stack<string> actionHistory = new Stack<string>();
            actionHistory.Push("Åbnede dokument");
            actionHistory.Push("Skrev overskrift");
            actionHistory.Push("Indsatte billede");

            Console.WriteLine($"Antal handlinger i historikken: {actionHistory.Count}");
            Console.WriteLine($"Øverste handling (uden at fjerne): {actionHistory.Peek()}");

            // Pop: fortryd den seneste handling først - sidst ind, først ud.
            while (actionHistory.Count > 0)
            {
                string action = actionHistory.Pop();
                Console.WriteLine($"Fortryder: {action}");
            }
        }

        // -------------------------------------------------------------
        // Et lille kig fremad: LINQ (Where/Select). I møder dette begreb
        // grundigere senere i forløbet - her er blot en smagsprøve.
        // -------------------------------------------------------------
        private static void ShowLinqExample()
        {
            Console.WriteLine("--- LINQ (perspektiv) ---");

            List<int> numbers = new List<int> { 3, 8, 12, 15, 20, 27, 34 };

            // Where: behold kun de elementer, der opfylder betingelsen (her: lige tal).
            List<int> evenNumbers = numbers.Where(t => t % 2 == 0).ToList();
            Console.WriteLine("Lige tal: " + string.Join(", ", evenNumbers));

            // Select: omdan hvert element til noget nyt (her: en tekstbeskrivelse).
            List<string> descriptions = numbers.Select(t => $"Tallet {t} er {(t % 2 == 0 ? "lige" : "ulige")}").ToList();
            foreach (string description in descriptions)
            {
                Console.WriteLine(description);
            }
        }
    }
}
