using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;
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

    private static Color white = new Color(1, 1, 1, 1);
    private static Color lightGray = new Color(0.9f, 0.9f, 0.9f, 1);
    
    public Scrollbar scrollbar;
    private void Start()
    {
        scrollbar.value = 1;
    }

    public void FillTable(List<List<string>> data, List<CellType> types, int dimensions, List<List<string>> FKs)
    {
        int j = 0;
        foreach (var dataRow in data)
        {
            j++;
            var newRow = Instantiate(row, table.transform);
            PaintRow(dataRow, types, newRow.transform, dimensions, j%2 == 0, FKs);
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

    internal void PaintRow(List<string> dataRow, List<CellType> types, Transform parent, int dimensions, bool even, List<List<string>> FKs)
    {
        for(int i = 0; i < dimensions; i++)
        {
            switch (types[i])
            {
                case CellType.InputField:
                    parent.GetChild(i).GetChild(0).GetComponent<TMPro.TMP_InputField>().text = dataRow[i];
                    parent.GetChild(i).GetChild(0).GetComponent<Image>().color = even ? lightGray : white;
                    break;
                case CellType.FKButton:
                    var input = parent.GetChild(i).GetChild(0).GetComponent<InputFK>();
                    input.Init(FKs[input.index], dataRow[i]);
                    if(even)
                    {
                        input.dropdown.colors = new ColorBlock()
                        {
                            normalColor = lightGray,
                            highlightedColor = new Color(0.8f, 0.8f, 0.8f, 1),
                            pressedColor = new Color(0.7f, 0.7f, 0.7f, 1),
                            disabledColor = lightGray,
                            selectedColor = lightGray,
                            colorMultiplier = 1,
                            fadeDuration = 0.1f
                        };
                    }
                    break;
                case CellType.Toggle:
                    parent.GetChild(i).GetChild(0).GetComponent<Toggle>().isOn = (dataRow[i] != "0");
                    parent.GetChild(i).GetChild(0).GetChild(0).GetComponent<Image>().color = even ? lightGray : white;
                    break;
            }
            
        }
    }
}
