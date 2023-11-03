using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Configuration;
using DapperService.Interfaces;
using System.Drawing;

namespace DapperService;

public class Connection : IConnection
{
    public readonly List<DynamicDatabase> Databases = new List<DynamicDatabase>();

    // Get database info on initialization
    public Connection InitConnection(List<string> connectionStrings)
    {
        foreach (var conn in connectionStrings)
        {
            DynamicDatabase db = GetDatabase(conn);

            foreach (DynamicSchema schema in db.Schemas)
            {
                schema.Tables = GetTablesBySchemaName(db.ConnectionString, schema.SchemaName);
            }

            Databases.Add(db);            
        }

        return this;
    }

    public async Task<List<DynamicTable>> GetAllTables(string conn)
    {
        var sql = $@"SELECT 
                        s.name AS SchemaName,
                        t.name AS TableName
                    FROM sys.tables t
                    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                    ORDER BY s.name, t.name;";

        
        using IDbConnection connection = new SqlConnection(conn);
        var rows = (await connection.QueryAsync<DynamicTable>(sql)).ToList();

        return rows;
    }

    public async Task<List<DynamicTable>> GetTablesBySchemaNameAsync(string conn, string schemaName)
    {
        var sql = $@"SELECT 
                        s.name AS SchemaName,
                        t.name AS TableName
                    FROM sys.tables t
                    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                    WHERE s.name = '{schemaName}'
                    ORDER BY s.name, t.name;";

        using IDbConnection connection = new SqlConnection(conn);
        var rows = (await connection.QueryAsync<DynamicTable>(sql)).ToList();

        return rows;
    }


    public List<DynamicTable> GetTablesBySchemaName(string conn, string schemaName)
    {
        var sql = $@"SELECT 
                        s.name AS SchemaName,
                        t.name AS TableName
                    FROM sys.tables t
                    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                    WHERE s.name = '{schemaName}'
                    ORDER BY s.name, t.name;";

        using IDbConnection connection = new SqlConnection(conn);
        var rows = (connection.Query<DynamicTable>(sql)).ToList();

        return rows;
    }

    // Change this to use a connection string name
    public async Task<DynamicDatabase> GetDatabaseAsync(string conn)
    {
        var db = new DynamicDatabase();        
        var sqlDbName = "SELECT DB_NAME() AS CurrentDatabaseName;";
        var sqlDbSchemas = "SELECT SchemaId, SchemaName FROM config.Schemas";

        // modify to use connection string by name and get from settings
        using IDbConnection connection = new SqlConnection(conn);
        db.Name = await connection.QuerySingleAsync<string>(sqlDbName);
        db.Schemas = (await connection.QueryAsync<DynamicSchema>(sqlDbSchemas)).ToList();
        db.ConnectionString = conn;

        return db;
    }

    // Change this to use a connection string name
    public DynamicDatabase GetDatabase(string conn)
    {
        var db = new DynamicDatabase();
        var sqlDbName = "SELECT DB_NAME() AS CurrentDatabaseName;";
        var sqlDbSchemas = "SELECT SchemaId, SchemaName FROM config.Schemas";

        // modify to use connection string by name and get from settings
        using IDbConnection connection = new SqlConnection(conn);
        db.Name = connection.QuerySingle<string>(sqlDbName);
        db.Schemas = (connection.Query<DynamicSchema>(sqlDbSchemas)).ToList();
        db.ConnectionString = conn;

        return db;
    }

    public async Task<List<ColumnMetadata>> GetTableColumnMetadata(string conn, string schema, string tableName)
    {
        var sql = $@"SELECT 
                        c.name AS ColumnName,
                        ty.name AS DataType,
                        c.max_length AS MaxLength,
                        c.is_nullable AS IsNullable
                    FROM sys.tables t
                    INNER JOIN sys.columns c ON t.object_id = c.object_id
                    INNER JOIN sys.types ty ON c.user_type_id = ty.user_type_id
                    WHERE t.name = '{tableName}' AND
	                    t.schema_id = (SELECT schema_id 
					                     FROM sys.schemas 
					                     WHERE name = '{schema}')
                    ORDER BY t.name, c.column_id;";

        using IDbConnection connection = new SqlConnection(conn);
        var rows = (await connection.QueryAsync<ColumnMetadata>(sql)).ToList();
        return rows;
    }

    public async Task<List<dynamic>> GetTableData(string conn, string schema, string tableName, List<ColumnMetadata> columns)
    {
        var sql = $@"SELECT *  
                     FROM {schema}.{tableName}";

        using IDbConnection connection = new SqlConnection(conn);
        var rows = (await connection.QueryAsync<dynamic>(sql)).ToList();
        var data = new List<object>();

        foreach (var row in rows)
        {
            var fields = row as IDictionary<string, object>;
            var rowData = new ExpandoObject() as IDictionary<string, object>;

            foreach (var col in columns)
            {
                var propertyName = col.ColumnName;
                var value = fields[col.ColumnName];

                rowData[propertyName] = value;
            }

            data.Add(rowData);
        }

        return data;
    }
}


