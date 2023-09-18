using Microsoft.Data.SqlClient;
using System;
using System.Data;


public class SqlService
{
    private readonly string connectionString;
    private readonly SqlConnection[] connectionPool;
    private readonly object poolLock = new object();
    private const int MaxConnections = 10;

    public SqlService(string connectionString)
    {
        this.connectionString = connectionString;
        connectionPool = new SqlConnection[MaxConnections];
    }

    public SqlConnection GetConnection()
    {
        lock (poolLock)
        {
            for (int i = 0; i < MaxConnections; i++)
            {
                if (connectionPool[i] == null)
                {
                    connectionPool[i] = new SqlConnection(connectionString);
                    connectionPool[i].Open();
                    return connectionPool[i];
                }
                else if (connectionPool[i].State == ConnectionState.Closed)
                {
                    connectionPool[i].Open();
                    return connectionPool[i];
                }
            }
        }
        throw new ApplicationException("Connection pool exhausted.");
    }

    public void ReleaseConnection(SqlConnection connection)
    {
        // Release the connection back to the pool.
        if (connection != null)
        {
            lock (poolLock)
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        }
    }
}
