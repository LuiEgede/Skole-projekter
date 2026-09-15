namespace Day2;

using System;
using System.Collections.Generic;


class Program
{
    static void Main()
    {
        // Task 1:
        var products = ProductData.GetProducts();

        var computers = products.Where(product => product.Category == "Computer");
        var expensiveProducts = products.Where(product => product.Price > 5000m);
        var midRangeProducts = products.Where(product => product.Price >= 1000m && product.Price <= 5000m);
        var accessoriesOver1000 = products.Where(product => product.Category == "Tilbehør" && product.Price > 1000m);
        var gamingProducts = products.Where(product => product.Name.Contains("Gaming"));


        // Task 2 A:
        var productNames = products.Select(product => product.Name);

        foreach (var name in productNames)
        {
            Console.WriteLine(name);
        }

        // Task 2 B:
        var nameAndPrice = products.Select(product => new
        {
            product.Name,
            product.Price
        });

        foreach (var item in nameAndPrice)
        {
            Console.WriteLine($"{item.Name} - {item.Price} kr.");
        }

        // Task 2 C:
        var productInfo = products.Select(product => new
        {
            product.Name,
            product.Category,
            product.Price
        });

        foreach (var item in productInfo)
        {
            Console.WriteLine($"{item.Name} - {item.Category} - {item.Price} kr.");
        }

        // Task 2 D:
        var formattedProducts = products.Select(product =>
            $"{product.Name} koster {product.Price} kr.");

        foreach (var item in formattedProducts)
        {
            Console.WriteLine(item);
        }



        // Task 3 A Sort by ASC price:
        var sortByPriceAscending = products.OrderBy(product => product.Price);

        // Task 3 B Sort by DESC price:
        var sortByPriceDescending = products.OrderByDescending(product => product.Price);

        // Task 3 C Sort by category
        var sortByCategory = products.OrderBy(product => product.Category);

        // Task 3 D Sort by category and then by price
        var sortByCategoryThenPrice = products
            .OrderBy(product => product.Category)
            .ThenBy(product => product.Price);

        // Task 3 E Sort first by category and then by product name
        var sortByCategoryThenName = products
            .OrderBy(product => product.Category)
            .ThenBy(product => product.Name);

        // Task 4 A Find all categories from products
        var categories = products.Select(product => product.Category).Distinct();

        // Task 4 B Count the exiting categories
        var categoryCount = categories.Count();
        Console.WriteLine($"Number of different categories: {categoryCount}");

        // Task 5 A Check if product > 10000
        bool hasProductOver10000 = products.Any(product => product.Price > 10000m);

        // Task 5 B Check if there is a product in category "skærm"
        bool hasScreenProduct = products.Any(product => product.Category == "Skærm");

        // Task 5 C Check if ALL products are over 500
        bool allProductsOver500 = products.All(product => product.Price > 500m);

        // Task 5 Check if all computers cost more than 5000
        bool allComputersOver5000 = products
            .Where(product => product.Category == "Computer")
            .All(product => product.Price > 5000m);

        // Task 6 A Count total products
        var totalProducts = products.Count();

        // Task 6 B The total value of all products
        var totalValue = products.Sum(product => product.Price);

        // Task 6 C Average price of all products
        var averagePrice = products.Average(product => product.Price);

        // Task 6 D Cheapest product
        var cheapestProduct = products.OrderBy(product => product.Price).First();

        // Task 6 E Most expensive product
        var mostExpensiveProduct = products.OrderByDescending(product => product.Price).First();

        // Task 6 F Count amount of computers
        var computerCount = products.Count(product => product.Category == "Computer");

        // Task 6 G Average price of products in the category "Tilbehør"
        var accessoriesAveragePrice = products
            .Where(product => product.Category == "Tilbehør")
            .Average(product => product.Price);

        // Task 7 A Group products by category
        var productsByCategory = products.GroupBy(product => product.Category);

        // Task 7 B Show all products under their category
        foreach (var categoryGroup in productsByCategory)
        {
            Console.WriteLine(categoryGroup.Key);

            foreach (var product in categoryGroup)
            {
                Console.WriteLine($" - {product.Name}");
            }

        }

        // Task 7 C Amount of products in each category
        foreach (var categoryGroup in productsByCategory)
        {
            Console.WriteLine($"{categoryGroup.Key}: {categoryGroup.Count()} products");
        }

        // Task 7 D Average price of products in each category
        foreach (var categoryGroup in productsByCategory)
        {
            var averagePriceInEachCategory = categoryGroup.Average(product => product.Price);
            Console.WriteLine($"{categoryGroup.Key}: {averagePriceInEachCategory} kr.");
        }

        // Task 7 E Most expensive product in each category
        foreach (var categoryGroup in productsByCategory)
        {
            var mostExpensiveProductInEachCategory = categoryGroup.OrderByDescending(product => product.Price).First();
            Console.WriteLine($"{categoryGroup.Key}: {mostExpensiveProductInEachCategory.Name} - {mostExpensiveProductInEachCategory.Price} kr.");
        }

        // Task 8 A Top 3 most expensive items
        var top3ExpensiveProducts = products
            .OrderByDescending(product => product.Price)
            .Take(3);
        
        // Task 8 B The 5 lowest priced products
        var top5CheapestProducts = products
            .OrderBy(product => product.Price)
            .Take(5);
        
        // Task 8 C Sort list after DESC price
        var sortedDescendingProducts = products
            .OrderByDescending(product => product.Price);
        
        // Task 8 D Skip the 3 most expensive and take the next 3
        var middleProducts = products
            .OrderByDescending(product => product.Price)
            .Skip(3)
            .Take(3);
        
        // Task 8 E
        var page1 = products
            .OrderByDescending(product => product.Price)
            .Take(3);

        var page2 = products
            .OrderByDescending(product => product.Price)
            .Skip(3)
            .Take(3);

        var page3 = products
            .OrderByDescending(product => product.Price)
            .Skip(6)
            .Take(3);
    }
}