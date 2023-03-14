using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;


public class TestInputName : MonoBehaviour
{
    private TMPro.TMP_InputField inputField;
    private string oldText;

    private void Awake()
    {
        inputField = GetComponent<TMPro.TMP_InputField>();
    }
    private void Start()
    {
        inputField.onSelect.AddListener(OnSelect);
        inputField.onDeselect.AddListener(OnDeselect);
    }

    private void OnSelect(string newText)
    {
        oldText = newText;
    }
    private void OnDeselect(string newText)
    {
        if (newText != oldText)
        {
            //empty
            if(newText == "")
            {
                inputField.text = oldText;
            }
            else
            {
                if(int.TryParse(newText, out int name))
                {
                    string connectionString = Application.streamingAssetsPath + "/test.db";
                    SQLiteConnection connection = new SQLiteConnection(connectionString);
                    int id = int.Parse(transform.parent.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text);
                    string insert = @$"
                    UPDATE testnomer1
                    SET name = {name}
                    WHERE id = {id};";
                    SQLiteCommand commandIn = new SQLiteCommand(connection);
                    commandIn.CommandText = insert;
                    commandIn.ExecuteNonQuery();
                }
                else //not a number
                {
                    inputField.text = oldText;
                }
            }
        }
    }
}
