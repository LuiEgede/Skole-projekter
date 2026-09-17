using System.Xml.Linq;
using day4;

static void PrintProducts()
{
    var doc = XDocument.Load("Product.xml");

    var products = doc
        .Descendants("Product")
        .Select(product => new Product(
            product.Element("Name")?.Value ?? string.Empty,
            product.Element("Category")?.Value ?? string.Empty,
            decimal.Parse(product.Element("Price")?.Value ?? "0")))
        .ToList();

    foreach (var product in products)
    {
        Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
    }

    Console.WriteLine();
}

var doc = XDocument.Load("Product.xml");

// Maps the XML data to a list of product
// .Descendants finds all the elements in the XML file
var products = doc
    .Descendants("Product")
    .Select(product => new Product(
        product.Element("Name")?.Value ?? string.Empty,
        product.Element("Category")?.Value ?? string.Empty,
        decimal.Parse(product.Element("Price")?.Value ?? "0")))
    .ToList();


// Task 1 A Find all products in the category "Computer"
var computers = products.Where(product => product.Category == "Computer");

foreach (var product in computers)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

Console.WriteLine();

// Task 1 B Find all products that cost more than 5000
var expensiveProducts = products.Where(product => product.Price > 5000m);

foreach (var product in expensiveProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

Console.WriteLine();

// Task 1 C Find all products that cost between 1000 and 5000
var midRangeProducts = products.Where(product => product.Price >= 1000m && product.Price <= 5000m);

foreach (var product in midRangeProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

Console.WriteLine();

// Task 1 D Find all products in the category "Tilbehør" that cost more than 1000
var accessoriesOver1000 = products.Where(product => product.Category == "Tilbehør" && product.Price > 1000m);

foreach (var product in accessoriesOver1000)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

Console.WriteLine();

// Task 1 E Find all products, whose name contains "Gaming"
var gamingProducts = products.Where(product => product.Name.Contains("Gaming"));

foreach (var product in gamingProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

Console.WriteLine();

// Task 2 A Add new product under the category "Tilbehør"
var newProduct = new XElement("Product",
    new XElement("Name", "Gaming Mousepad"),
    new XElement("Category", "Tilbehør"),
    new XElement("Price", 299));

doc.Root?.Add(newProduct);
doc.Save("Product.xml");

Console.WriteLine("After create:");
PrintProducts();

// Task 2 B Update the price of the newly added product
var productToUpdate = doc.Descendants("Product")
    .FirstOrDefault(product => product.Element("Name")?.Value == "Gaming Mousepad");

if (productToUpdate != null)
{
    var oldPrice = productToUpdate.Element("Price")?.Value ?? "0";
    productToUpdate.Element("Price")!.Value = "349";
    doc.Save("Product.xml");
}

PrintProducts();

// Task 2 C Delete the new product from XML
var productToDelete = doc.Descendants("Product")
    .FirstOrDefault(product => product.Element("Name")?.Value == "Gaming Mousepad");

if (productToDelete != null)
{
    productToDelete.Remove();
    doc.Save("Product.xml");
}

PrintProducts();