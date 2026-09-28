using GUI.Data;
using GUI.Models;
using MySqlConnector;

namespace GUI.Repositories;

public class DatabaseRepository
{
    public IEnumerable<TableInfo> GetTables()
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            const string sql = """
                SELECT TABLE_NAME AS TableName
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'day1'
                ORDER BY TABLE_NAME;
                """;

            var tables = new List<TableInfo>();

            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                tables.Add(new TableInfo
                {
                    TableName = reader.GetString("TableName")
                });
            }

            return tables;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while fetching tables: {ex.Message}");
            return Enumerable.Empty<TableInfo>();
        }
    }

    public IEnumerable<TableRow> GetTableData(string tableName)
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            string sql = $"SELECT * FROM day1.`{tableName}`;";

            var rows = new List<TableRow>();

            using var command = new MySqlCommand(sql, connection);
            using var reader = command.ExecuteReader();

            while (reader.Read())
            {
                var row = new TableRow();

                for (int i = 0; i < reader.FieldCount; i++)
                {
                    var columnName = reader.GetName(i);
                    var value = reader.IsDBNull(i) ? null : reader.GetValue(i);
                    row.Values[columnName] = value;
                }

                rows.Add(row);
            }

            return rows;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while fetching data from {tableName}: {ex.Message}");
            return Enumerable.Empty<TableRow>();
        }
    }
}