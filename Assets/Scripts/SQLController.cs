using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class SQLController : MonoBehaviour
{
    private SQLiteConnection connection;
    public static SQLController Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        string connectionString = Application.streamingAssetsPath + "/ZLAGODA.sqlite";
        connection = new SQLiteConnection(connectionString);
    }

    public bool TryExecuteNonQuery(string sqlQuery)
    {
        Debug.Log(sqlQuery);
        try
        {
            SQLiteCommand commandIn = new SQLiteCommand(connection);
            commandIn.CommandText = sqlQuery;
            commandIn.ExecuteNonQuery();  
            return true;                      
        }
        catch (SQLiteException e)
        {
            Debug.LogWarning($"{e.GetType()}: {e.Message}");
            return false;
        }
    }

    public List<T> ExecuteQuery<T>(string sqlQuery) where T : new()
    {
        Debug.Log(sqlQuery);
        return connection.Query<T>(sqlQuery);
    }
}
