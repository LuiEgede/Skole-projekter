// ============================================================================
// 03_InventoryCombinedExample.cs
//
// Formål: Vise, hvordan collections og exception handling kombineres i en
// lille, sammenhængende case: et lille lagersystem, hvor brugeren kan slå
// varer op, trække fra lager og se en historik over de seneste handlinger.
//
// Bemærk domænet: dette eksempel bruger et LAGER (varer/beholdning), mens
// opgaven i opgave.md handler om et BIBLIOTEK (bøger/udlån). Det er bevidst,
// så du kan bruge dette eksempel som inspiration uden bare at kunne kopiere
// løsningen til opgaven.
//
// Collections i brug:
// - List<Product>            : den fulde liste af varer, i den rækkefølge de blev oprettet
// - Dictionary<string,Product>: hurtigt opslag på varenummer (nøgle)
// - Stack<string>             : historik over de seneste handlinger (nyeste først - "fortryd"-stil)
//
// OBS: Denne fil er tænkt som et selvstændigt eksempel til gennemgang.
// Opret et nyt konsolprojekt og indsæt hele filens indhold i Program.cs
// (eller kald Kør() fra dit eget Main), hvis du vil køre den for dig selv.
// ============================================================================

using System;
using System.Collections.Generic;
using System.Linq;

namespace Lesson03Examples
{
    // ------------------------------------------------------------------
    // Egen exception-klasse med ekstra data (varenummer og ønsket antal),
    // så kaldende kode kan give en præcis, informativ fejlbesked.
    // ------------------------------------------------------------------
    public class InsufficientStockException : Exception
    {
        public string ProductNumber { get; }
        public int QuantityInStock { get; }
        public int RequestedQuantity { get; }

        public InsufficientStockException(string productNumber, int quantityInStock, int requestedQuantity)
            : base($"Vare '{productNumber}' har kun {quantityInStock} stk. på lager - der blev bedt om {requestedQuantity} stk.")
        {
            ProductNumber = productNumber;
            QuantityInStock = quantityInStock;
            RequestedQuantity = requestedQuantity;
        }
    }

    // ------------------------------------------------------------------
    // Simpelt objekt til at repræsentere en vare på lageret.
    // Indkapsling: felterne kan kun ændres via metoder/properties, jf. dag 2.
    // ------------------------------------------------------------------
    public class Product
    {
        public string ProductNumber { get; }
        public string Name { get; }
        public int QuantityInStock { get; private set; }

        public Product(string productNumber, string name, int quantityInStock)
        {
            ProductNumber = productNumber;
            Name = name;
            QuantityInStock = quantityInStock;
        }

        public void RemoveStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Antal, der trækkes fra lager, skal være positivt.");
            }

            if (quantity > QuantityInStock)
            {
                throw new InsufficientStockException(ProductNumber, QuantityInStock, quantity);
            }

