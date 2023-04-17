using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI.Dates;
using System;

public class InputDate : MonoBehaviour
{
    private DatePicker _datePicker;
    public string attribute;

    private void Awake()
    {
        _datePicker = GetComponent<DatePicker>();
    }

    private void Start()
    {
        _datePicker.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().onValueChanged.AddListener(OnValueChanged);
    }

    private void OnValueChanged(string newDate)
    {
        SceneController.Instance.TryUpdateRow(attribute, newDate, transform.parent.parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText);
    }

    public void Init(string date)
    {
        if (date != null)
        {
            _datePicker.SelectedDate = new SerializableDate(DateTime.Parse(date));
        }
    }
}
