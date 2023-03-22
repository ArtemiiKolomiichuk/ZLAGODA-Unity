using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnSortButton : MonoBehaviour
{
    private Button button;
    public bool desc;
    public string attr;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnSortButtonClick);
    }

    private void OnSortButtonClick()
    {
        SceneController.Instance.ReloadOrdered(attr, desc);
    }
}
