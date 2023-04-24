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

    private void Awake()
    {
        continueButton.onClick.AddListener(Continue);
        if(query == 0)
        {
            inputs[0].GetComponent<TMP_Dropdown>().ClearOptions();
            inputs[0].GetComponent<TMP_Dropdown>().AddOptions(SceneController.Instance.GetFKs("Role"));
        }
        else if(query == 4)
        {
            inputs[0].GetComponent<TMP_Dropdown>().ClearOptions();
            inputs[0].GetComponent<TMP_Dropdown>().AddOptions(SceneController.Instance.GetFKs("Category"));
        }
        else if (query == 7)
        {
            inputs[0].GetComponent<TMP_Dropdown>().ClearOptions();
            inputs[0].GetComponent<TMP_Dropdown>().AddOptions(SceneController.Instance.GetFKs("Seller"));
            inputs[1].GetComponent<Toggle>().interactable = false;
            inputs[2].GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            inputs[3].GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            if(!PersistentData.isManager)
            {
                inputs[0].GetComponent<TMP_Dropdown>().ClearOptions();
                inputs[0].GetComponent<TMP_Dropdown>().AddOptions(new List<string>{PersistentData.userString});
                inputs[0].GetComponent<TMP_Dropdown>().interactable = false;
            }
        }
    }

    private void Continue()
    {
        string[] inputValues = new string[inputs.Length];
        for (int i = 0; i < inputs.Length; i++)
        {
            if(inputs[i].GetComponent<TMP_InputField>() != null)
            {
                inputValues[i] = inputs[i].GetComponent<TMP_InputField>().text;
            }
            else if(inputs[i].GetComponent<Toggle>() != null)
            {
                inputValues[i] = inputs[i].GetComponent<Toggle>().isOn.ToString();
            }
            else if(inputs[i].GetComponent<TMP_Dropdown>() != null)
            {
                inputValues[i] = inputs[i].GetComponent<TMP_Dropdown>().options[inputs[i].GetComponent<TMP_Dropdown>().value].text.Substring(0, inputs[i].GetComponent<TMP_Dropdown>().options[inputs[i].GetComponent<TMP_Dropdown>().value].text.IndexOf(":"));
            }
            else if(inputs[i].GetComponent<DatePicker>() != null)
            {
                inputValues[i] = inputs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_InputField>().text;
            }
        }
        Debug.Log(GetWhereHaving(query, inputValues));
        SearchController.Instance.SetWhereHaving(GetWhereHaving(query, inputValues));
    }

    /*
- Працівник
0    - посада
1    - прізвище
- Клієнт
2    - відсоток
3(1)    - прізвище
- Товар
4    - категорія
~5   - UPC 
6    - назва
- Чек
7    - Касир + період
8    - номер
*/
    private string GetWhereHaving(int index, string[] inputs) => index switch
    {
        0 => $"WHERE role = '{inputs[0]}'",
        1 => $"WHERE last_name LIKE '%{inputs[0]}%'",
        2 => $"WHERE percent = '{((decimal.Parse(inputs[0].Replace('.', ',')) > 1 ? (decimal.Parse(inputs[0].Replace('.', ','))/100).ToString().Replace(',', '.') : inputs[0].Replace(',', '.')))}'",
        //3 => $"WHERE last_name LIKE '%{inputs[0]}%'",
        4 => $"WHERE category_number = '{inputs[0]}'",
        //5 => $"WHERE id LIKE '%{inputs[0]}%'",
        6 => $"WHERE product_name LIKE '%{inputs[0]}%'",
        7 => $"WHERE id_employee {((bool.Parse(inputs[1]) && PersistentData.isManager) ? ("LIKE '%%'") : ($"= '{inputs[0]}'"))} AND print_date BETWEEN '{inputs[2]}' AND '{inputs[3]}'",
        8 => $"WHERE check_number LIKE '%{inputs[0]}%' {(!PersistentData.isManager ? $"AND id_employee = '{PersistentData.userString}'" : "")}",
        _ => throw new System.NotImplementedException($"GetWhereHaving {index} not implemented")
    };
}
