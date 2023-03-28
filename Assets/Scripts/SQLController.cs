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

    public void ExecuteNonQuery(string sqlQuery)
    {
        Debug.Log(sqlQuery);
        SQLiteCommand commandIn = new SQLiteCommand(connection);
        commandIn.CommandText = sqlQuery;
        commandIn.ExecuteNonQuery();
    }

    public List<T> ExecuteQuery<T>(string sqlQuery) where T : new()
    {
        return connection.Query<T>(sqlQuery);
    }
}
