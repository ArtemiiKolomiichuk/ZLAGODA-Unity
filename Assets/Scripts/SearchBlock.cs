using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UI.Dates;

public class SearchBlock : MonoBehaviour
{
    [SerializeField] private GameObject[] inputs;
    [SerializeField] private Button continueButton;
    [SerializeField] private int query;

    private void Start()
    {
        continueButton.onClick.AddListener(Continue);
    }

    private void Continue()
    {
        string[] inputValues = new string[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            if(inputs[i].GetComponent<InputField>() != null)
            {
                inputValues[i] = inputs[i].GetComponent<TMP_InputField>().text;
            }
            else if(inputs[i].GetComponent<Dropdown>() != null)
            {
                inputValues[i] = inputs[i].GetComponent<Dropdown>().options[inputs[i].GetComponent<Dropdown>().value].text;
            }
            else if(inputs[i].GetComponent<DatePicker>() != null)
            {
                inputValues[i] = inputs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text;
            }
        }
        SearchController.Instance.SetWhereHaving(GetWhereHaving(query, inputValues));
    }

    /*
- Працівник
0    - FKпосада
1    - прізвище
- Клієнт
2    - відсоток
3    - прізвище
- Товар
4    - категорія
~5    - UPC 
6    - назва
- Чек
7    - Касир + період
8    - номер
*/
    private string GetWhereHaving(int index, string[] inputs) => index switch
    {
        0 => $"WHERE role = '{inputs[0]}'",
        1 => $"WHERE surname LIKE '%{inputs[0]}%'",
        2 => $"WHERE percent = '%{(decimal.Parse(inputs[0].Replace('.', ',')) > 1 ? (decimal.Parse(inputs[0].Replace('.', ','))/100) : inputs[0])}%'",
        3 => $"WHERE surname LIKE '%{inputs[0]}%'",
        4 => $"WHERE category = '{inputs[0]}'",
        //5 => $"WHERE id LIKE '%{inputs[0]}%'",
        6 => $"WHERE name LIKE '%{inputs[0]}%'",
        7 => $"WHERE id_employee = '{inputs[0]}' AND print_date BETWEEN '{inputs[1]}' AND '{inputs[2]}'",
        8 => $"WHERE check_number LIKE '%{inputs[0]}%'",
        _ => throw new System.NotImplementedException($"GetWhereHaving {index} not implemented")
    };
    
}
