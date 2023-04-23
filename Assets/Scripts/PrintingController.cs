using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UI.Dates;
using UI.Tables;

public class PrintingController : MonoBehaviour
{
    string path = Application.streamingAssetsPath + "\\screenshot.png";
    [SerializeField] private GameObject textCell;
    [SerializeField] private GameObject textRow;
    [SerializeField] private Transform canvas;
    [SerializeField] private GameObject[] elementsToHide;
    [SerializeField] private GameObject table;

    private List<List<string>> rowsText;
    private int pages
    {
        get
        {
            return totalRows/12 + (totalRows%12 == 0 ? 0 : 1);
        }
    }
    private int currentPage = 0;
    private int totalRows = 0;

    private void Start()
    {
        table.GetComponent<TableLayout>().ColumnWidths = GetColumnWidths();
        foreach(Transform child in PersistentData.tableHeader.transform)
        {
            var text = child.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text;
            var row = table.transform.GetChild(0);
            Instantiate(textCell, row).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = text;
        }
        rowsText = new List<List<string>>();
        foreach(Transform child in PersistentData.tableContent.transform)
        {
            var row = new List<string>();
            totalRows++;
            foreach(Transform child2 in child)
            {
                try
                {
                    row.Add(GetText(child2.GetChild(0).gameObject));
                }catch{}
            }
            rowsText.Add(row);
        }
        ShowPage(0);
        if(pages < 2)
        {
            Destroy(GameObject.Find("NextPage"));
        }
    }

    private List<float> GetColumnWidths() => PersistentData.table switch
    {
        "Product" => new List<float> { 400,400,600,400,300,200,330,330 },
        _ => throw new NotImplementedException($"Not implemented for {PersistentData.table}")
    };

    private void ShowPage(int index)
    {
        currentPage = index;
        for(int i = table.transform.childCount - 1; i > 0 ; i--)
        {
            Destroy(table.transform.GetChild(i).gameObject);
        }
        for(int i = 0; i < 12; i++)
        {
            if(index*12 + i >= totalRows)
            {
                break;
            }
            var row = Instantiate(textRow, table.transform);
            for(int j = 0; j < rowsText[index*12 + i].Count; j++)
            {
                Instantiate(textCell, row.transform).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = rowsText[index*12 + i][j];
            }
        }
    }
    public void ShowNextPage()
    {
        int index = currentPage + 1;
        if(index >= pages)
        {
            ShowPage(0);
        }
        else
        {
            ShowPage(index);
        }
    }


    private string GetText(GameObject obj)
    {
        if(obj.GetComponent<TextMeshProUGUI>() != null)
        {
            return obj.GetComponent<TextMeshProUGUI>().text;
        }
        else if(obj.GetComponent<TMP_InputField>() != null)
        {
            return obj.GetComponent<TMP_InputField>().text;
        }
        else if(obj.GetComponent<TMP_Dropdown>() != null)
        {
            return obj.GetComponent<TMP_Dropdown>().options[obj.GetComponent<TMP_Dropdown>().value].text;
        }
        else if(obj.GetComponent<Toggle>() != null)
        {
            return obj.GetComponent<Toggle>().isOn ? "+" : "-";
        }
        else if (obj.GetComponent<DatePicker>() != null)
        {
            return obj.GetComponent<DatePicker>().SelectedDate.ToString();
        }
        else
        {
            throw new NotImplementedException($"Not implemented for {obj.name}");
        }
    }

    public void OpenScreenshotPaint()
    {
        StartCoroutine(Paint());
    }

    public void PrintScreenshot()
    {
        StartCoroutine(Print());
    }

    public void OpenScreenshotExplorer()
    {
        StartCoroutine(Explorer());
    }

    private void HideElements()
    {
        foreach (GameObject element in elementsToHide)
        {
            if(element != null)
                element.SetActive(false);
        }
    }

    private void ShowElements()
    {
        foreach (GameObject element in elementsToHide)
        {
            if(element != null)
                element.SetActive(true);
        }
    }

    IEnumerator TakeScreenshot()
    {
        HideElements();
        Screen.SetResolution(1485, 1050, FullScreenMode.Windowed);
        yield return new WaitForSecondsRealtime(0.1f);
        ScreenCapture.CaptureScreenshot(path);
        yield return new WaitForSecondsRealtime(0.1f);
        //FIXME: Resolution
        Screen.SetResolution(1366, 768, FullScreenMode.Windowed);
        ShowElements();
    }

    IEnumerator Explorer()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start($"\"{path}\"");
    }

    IEnumerator Paint()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start("mspaint", $"\"{path}\"");
    }

    IEnumerator Print()
    {
        yield return TakeScreenshot();
        System.Diagnostics.Process.Start("mspaint", $"/p \"{path}\"");
    }
}
