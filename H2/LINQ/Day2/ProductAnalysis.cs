namespace Day2;

public static class ProductAnalysis
{
    public static void GenerateReport(List<Product> products)
    {
        Console.WriteLine("PRODUKTANALYSE");
        Console.WriteLine($"Antal produkter: {products.Count()}");
        Console.WriteLine(
            $"Billigste produkt: {products.OrderBy(p => p.Price).First().Name} - {products.Min(p => p.Price)} kr.");
        Console.WriteLine(
            $"Dyreste produkt: {products.OrderByDescending(p => p.Price).First().Name} - {products.Max(p => p.Price)} kr.");
        Console.WriteLine($"Gennemsnitspris: {products.Average(p => p.Price)} kr.");
        Console.WriteLine($"Samlet produktværdi: {products.Sum(p => p.Price)} kr.");
        Console.WriteLine();

        Console.WriteLine("KATEGORIANALYSE");
        var groupedProducts = products.GroupBy(p => p.Category);

        foreach (var categoryGroup in groupedProducts)
        {
            var categoryName = categoryGroup.Key;
            var count = categoryGroup.Count();
            var averagePrice = categoryGroup.Average(p => p.Price);
            var cheapest = categoryGroup.OrderBy(p => p.Price).First();
            var mostExpensive = categoryGroup.OrderByDescending(p => p.Price).First();

            Console.WriteLine(categoryName);
            Console.WriteLine($"  Antal produkter: {count}");
            Console.WriteLine($"  Gennemsnitspris: {averagePrice} kr.");
            Console.WriteLine($"  Billigste produkt: {cheapest.Name} - {cheapest.Price} kr.");
            Console.WriteLine($"  Dyreste produkt: {mostExpensive.Name} - {mostExpensive.Price} kr.");
        }

        Console.WriteLine();

        Console.WriteLine("TOPPRODUKTER");
        Console.WriteLine("De 3 dyreste produkter:");
        var top3Expensive = products.OrderByDescending(p => p.Price).Take(3);
        foreach (var product in top3Expensive)
        {
            Console.WriteLine($"  {product.Name} - {product.Price} kr.");
        }

        Console.WriteLine("De 3 billigste produkter:");
        var top3Cheapest = products.OrderBy(p => p.Price).Take(3);
        foreach (var product in top3Cheapest)
        {
            Console.WriteLine($"  {product.Name} - {product.Price} kr.");
        }

        Console.WriteLine();

        Console.WriteLine("PRODUKTFILTRERING");
        Console.WriteLine("Alle produkter over 5.000 kr.:");
        var over5000 = products.Where(p => p.Price > 5000m);
        foreach (var product in over5000)
        {
            Console.WriteLine($"  {product.Name} - {product.Price} kr. - {product.Category}");
        }

        Console.WriteLine("Alle produkter i kategorien \"Computer\":");
        var computers = products.Where(p => p.Category == "Computer");
        foreach (var product in computers)
        {
            Console.WriteLine($"  {product.Name} - {product.Price} kr.");
        }

        Console.WriteLine("Alle produkter med \"Gaming\" i navnet:");
        var gamingProducts = products.Where(p => p.Name.Contains("Gaming"));
        foreach (var product in gamingProducts)
        {
            Console.WriteLine($"  {product.Name} - {product.Price} kr.");
        }

        Console.WriteLine();

        Console.WriteLine("TAGS");
        var allTags = products.SelectMany(p => p.Tags);
        var uniqueTags = allTags.Distinct();

        Console.WriteLine("Alle forskellige tags:");
        foreach (var tag in uniqueTags)
        {
            Console.WriteLine($"  {tag}");
        }

        Console.WriteLine($"Antallet af forskellige tags: {uniqueTags.Count()}");

        Console.WriteLine("Tags anvendt på produkter over 5.000 kr.:");
        var tagsFromExpensiveProducts = products
            .Where(p => p.Price > 5000m)
            .SelectMany(p => p.Tags)
            .Distinct();

        foreach (var tag in tagsFromExpensiveProducts)
        {
            Console.WriteLine($"  {tag}");
        }
    }
}