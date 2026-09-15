namespace Day2;
public static class ProductData
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product("Gaming Laptop", "Computer", 12500m),
            new Product("Office Laptop", "Computer", 7500m),
            new Product("Gaming Mus", "Tilbehør", 650m),
            new Product("Keyboard", "Tilbehør", 1100m),
            new Product("4K Skærm", "Skærm", 4500m),
            new Product("Gaming Headset", "Tilbehør", 1500m),
            new Product("27\" Gaming Skærm", "Skærm", 3500m),
            new Product("USB-C Dock", "Tilbehør", 1800m),
            new Product("MacBook Air", "Computer", 9500m),
            new Product("Gaming PC", "Computer", 15000m),
            new Product("Webkamera", "Tilbehør", 850m),
            new Product("32\" 4K Skærm", "Skærm", 5500m)
        };
    }
}