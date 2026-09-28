using Dapper;
using GUI.Data;
using GUI.Models;

namespace GUI.Repositories;

public class DatabaseRepository
{
    public IEnumerable<TableInfo> GetTables()
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();

            string sql = """
                SELECT TABLE_NAME AS TableName
                FROM INFORMATION_SCHEMA.TABLES
                WHERE TABLE_SCHEMA = 'day1'
                ORDER BY TABLE_NAME;
                """;

            return connection.Query<TableInfo>(sql);
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

            string sql = $"SELECT * FROM `{tableName}`;";
            var rows = connection.Query(sql);

            var result = new List<TableRow>();

            foreach (var row in rows)
            {
                var dict = (IDictionary<string, object>)row;
                var tableRow = new TableRow();

                foreach (var kvp in dict)
                {
                    tableRow.Values[kvp.Key] = kvp.Value;
                }

                result.Add(tableRow);
            }

            return result;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while fetching table data: {ex.Message}");
            return Enumerable.Empty<TableRow>();
        }
    }
}