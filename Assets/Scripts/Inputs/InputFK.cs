using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class InputFK : MonoBehaviour
{
    [SerializeField] private string attribute;
    [NonSerialized] public TMP_Dropdown dropdown;
    [SerializeField] private bool useOnValueChanged = true;
    public int index = 0;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }

    private void Start() 
    {
        if (useOnValueChanged)
        {
            dropdown.onValueChanged.AddListener(OnValueChanged);
        }
    }

    public void Init(List<string> options)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        dropdown.value = 0;
    }

    public void Init(List<string> options, string currentPK)
    {
        int index = 0;
        dropdown.ClearOptions();
        dropdown.AddOptions(options);
        for (int i = 0; i < options.Count; i++)
        {
            if (options[i].StartsWith(currentPK))
            {
                index = i;
                break;
            }
        }
        dropdown.value = index;
    }

    public string GetPK()
    {
        return dropdown.options[dropdown.value].text.Substring(0, dropdown.options[dropdown.value].text.IndexOf(":"));
    }

    private void OnValueChanged(int optionIndex)
    {
        string PK = dropdown.options[optionIndex].text.Substring(0, dropdown.options[optionIndex].text.IndexOf(":"));
        SceneController.Instance.TryUpdateRow(attribute, PK, transform.parent.parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText);
    }
}
