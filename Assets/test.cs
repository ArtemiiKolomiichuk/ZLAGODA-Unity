using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System.Data;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string connectionString = Application.streamingAssetsPath + "/test.db";
        SQLiteConnection connection = new SQLiteConnection(connectionString);

        string insert = "INSERT INTO testnomer1(id, name) VALUES(17, 'max');";
        SQLiteCommand commandIn = new SQLiteCommand(connection);
        commandIn.CommandText = insert;
        commandIn.ExecuteNonQuery();

        string sqlQuery = "SELECT * FROM testnomer1";
        List<Test> tests = connection.Query<Test>(sqlQuery);

        foreach (Test test in tests)
        {
            Debug.Log("ID: " + test.id + ", Name: " + test.name);
        }
    }

    public class Test
    {
        public int id { get; set; }
        public string name { get; set; }
    }

}
