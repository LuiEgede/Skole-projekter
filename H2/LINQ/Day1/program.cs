using System;
using System.Collections.Generic;

class Product
{
    public string Name { get; set; }
    public string Department { get; set; }
    public decimal Price { get; set; }

    public Product(string name, string department, decimal price)
    {
        Name = name;
        Department = department;
        Price = price;
    }
}

class Program
{
    static void Main()
    {
        var products = new List<Product>
        {
            new Product("Gaming Laptop", "Computer", 12500m),
            new Product("Office Laptop", "Computer", 7500m),
            new Product("Gaming Mus", "Tilbehør", 650m),
            new Product("Keyboard", "Tilbehør", 1100m),
            new Product("4K Skærm", "Skærm", 4500m)
        };

        // Lambda expression to check if a product is more than 5000
        Func<Product, bool> isExpensive = product => product.Price > 5000m;
        // Lambda expression and the use of Where() to filter products that are more than 5000
        var expensiveProducts = products.Where(product => product.Price > 5000m);

        // foreach loop where i use the isExpensive lambda
        foreach (var product in products)
        {
            if (isExpensive(product))
            {
                Console.WriteLine($"Products that are more than 5000: {product.Name} - {product.Price}");
            }
        }

        // foreach loop where i use the Where() method
        foreach (var product in products)
        {
            if (expensiveProducts.Contains(product))
            {
                Console.WriteLine($"Products that are more than 5000: {product.Name} - {product.Price}");
            }
        }




    }
}

