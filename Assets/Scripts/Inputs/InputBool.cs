using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputBool : MonoBehaviour
{
    private Toggle toggle;
    public string attribute;
    public bool updateRowOnEdit;
    private void Awake()
    {
        toggle = GetComponent<Toggle>();
    }

    private void Start()
    {
        toggle.onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(bool value)
    {
        if(!SceneController.Instance.TryUpdateRow(
            attribute, 
            (value ? 1 : 0).ToString(), 
            transform.parent.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text))
        {
            toggle.isOn = !value;
            Debug.LogWarning($"{new FormatException().GetType()}: '{value}' is not a valid value for '{attribute}'");
        }
        else if (updateRowOnEdit)
        {
            SceneController.Instance.RepaintRow(transform.parent.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text, transform.parent.parent, 
            transform.GetChild(0).GetComponent<Image>().color.r < 0.98f);
        }
    }
}
