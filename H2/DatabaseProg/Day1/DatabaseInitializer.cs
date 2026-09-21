using Microsoft.Data.Sqlite;
namespace Day1;

public static class DatabaseInitializer
{
    public static void CreateDatabase()
    {
        string dbPath = "workshop.db";
        string sqlPath = "CreateWorkshopDB.sql";

        string connectionString = $"Data Source={dbPath}";

        string sqlScript = File.ReadAllText(sqlPath);

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = sqlScript;
        command.ExecuteNonQuery();
    }
}