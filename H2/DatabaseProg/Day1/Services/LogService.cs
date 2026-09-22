namespace Day1.Services;

public static class LogService
{
    private static readonly string LogFilePath = Path.Combine(AppContext.BaseDirectory, "log.txt");

    public static void Log(
        string user,
        string operation,
        string tableName,
        int recordId,
        string? oldData,
        string? newData)
    {
        string logLine =
            $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} | User: {user} | Operation: {operation} | Table: {tableName} | RecordId: {recordId} | OldData: {oldData ?? "-"} | NewData: {newData ?? "-"}";

        File.AppendAllText(LogFilePath, logLine + Environment.NewLine);
    }
}