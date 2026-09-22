using Microsoft.Data.Sqlite;

namespace Day1.Services;

public static class TriggerSetup
{
    public static void CreateWorkOrderStatusTrigger()
    {
        string dbPath = Path.Combine(AppContext.BaseDirectory, "workshop.db");
        string connectionString = $"Data Source={dbPath}";

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string createTableSql = @"
CREATE TABLE IF NOT EXISTS WorkOrderAuditLogs (
    WorkOrderAuditLogId INTEGER PRIMARY KEY AUTOINCREMENT,
    WorkOrderId INTEGER NOT NULL,
    OldStatus TEXT,
    NewStatus TEXT,
    ChangedAt TEXT NOT NULL
);";

        string createTriggerSql = @"
CREATE TRIGGER IF NOT EXISTS trg_WorkOrders_StatusUpdate
AFTER UPDATE OF WorkStatus ON WorkOrders
FOR EACH ROW
WHEN OLD.WorkStatus <> NEW.WorkStatus
BEGIN
    INSERT INTO WorkOrderAuditLogs (WorkOrderId, OldStatus, NewStatus, ChangedAt)
    VALUES (NEW.WorkOrderId, OLD.WorkStatus, NEW.WorkStatus, datetime('now'));
END;";

        using var createTableCommand = connection.CreateCommand();
        createTableCommand.CommandText = createTableSql;
        createTableCommand.ExecuteNonQuery();

        using var createTriggerCommand = connection.CreateCommand();
        createTriggerCommand.CommandText = createTriggerSql;
        createTriggerCommand.ExecuteNonQuery();
    }
}