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

// Return an object of your choice for items over 5000
static class ProductExtensions
{
    public static IEnumerable<Product> ExpensiveProductsOwnClass(this IEnumerable<Product> products)
    {
        return products.Where(product => product.Price > 5000m);
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

        foreach (var product in products)
        {
            if (isExpensive(product))
            {
                Console.WriteLine($"Products that are more than 5000: {product.Name} - {product.Price}");
            }
        }




        // Lambda expression and the use of Where() to filter products that are more than 10000 or belong to department "Skærm"
        var expensiveProducts = products.Where(product => product.Price > 10000m || product.Department == "Skærm");

        foreach (var product in expensiveProducts)
        {
                Console.WriteLine($"Products that are more than 10000 or belong to department 'Skærm': {product.Name} - {product.Price}");
        }




        // Lambda expression to check if a product is between 1000 and 10000 and not in the department "Tilbehør" and descending price
        var isBetweenAndNotPartOff = products.Where(product =>
            product.Price > 1000m &&
            product.Price < 10000m &&
            product.Department != "Tilbehør")
            .OrderByDescending(product => product.Price);

        foreach (var product in isBetweenAndNotPartOff)
        {
            Console.WriteLine($"Products that are between 1000 and 10000 and not in the department 'Tilbehør': {product.Name} - {product.Price}");
        }

        // calling the extension method to get products that are more than 5000
        var expensiveProductsOwnClass = products.ExpensiveProductsOwnClass();

        foreach (var product in expensiveProductsOwnClass)
        {
            Console.WriteLine($"{product.Name} - {product.Price} - {product.Department}");
        }


        // Select() creates a new anonymous object for each product listed(anonymous type)
        var productInfo = products.Select(product => new
        {
            product.Name,
            product.Department,
            product.Price
        });

        foreach (var item in productInfo)
        {
            Console.WriteLine($"{item.Name} - {item.Department} - {item.Price}");
        }


        // Find all computers
        var computers = products.Where(product => product.Department == "Computer");

        // Find all products over 1000
        var productsOver1000 = products.Where(product => product.Price > 1000m);

        // Sort products by price
        var productsSortedByPrice = products.OrderBy(product => product.Price);

        // Find the most expensive product
        var mostExpensiveProduct = products.OrderByDescending(product => product.Price).First();

        // Count the number of products
        var productCount = products.Count();



        



    }
}

