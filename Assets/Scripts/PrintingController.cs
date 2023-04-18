using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintingController : MonoBehaviour
{
    string path = Application.streamingAssetsPath + "\\screenshot.png";
    [SerializeField] private GameObject[] pagePrefab; //first-mid-last
    [SerializeField] private GameObject row;
    [SerializeField] private Transform canvas;
    private GameObject table
    {
        get
        {
            return canvas.GetChild(1).GetChild(0).GetChild(0).gameObject;
        }
    }
    [SerializeField] private GameObject[] elementsToHide;

    private List<Entities.SalesReport> reports;
    private int pages
    {
        get
        {
            return reports.Count/10 + 1;
        }
    }

    
    private void Start()
    {
        string query = $@"
        SELECT 
            p.id_product,
            p.product_name,
            SUM(cr.row_amount) AS total_amount,
            SUM(cr.row_price) AS total_revenue
        FROM 
            Product p 
            JOIN Check_row cr ON p.id_product = cr.id_product
            JOIN Bill b ON cr.check_number = b.check_number
        WHERE 
            cr.row_amount > 0 AND 
            b.print_date BETWEEN {PersistentData.dateFrom} AND {PersistentData.dateTo}
        GROUP BY 
            p.id_product
        ORDER BY 
            total_revenue DESC;";
        reports = SQLController.Instance.ExecuteQuery<Entities.SalesReport>(query);
        ShowFirstPage();
        if(pages < 2)
        {
            Destroy(GameObject.Find("NextPage"));
        }
    }

    private void ShowFirstPage()
    {
        var page = Instantiate(pagePrefab[0], canvas);
        for (int i = 0; i < 10; i++)
        {
            if(i >= reports.Count)
            {
                break;
            }
            var row = Instantiate(this.row, table.transform);
            row.transform.GetChild(0).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = reports[i].id_product.ToString();
            row.transform.GetChild(1).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = reports[i].product_name;
            row.transform.GetChild(2).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = reports[i].total_amount.ToString();
            row.transform.GetChild(3).GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = reports[i].total_revenue.ToString();
        }
        GameObject.Find("From").GetComponent<TMPro.TextMeshProUGUI>().text = "з: " + PersistentData.dateFrom;
        GameObject.Find("To").GetComponent<TMPro.TextMeshProUGUI>().text = "по: " +PersistentData.dateTo;
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

    public void ShowNextPage()
    {
        Debug.Log("ShowNextPage");
    }
}
