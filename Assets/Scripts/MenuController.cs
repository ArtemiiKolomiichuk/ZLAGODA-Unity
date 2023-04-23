using UnityEngine;
using UnityEngine.SceneManagement;
using static AccessController.AccessRights;

public class MenuController : MonoBehaviour
{
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
                    SceneController.Instance.accessRights = Edit;
                    //TODO: ~own bills
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
            case "19.20.":
            case "21.":
            case "9.10.":
            case "15.Seller":
            case "7.":
            default:
                throw new System.NotImplementedException($"LoadScene for \"{code}\"");
        }
    }
}
