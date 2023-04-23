using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using static AccessController.AccessRights;

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
                SceneManager.LoadScene("Category");
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                break;
            case "1.Employee":
                SceneController.Instance.currentEntity = "Employee";
                SceneManager.LoadScene("Employee");
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                break;
            case "1.Product":
                SceneController.Instance.currentEntity = "Product";
                SceneManager.LoadScene("Product");
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                break;
            case "1.SProduct":
                SceneController.Instance.currentEntity = "Store_product";
                SceneManager.LoadScene("Store_product");
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                break;
            case "1.Bill":
                SceneController.Instance.currentEntity = "Bill";
                SceneManager.LoadScene("Bill");
                if(manager)
                    SceneController.Instance.accessRights = ViewDelete;
                if(!manager)
                    SceneController.Instance.accessRights = Edit;
                    //TODO: ~own bills
                break;
            case "1.Cards":
                SceneController.Instance.currentEntity = "Customer_card";
                SceneManager.LoadScene("Customer_card");
                if(manager)
                    SceneController.Instance.accessRights = Edit;
                if(!manager)
                    SceneController.Instance.accessRights = Edit;
                break;
            case "1.AllRows":
                SceneController.Instance.currentEntity = "Check_row";
                SceneManager.LoadScene("Check_row");
                if(!manager)
                {
                    SceneController.Instance.whereHaving = 
                    $"WHERE check_number IN (SELECT check_number FROM Bill WHERE id_employee = '{PersistentData.userId}')";
                    SceneController.Instance.accessRights = Edit;
                }
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
        if(manager)
        {
            //init 19-20,21
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
                        SUM(sp.amount) - 
                        (SELECT SUM(c.row_amount) 
                            FROM Check_row c 
                            WHERE c.id_product = {id}) AS amount 
                        FROM 
                            Store_product sp 
                            JOIN Product p ON sp.id_product = p.id_product 
                        WHERE 
                            sp.id_product = {17};");
                canvases[0].transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = 
                $"В наявності {reports[0].amount} шт. по ціні {reports[0].price} грн. за шт.\nНазва: {reports[0].product_name}\nХарактеристики: {reports[0].charachteristics}";
                break;
            case 1:
                //19
                break;
            case 2:
                //20
                break;
            default:
                throw new System.NotImplementedException($"Process for \"{index}\"");
        }
    }

}
