using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connectionString = Application.streamingAssetsPath + "/test.db";
        SQLiteConnection connection = new SQLiteConnection(connectionString);

        string insert = "INSERT INTO testnomer1(id, name) VALUES(14, 'n');";
        SQLiteCommand commandIn = new SQLiteCommand(connection);
        commandIn.CommandText = insert;
        commandIn.ExecuteNonQuery();

        string sqlQuery = "SELECT id, name FROM testnomer1";
        List<Test> rows = connection.Query<Test>(sqlQuery);

        foreach (var row in rows)
        {
            Debug.Log(row);
            Debug.Log("ID: " + row.id + ", Name: " + row.name);
        }
    }

    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
