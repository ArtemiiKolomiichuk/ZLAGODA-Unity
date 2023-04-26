using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static AccessController.AccessRights;
using UI.Dates;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] Canvas[] canvases; //14,19,20,21

    private bool manager => AccessController.isManager;
    public void LoadScene(string code)
    {
        switch (code)
        {
            case "1.Category":
                SceneController.Instance.currentEntity = "Category";
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Category");
                break;
            case "1.Employee":
                SceneController.Instance.currentEntity = "Employee";
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Employee");
                break;
            case "1.Product":
                SceneController.Instance.currentEntity = "Product";                
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Product");
                break;
            case "1.SProduct":
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneController.Instance.currentEntity = "Store_product";
                SceneManager.LoadScene("Store_product");                
                break;
            case "1.Bill":
                SceneController.Instance.currentEntity = "Bill";
                if(manager)
                    SceneController.Instance.accessRights = ViewDelete;
                if(!manager)
                {
                    SceneController.Instance.accessRights = Edit;
                    SceneController.Instance.whereHaving = $"WHERE id_employee = '{PersistentData.userId}'";
                }
                SceneManager.LoadScene("Bill");                
                break;
            case "1.Cards":
                SceneController.Instance.currentEntity = "Customer_card";
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                if(!manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Customer_card");
                break;
            case "1.AllRows":
                SceneController.Instance.currentEntity = "Check_row";
                if(!manager)
                {
                    SceneController.Instance.whereHaving = 
                    $"WHERE check_number IN (SELECT check_number FROM Bill WHERE id_employee = '{PersistentData.userId}')";
                }
                SceneManager.LoadScene("Check_row");                
                break;
            case "15.":
                SceneController.Instance.currentEntity = "Product";
                SceneController.Instance.whereHaving = 
                "WHERE discounted <> 0";
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Product");
                break;
            case "16.":
                SceneController.Instance.currentEntity = "Product";
                SceneController.Instance.whereHaving = 
                "WHERE discounted = 0";
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                SceneManager.LoadScene("Product");
                break;
            case "14.":
                Show(0);
                break;
            case "19.20.":
                Show(1);
                break;
            case "21.":
                Show(2);
                break;
            case "15.Seller":
                Show(1);
                Process(3);
                break;
            case "Z1K":
                SceneController.Instance.currentEntity = "Category";
                SceneController.Instance.whereHaving =
@"WHERE NOT EXISTS(
  SELECT 
    1
  FROM 
    Product p
  WHERE 
    p.category_number = Category.category_number
  AND NOT EXISTS(
    SELECT 
        1
    FROM 
        Check_row cr
    INNER JOIN 
        Product p2 ON cr.id_product = p.id_product
    WHERE 
        p2.category_number = Category.category_number
    AND 
        cr.row_amount > 0
  )
) 
";
                SceneManager.LoadScene("Category");
                break;
            case "Z2K":
                Show(3);
                break;
            case "Z1O":
                Show(4);
                break;
            case "Z2O":
                SceneController.Instance.currentEntity = "Category_products";
                SceneManager.LoadScene("Category_products");
                break;
            case "7.":
            default:
                throw new System.NotImplementedException($"LoadScene for \"{code}\"");
        }
    }


    private void Start()
    {
        var dropdown = canvases[0].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>();
        dropdown.ClearOptions();
        dropdown.AddOptions(SceneController.Instance.GetFKs("Product"));
        if(AccessController.isManager)//manager
        {
            {
                var table = canvases[1].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                dropdown = table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>();
                dropdown.ClearOptions();
                dropdown.AddOptions(SceneController.Instance.GetFKs("Seller"));
                table.GetChild(2).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
                table.GetChild(3).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            }
            //21
            {
                var table = canvases[2].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                dropdown = table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>();
                dropdown.ClearOptions();
                dropdown.AddOptions(SceneController.Instance.GetFKs("Product"));
                table.GetChild(1).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
                table.GetChild(2).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            }
            {
                var table = canvases[3].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                table.GetChild(0).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
                table.GetChild(1).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            }
            {
                var table = canvases[4].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                table.GetChild(0).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate = new SerializableDate(System.DateTime.Now.Date);
            }
        }
        HideAll();
    }
    public void HideAll()
    {
        for(int i = 0; i < canvases.Length; i++)
        {
            canvases[i].enabled = false;
            canvases[i].transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    private void Show(int index)
    {
        HideAll();
        canvases[index].enabled = true;
        canvases[index].transform.GetChild(0).gameObject.SetActive(true);
    }
    public void Process(int index)
    {
        switch(index)
        {
            case 0:
                var dropdown = canvases[0].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>();
                int id = int.Parse(dropdown.options[dropdown.value].text.Substring(0, dropdown.options[dropdown.value].text.IndexOf(":")));
                var reports = SQLController.Instance.ExecuteQuery<Entities.Report>(
                    $@"SELECT 
                        p.product_name,
                        p.charachteristics,
                        p.real_price as price,
                        coalesce(SUM(sp.amount),0) - 
                        coalesce((SELECT SUM(c.row_amount) 
                            FROM Check_row c 
                            WHERE c.id_product = {id}),0) AS amount 
                        FROM 
                            Product p
                            LEFT JOIN Store_product sp  ON sp.id_product = p.id_product 
                        WHERE 
                            p.id_product = {id};");
                canvases[0].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                $"In stock {reports[0].amount} pcs. at price {reports[0].price} UAH per pcs.\nName: {reports[0].product_name}\nCharacteristics: {reports[0].charachteristics}"; 
                break;
            case 1:
                //19
                var table = canvases[1].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                int idSeller = int.Parse(table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().options[table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().value].text.Substring(0, table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().options[table.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().value].text.IndexOf(":")));
                bool allSellers = table.GetChild(1).GetChild(0).GetChild(0).GetComponent<Toggle>().isOn;
                DateTime from = table.GetChild(2).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate;
                DateTime to = table.GetChild(3).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate;
                string where = "id_employee " + (allSellers ? "LIKE \"%%\"" : $"= '{idSeller}'");
                var reports19 = SQLController.Instance.ExecuteQuery<Entities.Report>(
                    $@"
                    SELECT
                        SUM(b.sum_total) AS total_revenue,
                        (SELECT SUM(row_amount) 
                        FROM Check_row cr 
                        WHERE cr.check_number IN 
                            (SELECT check_number 
                            FROM Bill 
                            WHERE 
                                {where}
                            AND 
                                print_date BETWEEN '{from.ToString("yyyy-MM-dd")}' AND '{to.ToString("yyyy-MM-dd")}')
                        ) AS total_amount
                    FROM 
                        Bill b 
                    WHERE 
                        b.{where}
                    AND 
                        b.print_date BETWEEN '{from.ToString("yyyy-MM-dd")}' AND '{to.ToString("yyyy-MM-dd")}';
                    ");
                canvases[1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                $"Total revenue: {reports19[0].total_revenue} UAH\nTotal amount of sold products: {reports19[0].total_amount} pcs.\n";
                break;
            case 2:
                var table2 = canvases[2].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0);
                int idProduct = int.Parse(table2.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().options[table2.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().value].text.Substring(0, table2.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().options[table2.GetChild(0).GetChild(1).GetChild(0).GetComponent<TMP_Dropdown>().value].text.IndexOf(":")));
                DateTime from2 = table2.GetChild(1).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate;
                DateTime to2 = table2.GetChild(2).GetChild(1).GetChild(0).GetComponent<DatePicker>().SelectedDate;
                var reports21 = SQLController.Instance.ExecuteQuery<Entities.Report>(
                    $@"
                    SELECT 
                        SUM(row_amount) as total_amount
                    FROM 
                        Check_row cr
                        INNER JOIN Bill b ON cr.check_number = b.check_number
                    WHERE 
                        cr.id_product = '{idProduct}'
                    AND 
                        b.print_date BETWEEN '{from2.ToString("yyyy-MM-dd")}' AND '{to2.ToString("yyyy-MM-dd")}';
                    ");
                canvases[2].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text =
                $"Total amount of sold products: {reports21[0].total_amount} pcs.\n";
                break;
            case 3:
                var seller = SQLController.Instance.ExecuteQuery<Entities.Employee>(
                    $@"
                    SELECT 
                        *
                    FROM
                        Employee
                    WHERE
                        id_employee = '{PersistentData.userId}';
                    ");
                var e = seller[0];
                canvases[1].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                @$"ID: {e.id_employee}
Name: {e.name}
Last name: {e.last_name}
Patronymic: {e.patronymic}
Date of birth: {e.date_of_birth}
Date of start: {e.date_of_start}
Role: {(e.role == "2" ? "Seller" : "Manager")}
Salary: {e.salary}
Address: {e.city}, {e.street}, {e.zipcode}
Phone number: {e.phone_number}";
                break;
            case 4:
                //sales statistics
                var dateFrom = canvases[3].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_InputField>().text;
                var dateTo = canvases[3].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_InputField>().text;
                SceneController.Instance.currentEntity = "Sales_report";
                SceneController.Instance.whereHaving = 
@$"WHERE 
    cr.row_amount > 0 AND 
    b.print_date BETWEEN '{dateFrom}' AND '{dateTo}'
GROUP BY 
    p.id_product
";
                SceneManager.LoadScene("Sales_report");
                break;
            case 5:
                var date = canvases[4].transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_InputField>().text;
                SceneController.Instance.currentEntity = "Product";
                SceneController.Instance.whereHaving =
@$"WHERE id_product NOT IN (
    SELECT id_product
    FROM Product
    WHERE id_product NOT IN (
        SELECT id_product
        FROM Store_product
        WHERE best_before>'{date}'
    )
)";
                SceneManager.LoadScene("Product");
                break;
            default:
                throw new System.NotImplementedException($"Process for \"{index}\"");
        }
    }

}
