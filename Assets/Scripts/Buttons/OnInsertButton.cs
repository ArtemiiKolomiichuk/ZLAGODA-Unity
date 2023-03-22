using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnInsertButton : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnInsertButtonClick);
    }

    private void OnInsertButtonClick()
    {
        //TODO:
        Debug.Log("Insert");
    }
}
