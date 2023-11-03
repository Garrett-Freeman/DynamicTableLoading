namespace DapperService.Interfaces;

public interface IConnection
{
    Task<List<ColumnMetadata>> GetTableColumnMetadata(string conn, string schema, string tableName);
    Task<DynamicDatabase> GetDatabaseAsync(string conn);
    DynamicDatabase GetDatabase(string conn);
    Task<List<dynamic>> GetTableData(string conn, string schema, string tableName, List<ColumnMetadata> columns);
    Task<List<DynamicTable>> GetAllTables(string conn);
    Task<List<DynamicTable>> GetTablesBySchemaNameAsync(string conn, string schemaName);
    List<DynamicTable> GetTablesBySchemaName(string conn, string schemaName);
}