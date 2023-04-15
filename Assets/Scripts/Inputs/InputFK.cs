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
    public int index = 0;
    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
        dropdown.onValueChanged.AddListener(OnValueChanged);
    }

    public void Init(List<string> options, string currentPK)
    {
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

    private void OnValueChanged(int optionIndex)
    {
        string PK = dropdown.options[optionIndex].text.Substring(0, dropdown.options[optionIndex].text.IndexOf(":"));
        SceneController.Instance.TryUpdateRow(attribute, PK, transform.parent.parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText);
    }
}
