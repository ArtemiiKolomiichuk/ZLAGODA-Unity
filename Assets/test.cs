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
        /*
        string insert = @"
        INSERT INTO testnomer1(id, name) 
        VALUES(2122, '7878');";
        SQLiteCommand commandIn = new SQLiteCommand(connection);
        commandIn.CommandText = insert;
        commandIn.ExecuteNonQuery();
        */
        string sqlQuery = @"
        SELECT * 
        FROM testnomer1";
        List<Test> rows = connection.Query<Test>(sqlQuery);

        

        List<List<string>> data = new List<List<string>>();
        foreach (var row in rows)
        {
            data.Add(row.ToList());
        }
        TableFiller.Instance.FillTable(data);
    }



    public class Test
    {
        public int id { get; set; }
        public int name { get; set; }
        public List<string> ToList()
        {
            return new List<string> { id.ToString(), name.ToString() };
        }
    }

}
