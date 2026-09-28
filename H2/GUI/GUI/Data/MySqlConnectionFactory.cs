using MySqlConnector;

namespace GUI.Data;

public static class MySqlConnectionFactory
{
    private static readonly string ConnectionString =
        "Server=localhost;Database=day1;User ID=root;Password=Passw0rd;";

    public static MySqlConnection CreateConnection()
    {
        return new MySqlConnection(ConnectionString);
    }
}