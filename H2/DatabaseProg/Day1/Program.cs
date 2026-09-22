using Day1;

if (!File.Exists("workshop.db"))
{
    DatabaseInitializer.CreateDatabase();
}