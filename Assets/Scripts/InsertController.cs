using System.Collections;
using System.Collections.Generic;
using Entities;
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
        for(int i = 0; i < inputs.Length; i++)
        {
            if(SceneController.Instance.CellTypes()[i] == CellType.FKButton)
            {
                inputs[i].GetComponent<InputFK>().Init(SceneController.Instance.GetFKs(SceneController.Instance.FKEntities()[index]));
                index++;
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
        }
        if(SceneController.Instance.currentEntity == "Product")
        {
            values.Add("0");
        }

        if(SceneController.Instance.currentEntity == "Bill")
        {
            values = new List<string>() { inputs[0].GetComponent<TMPro.TMP_InputField>().text, 
            inputs[1].GetComponent<InputFK>().GetPK(),
            inputs[2].GetComponent<InputFK>().GetPK(),
            "0", PersistentData.chosenDate,
            "0","0"};
        }
        if (SceneController.Instance.TryAddRow(values))
        {
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
            Hide();
            TableFiller.Instance.DeleteAllChildren();
            SceneController.Instance.Load();
            OnHeaderSort.ResetSorting();
        }
    }
}