            QuantityInStock -= quantity;
        }

        public void AddStock(int quantity)
        {
            if (quantity <= 0)
            {
                throw new ArgumentException("Antal, der lægges på lager, skal være positivt.");
            }

            QuantityInStock += quantity;
        }

        public override string ToString()
        {
            return $"{ProductNumber} - {Name} ({QuantityInStock} stk. på lager)";
        }
    }

    // ------------------------------------------------------------------
    // Selve lagersystemet - kombinerer flere collections til forskellige formål.
    // ------------------------------------------------------------------
    public class Inventory
    {
        // Den fulde liste over varer, i den rækkefølge de er oprettet.
        // Bruges bl.a. til at vise alt og til LINQ-forespørgsler.
        private readonly List<Product> _products = new List<Product>();

        // Opslagsstruktur: giver hurtigt opslag på varenummer i stedet for
        // at gennemløbe hele listen, hver gang vi skal finde en bestemt vare.
        private readonly Dictionary<string, Product> _productsByNumber = new Dictionary<string, Product>();

        // Historik over de seneste handlinger. En Stack er velvalgt her,
        // fordi vi typisk er interesserede i de SENESTE handlinger først.
        private readonly Stack<string> _actionHistory = new Stack<string>();

        public void AddProduct(Product product)
        {
            // Varenumre skal være unikke - det er derfor naturligt at bruge dem
            // som nøgle i en Dictionary. Vi tjekker selv for at give en tydelig
            // fejlbesked, i stedet for at lade Dictionary'ets egen exception
            // (ArgumentException, hvis nøglen findes i forvejen) forklare det.
            if (_productsByNumber.ContainsKey(product.ProductNumber))
            {
                throw new ArgumentException($"Der findes allerede en vare med varenummer '{product.ProductNumber}'.");
            }

            _products.Add(product);
            _productsByNumber.Add(product.ProductNumber, product);
            RegisterAction($"Oprettede vare {product.ProductNumber} ({product.Name})");
        }

        // Opslag via Dictionary - hurtigt, uanset hvor mange varer lageret har.
        public Product FindProduct(string productNumber)
        {
            if (_productsByNumber.TryGetValue(productNumber, out Product product))
            {
                return product;
            }

            // KeyNotFoundException ville også kunne opstå automatisk ved
            // _productsByNumber[productNumber], men her kaster vi en mere sigende
            // exception-type selv, så kaldende kode nemt kan skelne den fra
            // andre fejl.
            throw new ArgumentException($"Ingen vare fundet med varenummer '{productNumber}'.");
        }

        public void RemoveStock(string productNumber, int quantity)
        {
            Product product = FindProduct(productNumber); // Kan kaste ArgumentException, hvis varen ikke findes
            product.RemoveStock(quantity);                 // Kan kaste InsufficientStockException
            RegisterAction($"Trak {quantity} stk. fra {productNumber}");
        }

        public void ShowAllProducts()
        {
            Console.WriteLine("--- Alle varer på lager ---");
            foreach (Product product in _products)
            {
                Console.WriteLine(product);
            }
        }

        // LINQ-perspektiv: Where bruges til at filtrere, Select til at omdanne.
        public void ShowProductsWithLowStock(int threshold)
        {
            List<Product> lowStock = _products.Where(v => v.QuantityInStock < threshold).ToList();

            Console.WriteLine($"--- Varer med færre end {threshold} stk. på lager ---");
            List<string> descriptions = lowStock.Select(v => $"{v.Name}: kun {v.QuantityInStock} stk. tilbage!").ToList();

            foreach (string description in descriptions)
            {
                Console.WriteLine(description);
            }
        }

        private void RegisterAction(string description)
        {
            _actionHistory.Push(description);
        }

        public void ShowRecentActions(int count)
        {
            Console.WriteLine($"--- De {count} seneste handlinger (nyeste først) ---");

            // Vi vil ikke tømme selve historikken, så vi arbejder på en kopi.
            // ToArray() laver en kopi af stakkens indhold i "Pop"-rækkefølge (nyeste først).
            string[] copy = _actionHistory.ToArray();

            for (int i = 0; i < count && i < copy.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {copy[i]}");
            }
        }
    }

    public class InventoryCombinedExample
    {
        public static void Main(string[] args)
        {
            Inventory inventory = new Inventory();

            // Opbyg en lille startbeholdning.
            inventory.AddProduct(new Product("V001", "Skruetrækker", 12));
            inventory.AddProduct(new Product("V002", "Hammer", 3));
            inventory.AddProduct(new Product("V003", "Save", 7));

            inventory.ShowAllProducts();
            Console.WriteLine();

            // --- Robust håndtering af brugerinput ---
            // Vi beder om et varenummer og et antal, og håndterer alt, der kan gå galt:
            // forkert format på tal, negative tal, varenummer der ikke findes,
            // og for stort et antal i forhold til beholdningen.
            Console.Write("Indtast varenummer, du vil trække fra (fx V002, eller 'XYZ' for at se fejlhåndtering): ");
            string productNumber = Console.ReadLine();

            Console.Write("Indtast antal, du vil trække fra (prøv fx et bogstav eller et meget stort tal): ");
            string quantityInput = Console.ReadLine();

            try
            {
                int quantity = int.Parse(quantityInput); // Kan kaste FormatException
                inventory.RemoveStock(productNumber, quantity); // Kan kaste ArgumentException eller InsufficientStockException
                Console.WriteLine("Lagertræk gennemført.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Antal skal være et helt tal.");
            }
            catch (InsufficientStockException ex)
            {
                // Mest specifik exception-type først: giver adgang til ekstra data.
                Console.WriteLine(ex.Message);
                Console.WriteLine($"Du kan højst trække {ex.QuantityInStock} stk. fra lige nu.");
            }
            catch (ArgumentException ex)
            {
                // Fanger både "varen findes ikke" og "antal skal være positivt".
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Forsøget på at trække fra lager er afsluttet.");
            }

            Console.WriteLine();
            inventory.ShowAllProducts();
            Console.WriteLine();

            inventory.ShowProductsWithLowStock(5);
            Console.WriteLine();

            inventory.ShowRecentActions(3);
        }
    }
}
