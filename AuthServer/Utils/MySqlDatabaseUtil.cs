using MySql.Data.MySqlClient;

namespace AuthServer.Utils;

public class MySqlDatabaseUtil : IDatabaseUtil
{
    private readonly string _connectionString;
    private const int CommandTimeout = 1000;

    public MySqlDatabaseUtil(string connectionString)
    {
        _connectionString = connectionString;
        Console.WriteLine("CONN_STR" + connectionString);
    }
    
    public List<Dictionary<string, object>> PerformQuery(string query)
    {
        using var conn = new MySqlConnection();
        conn.ConnectionString = _connectionString;

        var cmd = new MySqlCommand(query, conn);
        cmd.CommandTimeout = CommandTimeout;
        cmd.Connection.Open();
        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        var queryResults = new List<Dictionary<string, object>>();
        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

        while(reader.Read())
        {
            var row = new Dictionary<string, object>();

            for(var i = 0; i < columns.Count; i++) 
            {
                row.Add(columns[i], reader.GetValue(i));
            }

            queryResults.Add(row);
        }

        return queryResults;
    }
    
    public List<Dictionary<string, object>> PerformQuery(string query, Dictionary<string, object> bindVars)
    {
        using var conn = new MySqlConnection();
        conn.ConnectionString = _connectionString;

        var cmd = new MySqlCommand(query, conn);
        cmd.CommandTimeout = CommandTimeout;

        foreach(var item in bindVars)
        {
            cmd.Parameters.Add(new MySqlParameter(item.Key, item.Value));
        }

        cmd.Connection.Open();
        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

        var queryResults = new List<Dictionary<string, object>>();
        List<string> columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();

        while(reader.Read())
        {
            var row = new Dictionary<string, object>();
            for(var i = 0; i < columns.Count; i++)
            {
                row.Add(columns[i], reader.GetValue(i));
            }

            queryResults.Add(row);
        }

        return queryResults;
    }
    
    public int PerformNonQuery(string sql, Dictionary<string, object> bindVars)
    {
        using var conn = new MySqlConnection();
        conn.ConnectionString = _connectionString;

        var cmd = new MySqlCommand(sql, conn);
        foreach(var item in bindVars)
        {
            cmd.Parameters.Add(new MySqlParameter(item.Key, item.Value));
        }

        cmd.Connection.Open();
        return cmd.ExecuteNonQuery();
    }
    
    public int PerformNonQueries(List<KeyValuePair<string, Dictionary<string, object>>> commands)
    {
        using var conn = new MySqlConnection();
        var numUpdates = 0;
        conn.ConnectionString = _connectionString;
        conn.Open();

        foreach(var (sql, value) in commands)
        {
            var cmd = new MySqlCommand(sql, conn);

            foreach(var bindVar in value)
            {
                cmd.Parameters.Add(new MySqlParameter(bindVar.Key, bindVar.Value));
            }

            numUpdates += cmd.ExecuteNonQuery();
        }

        return numUpdates;
    }
    
}