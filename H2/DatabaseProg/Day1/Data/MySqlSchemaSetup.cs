using MySqlConnector;

namespace Day1.Data;

public static class MySqlSchemaSetup
{
    public static void CreateDatabaseAndTables()
    {
        using var connection = new MySqlConnection("Server=localhost;User ID=root;Password=Passw0rd;");
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = "CREATE DATABASE IF NOT EXISTS day1;";
        command.ExecuteNonQuery();

        command.CommandText = "USE day1;";
        command.ExecuteNonQuery();

        string sqlPath = Path.Combine(AppContext.BaseDirectory, "MySQLCreateDB.sql");
        string sqlScript = File.ReadAllText(sqlPath);

        foreach (var statement in sqlScript.Split(';'))
        {
            var trimmed = statement.Trim();
            if (string.IsNullOrWhiteSpace(trimmed))
                continue;

            using var sqlCommand = connection.CreateCommand();
            sqlCommand.CommandText = trimmed;
            sqlCommand.ExecuteNonQuery();
        }
    }
}