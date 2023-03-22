using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Entities;
using System;

public class TableFiller : MonoBehaviour
{
    public static TableFiller Instance;
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
    public GameObject row;
    public GameObject addRowButton;
    public GameObject table;
    public RectTransform layout;

    public void FillTable(List<List<string>> data, int dimensions)
    {
        foreach (var dataRow in data)
        {
            var newRow = Instantiate(row, table.transform);
            for(int i = 0; i < dimensions; i++)
            {
                newRow.transform.GetChild(i).GetChild(0).GetComponent<TMPro.TMP_InputField>().text = dataRow[i];
            }
        }
        var addRow = Instantiate(addRowButton, table.transform);

        layout.sizeDelta = new Vector2(layout.sizeDelta.x, (data.Count+1) * 63.38f);
        var posY = layout.sizeDelta.y <= 532f ? -268 : -layout.sizeDelta.y / 2;
        layout.localPosition = new Vector3(layout.localPosition.x, posY, layout.position.z);
    }

    internal void DeleteAllChildren()
    {
        foreach (Transform child in layout.transform)
        {
            Destroy(child.gameObject);
        }
    }
}
