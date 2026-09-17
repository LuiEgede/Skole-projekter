using Day3;
using Spectre.Console;

// Using disposes the database context automatically when it is no longer needed, istead of having to call Dispose() manually
using var db = new AppDbContext();

if (!db.Products.Any())
{
    db.Products.AddRange(
        new Product { Name = "Gaming Laptop", Category = "Computer", Price = 12500m },
        new Product { Name = "Office Laptop", Category = "Computer", Price = 7500m },
        new Product { Name = "Gaming Mus", Category = "Tilbehør", Price = 650m },
        new Product { Name = "Keyboard", Category = "Tilbehør", Price = 1100m },
        new Product { Name = "4K Skærm", Category = "Skærm", Price = 4500m },
        new Product { Name = "Gaming Headset", Category = "Tilbehør", Price = 1500m },
        new Product { Name = "27\" Gaming Skærm", Category = "Skærm", Price = 3500m },
        new Product { Name = "USB-C Dock", Category = "Tilbehør", Price = 1800m },
        new Product { Name = "MacBook Air", Category = "Computer", Price = 9500m },
        new Product { Name = "Gaming PC", Category = "Computer", Price = 15000m },
        new Product { Name = "Webkamera", Category = "Tilbehør", Price = 850m },
        new Product { Name = "32\" 4K Skærm", Category = "Skærm", Price = 5500m }
    );

    db.SaveChanges();
}

var products = db.Products;

foreach (var product in products)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 1 A Find all the products in category "computer"
// IQueryable is used to build the query, but its not executed until we iterate over the results
var computers = db.Products
    .Where(product => product.Category == "Computer");

foreach (var product in computers)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 1 B Find all products that cost more than 5000
var expensiveProducts = db.Products
    .Where(product => product.Price > 5000m);

foreach (var product in expensiveProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 1 C Find all products that cost between 1000-5000
var midRangeProducts = db.Products
    .Where(product => product.Price >= 1000m && product.Price <= 5000m);

foreach (var product in midRangeProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 1 D Find all products in category "Tilbehør" that cost more than 1000
var accessoriesOver1000 = db.Products
    .Where(product => product.Category == "Tilbehør" && product.Price > 1000m);

foreach (var product in accessoriesOver1000)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 1 E Find all products that have "Gaming" in the name
var gamingProducts = db.Products
    .Where(product => product.Name.Contains("Gaming"));

foreach (var product in gamingProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}


// Task 2 CREATE new product to the db
var gamingKeyboard = new Product
{
    Name = "Gaming Keyboard",
    Category = "Tilbehør",
    Price = 1200m
};

db.Products.Add(gamingKeyboard);
db.SaveChanges();

// Task 2 READ all products from the db
var allProducts = db.Products;

foreach (var product in allProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}

// Task 2 UPDATE the price... FirstOrDefault = will throw an exception if the query does not return zero or one items.
var keyboardToUpdate = db.Products.FirstOrDefault(product => product.Name == "Gaming Keyboard");

if (keyboardToUpdate != null)
{
    keyboardToUpdate.Price = 1350m;
    db.SaveChanges();
}

// Task 2 DELETE the product
var keyboardToDelete = db.Products.FirstOrDefault(product => product.Name == "Gaming Keyboard");

if (keyboardToDelete != null)
{
    db.Products.Remove(keyboardToDelete);
    db.SaveChanges();
}

var remainingProducts = db.Products;

foreach (var product in remainingProducts)
{
    Console.WriteLine($"{product.Name} - {product.Category} - {product.Price} kr.");
}


// Task 3 A Add new column with migration

// dotnet ef migrations add AddUnitsSoldToProduct
// dotnet ef database update



//Task 3 B Update existing products with UnitsSold values
// FirstOrDefault = will throw an exception if the query does not return zero or one items.
var gamingLaptop = db.Products.FirstOrDefault(p => p.Name == "Gaming Laptop");
if (gamingLaptop != null) gamingLaptop.UnitsSold = 12;

var officeLaptop = db.Products.FirstOrDefault(p => p.Name == "Office Laptop");
if (officeLaptop != null) officeLaptop.UnitsSold = 8;

var gamingMus = db.Products.FirstOrDefault(p => p.Name == "Gaming Mus");
if (gamingMus != null) gamingMus.UnitsSold = 25;

var keyboard = db.Products.FirstOrDefault(p => p.Name == "Keyboard");
if (keyboard != null) keyboard.UnitsSold = 18;

var screen = db.Products.FirstOrDefault(p => p.Name == "4K Skærm");
if (screen != null) screen.UnitsSold = 10;

var gamingHeadset = db.Products.FirstOrDefault(p => p.Name == "Gaming Headset");
if (gamingHeadset != null) gamingHeadset.UnitsSold = 14;

var gamingScreen = db.Products.FirstOrDefault(p => p.Name == "27\" Gaming Skærm");
if (gamingScreen != null) gamingScreen.UnitsSold = 9;

var usbDock = db.Products.FirstOrDefault(p => p.Name == "USB-C Dock");
if (usbDock != null) usbDock.UnitsSold = 7;

var macBookAir = db.Products.FirstOrDefault(p => p.Name == "MacBook Air");
if (macBookAir != null) macBookAir.UnitsSold = 11;

var gamingPc = db.Products.FirstOrDefault(p => p.Name == "Gaming PC");
if (gamingPc != null) gamingPc.UnitsSold = 6;

var webkamera = db.Products.FirstOrDefault(p => p.Name == "Webkamera");
if (webkamera != null) webkamera.UnitsSold = 20;

var screen32 = db.Products.FirstOrDefault(p => p.Name == "32\" 4K Skærm");
if (screen32 != null) screen32.UnitsSold = 5;

db.SaveChanges();

// Task 3 C group products by category and total unit sold for each category
var unitsSoldByCategory = db.Products
    .GroupBy(product => product.Category)
    .Select(group => new
    {
        Category = group.Key,
        TotalUnitsSold = group.Sum(product => product.UnitsSold)
    });

foreach (var item in unitsSoldByCategory)
{
    Console.WriteLine($"{item.Category}: {item.TotalUnitsSold} units sold");
}

// Task 3 D Display the total units sold by category (I used Spectre)
var chart = new BarChart()
    .Width(60)
    .Label("[green bold]Units sold by category[/]")
    .CenterLabel();

foreach (var item in unitsSoldByCategory)
{
    chart.AddItem(item.Category, item.TotalUnitsSold, Color.Blue);
}

AnsiConsole.Write(chart);


