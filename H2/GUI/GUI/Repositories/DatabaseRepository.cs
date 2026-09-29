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

    public TableRow? GetRowById(string tableName, string idColumn, object idValue)
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            string sql = $"SELECT * FROM day1.`{tableName}` WHERE `{idColumn}` = @IdValue LIMIT 1;";

            using var command = new MySqlCommand(sql, connection);
            command.Parameters.AddWithValue("@IdValue", idValue);

            using var reader = command.ExecuteReader();

            if (!reader.Read())
                return null;

            var row = new TableRow();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                row.Values[reader.GetName(i)] = reader.IsDBNull(i) ? null : reader.GetValue(i);
            }

            return row;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while fetching row from {tableName}: {ex.Message}");
            return null;
        }
    }

    public void InsertRow(string tableName, TableRow row)
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            string sql;
            var command = new MySqlCommand();
            command.Connection = connection;

            switch (tableName)
            {
                case "customers":
                    sql = """
                        INSERT INTO day1.customers (CustomerName, Phone, Email)
                        VALUES (@CustomerName, @Phone, @Email);
                        """;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@CustomerName", row.Values["CustomerName"]);
                    command.Parameters.AddWithValue("@Phone", row.Values["Phone"]);
                    command.Parameters.AddWithValue("@Email", row.Values["Email"] ?? DBNull.Value);
                    break;

                case "cars":
                    sql = """
                        INSERT INTO day1.cars (CustomerId, LicensePlate, Brand, Model, ManufactureYear)
                        VALUES (@CustomerId, @LicensePlate, @Brand, @Model, @ManufactureYear);
                        """;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@CustomerId", row.Values["CustomerId"]);
                    command.Parameters.AddWithValue("@LicensePlate", row.Values["LicensePlate"]);
                    command.Parameters.AddWithValue("@Brand", row.Values["Brand"]);
                    command.Parameters.AddWithValue("@Model", row.Values["Model"]);
                    command.Parameters.AddWithValue("@ManufactureYear", row.Values["ManufactureYear"]);
                    break;

                case "workorders":
                    sql = """
                        INSERT INTO day1.workorders (CarId, StartDate, EndDate, WorkDescription, WorkStatus)
                        VALUES (@CarId, @StartDate, @EndDate, @WorkDescription, @WorkStatus);
                        """;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@CarId", row.Values["CarId"]);
                    command.Parameters.AddWithValue("@StartDate", row.Values["StartDate"]);
                    command.Parameters.AddWithValue("@EndDate", row.Values["EndDate"] ?? DBNull.Value);
                    command.Parameters.AddWithValue("@WorkDescription", row.Values["WorkDescription"]);
                    command.Parameters.AddWithValue("@WorkStatus", row.Values["WorkStatus"]);
                    break;

                case "parts":
                    sql = """
                        INSERT INTO day1.parts (PartName, Price)
                        VALUES (@PartName, @Price);
                        """;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@PartName", row.Values["PartName"]);
                    command.Parameters.AddWithValue("@Price", row.Values["Price"]);
                    break;

                case "workorderparts":
                    sql = """
                        INSERT INTO day1.workorderparts (WorkOrderId, PartId, Quantity)
                        VALUES (@WorkOrderId, @PartId, @Quantity);
                        """;
                    command.CommandText = sql;
                    command.Parameters.AddWithValue("@WorkOrderId", row.Values["WorkOrderId"]);
                    command.Parameters.AddWithValue("@PartId", row.Values["PartId"]);
                    command.Parameters.AddWithValue("@Quantity", row.Values["Quantity"]);
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported table: {tableName}");
            }

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while inserting into {tableName}: {ex.Message}");
        }
    }

    public void UpdateRow(string tableName, TableRow row)
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            var command = new MySqlCommand();
            command.Connection = connection;

            switch (tableName)
            {
                case "customers":
                    command.CommandText = """
                        UPDATE day1.customers
                        SET CustomerName = @CustomerName,
                            Phone = @Phone,
                            Email = @Email
                        WHERE CustomerId = @CustomerId;
                        """;
                    command.Parameters.AddWithValue("@CustomerId", row.Values["CustomerId"]);
                    command.Parameters.AddWithValue("@CustomerName", row.Values["CustomerName"]);
                    command.Parameters.AddWithValue("@Phone", row.Values["Phone"]);
                    command.Parameters.AddWithValue("@Email", row.Values["Email"] ?? DBNull.Value);
                    break;

                case "cars":
                    command.CommandText = """
                        UPDATE day1.cars
                        SET CustomerId = @CustomerId,
                            LicensePlate = @LicensePlate,
                            Brand = @Brand,
                            Model = @Model,
                            ManufactureYear = @ManufactureYear
                        WHERE CarId = @CarId;
                        """;
                    command.Parameters.AddWithValue("@CarId", row.Values["CarId"]);
                    command.Parameters.AddWithValue("@CustomerId", row.Values["CustomerId"]);
                    command.Parameters.AddWithValue("@LicensePlate", row.Values["LicensePlate"]);
                    command.Parameters.AddWithValue("@Brand", row.Values["Brand"]);
                    command.Parameters.AddWithValue("@Model", row.Values["Model"]);
                    command.Parameters.AddWithValue("@ManufactureYear", row.Values["ManufactureYear"]);
                    break;

                case "workorders":
                    command.CommandText = """
                        UPDATE day1.workorders
                        SET CarId = @CarId,
                            StartDate = @StartDate,
                            EndDate = @EndDate,
                            WorkDescription = @WorkDescription,
                            WorkStatus = @WorkStatus
                        WHERE WorkOrderId = @WorkOrderId;
                        """;
                    command.Parameters.AddWithValue("@WorkOrderId", row.Values["WorkOrderId"]);
                    command.Parameters.AddWithValue("@CarId", row.Values["CarId"]);
                    command.Parameters.AddWithValue("@StartDate", row.Values["StartDate"]);
                    command.Parameters.AddWithValue("@EndDate", row.Values["EndDate"] ?? DBNull.Value);
                    command.Parameters.AddWithValue("@WorkDescription", row.Values["WorkDescription"]);
                    command.Parameters.AddWithValue("@WorkStatus", row.Values["WorkStatus"]);
                    break;

                case "parts":
                    command.CommandText = """
                        UPDATE day1.parts
                        SET PartName = @PartName,
                            Price = @Price
                        WHERE PartId = @PartId;
                        """;
                    command.Parameters.AddWithValue("@PartId", row.Values["PartId"]);
                    command.Parameters.AddWithValue("@PartName", row.Values["PartName"]);
                    command.Parameters.AddWithValue("@Price", row.Values["Price"]);
                    break;

                case "workorderparts":
                    command.CommandText = """
                        UPDATE day1.workorderparts
                        SET Quantity = @Quantity
                        WHERE WorkOrderId = @WorkOrderId AND PartId = @PartId;
                        """;
                    command.Parameters.AddWithValue("@WorkOrderId", row.Values["WorkOrderId"]);
                    command.Parameters.AddWithValue("@PartId", row.Values["PartId"]);
                    command.Parameters.AddWithValue("@Quantity", row.Values["Quantity"]);
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported table: {tableName}");
            }

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while updating {tableName}: {ex.Message}");
        }
    }

    public void DeleteRow(string tableName, Dictionary<string, object?> keyValues)
    {
        try
        {
            using var connection = MySqlConnectionFactory.CreateConnection();
            connection.Open();

            var command = new MySqlCommand();
            command.Connection = connection;

            switch (tableName)
            {
                case "customers":
                    command.CommandText = "DELETE FROM day1.customers WHERE CustomerId = @CustomerId;";
                    command.Parameters.AddWithValue("@CustomerId", keyValues["CustomerId"]!);
                    break;

                case "cars":
                    command.CommandText = "DELETE FROM day1.cars WHERE CarId = @CarId;";
                    command.Parameters.AddWithValue("@CarId", keyValues["CarId"]!);
                    break;

                case "workorders":
                    command.CommandText = "DELETE FROM day1.workorders WHERE WorkOrderId = @WorkOrderId;";
                    command.Parameters.AddWithValue("@WorkOrderId", keyValues["WorkOrderId"]!);
                    break;

                case "parts":
                    command.CommandText = "DELETE FROM day1.parts WHERE PartId = @PartId;";
                    command.Parameters.AddWithValue("@PartId", keyValues["PartId"]!);
                    break;

                case "workorderparts":
                    command.CommandText = """
                        DELETE FROM day1.workorderparts
                        WHERE WorkOrderId = @WorkOrderId AND PartId = @PartId;
                        """;
                    command.Parameters.AddWithValue("@WorkOrderId", keyValues["WorkOrderId"]!);
                    command.Parameters.AddWithValue("@PartId", keyValues["PartId"]!);
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported table: {tableName}");
            }

            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deleting from {tableName}: {ex.Message}");
        }
    }
}