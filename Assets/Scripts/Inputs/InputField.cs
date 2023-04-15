using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using System;

public abstract class InputField : MonoBehaviour
{
    private TMPro.TMP_InputField inputField;
    public string oldText;
    protected Transform parent => transform.parent.parent;
    public string attribute;
    public bool updateRowOnEdit;

    void Awake()
    {
        inputField = GetComponent<TMPro.TMP_InputField>();
    }
    void Start()
    {
        inputField.onSelect.AddListener(OnSelect);
        inputField.onDeselect.AddListener(OnDeselect);
        oldText = inputField.text;
    }
    private void OnSelect(string newText)
    {
        oldText = newText;
    }
    private void OnDeselect(string newText)
    {
        if (newText != oldText)
        {
            //TODO: nullable
            if(newText == "")
            {
                inputField.text = oldText;
            }
            else
            {
                if(!TryUpdate(newText))
                {
                    inputField.text = oldText;
                    Debug.LogWarning($"{new FormatException().GetType()}: '{newText}' is not a valid value for '{attribute}'");
                }
                else
                {
                    oldText = newText;
                }
            }
        }
    }

    public virtual bool TryUpdate(string newText)
    {
        return false;
    }
}