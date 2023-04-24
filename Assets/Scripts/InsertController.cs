using System.Collections;
using System.Collections.Generic;
using Entities;
using UI.Dates;
using UnityEngine;
using UnityEngine.UI;

public class InsertController : MonoBehaviour
{
    [SerializeField] private GameObject[] inputs;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button insertButton;
    public static InsertController Instance;

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
        insertButton.onClick.AddListener(TryInsert);
        int index = 0;
        if(SceneController.Instance.currentEntity == "Check_row")
        {
            inputs[2].GetComponent<InputFK>().Init(SceneController.Instance.GetFKs(SceneController.Instance.FKEntities()[1]));
        }
        else
        {
            for(int i = 0; i < inputs.Length; i++)
            {
                if(SceneController.Instance.CellTypes()[i] == CellType.FKButton)
                {
                    inputs[i].GetComponent<InputFK>().Init(SceneController.Instance.GetFKs(SceneController.Instance.FKEntities()[index]));
                    index++;
                }
                if(SceneController.Instance.CellTypes()[i] == CellType.Date)
                {
                    inputs[i].GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
                }
            }
        }
        if(OnInsertButton.Instance != null)
            OnInsertButton.Instance.GetComponent<Button>().onClick.AddListener(() => gameObject.SetActive(true));
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void TryInsert()
    {
        var values = new List<string>();
        var types = SceneController.Instance.CellTypes();
        try
        {
            for (int i = 0; i < inputs.Length; i++)
            {
                if(types[i] == CellType.FKButton)
                {
                    values.Add(inputs[i].GetComponent<InputFK>().GetPK());
                    continue;
                }
                if(types[i] == CellType.InputField)
                {
                    if(inputs[i].GetComponent<TMPro.TMP_InputField>().text.Length == 0)
                    {
                        values.Add("NULL");
                        continue;
                    }
                    {
                        var t = (inputs[i]).GetComponent<TMPro.TMP_InputField>().text;
                        if(decimal.TryParse(t.Replace('.',','), out decimal d))
                        {
                            if(d < 0)
                            {
                                ExceptionHandler.Instance.ShowMessage("Negative value", "Negative value is not allowed");
                                return;
                            }
                        }
                    }
                    if(inputs[i].GetComponent<TMPro.TMP_InputField>().contentType == TMPro.TMP_InputField.ContentType.DecimalNumber)
                    {
                        values.Add(inputs[i].GetComponent<TMPro.TMP_InputField>().text.Replace(',', '.'));
                        continue;
                    }
                    values.Add($"\'{inputs[i].GetComponent<TMPro.TMP_InputField>().text}\'");
                    continue;
                }
                if(types[i] == CellType.Toggle)
                {
                    values.Add(inputs[i].GetComponent<Toggle>().isOn ? "1" : "0");
                    continue;
                }
                if(types[i] == CellType.Date)
                {
                    values.Add($"\'{inputs[i].transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text}\'");
                    continue;
                }
            }
        }
        catch{}
        if(SceneController.Instance.currentEntity == "Product")
        {
            values.Add("0");
        }

        if(SceneController.Instance.currentEntity == "Bill")
        {
            values = new List<string>() { inputs[0].GetComponent<TMPro.TMP_InputField>().text, 
            inputs[1].GetComponent<InputFK>().GetPK(),
            inputs[2].GetComponent<InputFK>().GetPK(),
            "0", $"\"{System.DateTime.Now.ToString("yyyy-MM-dd")}\"",
            "0","0"};
        }
        if(SceneController.Instance.currentEntity == "Check_row")
        {
            values = new List<string>() { inputs[0].GetComponent<TMPro.TMP_InputField>().text, 
            inputs[1].GetComponent<TMPro.TMP_InputField>().text,
            "0",
            PersistentData.additionalData,
            inputs[2].GetComponent<InputFK>().GetPK(),
            "0"
            };
        }
        if(SceneController.Instance.currentEntity == "Employee")
        {
            int i = inputs.Length - 1;
            if(inputs[i].GetComponent<TMPro.TMP_InputField>().text.Length == 0)
            {
                values.Add("NULL");
                ExceptionHandler.Instance.ShowMessage("Invalid password","Password cannot be empty");
            }
            else
            {
                values.Add($"{inputs[i].GetComponent<TMPro.TMP_InputField>().text}");
            }
            values[values.Count - 1] = $"\"{InputPassword.Encrypt(values[values.Count - 1])}\"";
        }
        if(SceneController.Instance.currentEntity == "Customer_card")
        {
            decimal val = decimal.TryParse(values[values.Count - 1].Replace('.', ','), out val) ? val : 0;
            if(val < 0)
            {
                ExceptionHandler.Instance.ShowMessage("Invalid value", "Percent cannot be negative");
                return;
            }
            else if(val > 100)
            {
                ExceptionHandler.Instance.ShowMessage("Invalid value", "Percent cannot be greater than 100");
                return;
            }
            else if(val >=1)
            {
                val = val / 100;
            }
            values[values.Count - 1] = $"'{val.ToString().Replace(',', '.')}'";
        }
        if (SceneController.Instance.TryAddRow(values))
        {
            try{
                for (int i = 0; i < inputs.Length; i++)
                {
                    if (SceneController.Instance.CellTypes()[i] == CellType.InputField)
                    {
                        inputs[i].GetComponent<TMPro.TMP_InputField>().text = "";
                        continue;
                    }
                    if (SceneController.Instance.CellTypes()[i] == CellType.Toggle)
                    {
                        inputs[i].GetComponent<Toggle>().isOn = false;
                        continue;
                    }
                }
            }catch{}
            Hide();
            TableFiller.Instance.DeleteAllChildren();
            SceneController.Instance.Load();
            OnHeaderSort.ResetSorting();
        }
    }
}
