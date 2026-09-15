namespace Day2;

public static class ProductData
{
    public static List<Product> GetProducts()
    {
        return new List<Product>
        {
            new Product("Gaming Laptop", "Computer", 12500m,
                new List<string> { "Gaming", "Laptop", "High Performance" }),
            new Product("Office Laptop", "Computer", 7500m, new List<string> { "Laptop", "Office", "Work" }),
            new Product("Gaming Mus", "Tilbehør", 650m, new List<string> { "Gaming", "Mus", "RGB" }),
            new Product("Keyboard", "Tilbehør", 1100m, new List<string> { "Keyboard", "Office", "Gaming" }),
            new Product("4K Skærm", "Skærm", 4500m, new List<string> { "Skærm", "4K", "Work" }),
            new Product("Gaming Headset", "Tilbehør", 1500m, new List<string> { "Gaming", "Headset", "Audio" }),
            new Product("27\" Gaming Skærm", "Skærm", 3500m, new List<string> { "Gaming", "Skærm", "27 inch" }),
            new Product("USB-C Dock", "Tilbehør", 1800m, new List<string> { "USB-C", "Dock", "Work" }),
            new Product("MacBook Air", "Computer", 9500m, new List<string> { "Laptop", "Apple", "Work" }),
            new Product("Gaming PC", "Computer", 15000m, new List<string> { "Gaming", "PC", "High Performance" }),
            new Product("Webkamera", "Tilbehør", 850m, new List<string> { "Webkamera", "Camera", "Work" }),
            new Product("32\" 4K Skærm", "Skærm", 5500m, new List<string> { "Skærm", "4K", "32 inch" })
        };
    }
}
