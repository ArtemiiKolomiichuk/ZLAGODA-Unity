using UnityEngine;
using UnityEngine.SceneManagement;
public class MenuController : MonoBehaviour
{
    public void LoadScene(string code)
    {
        switch (code)
        {
            case "1.Category":
                SceneController.Instance.currentEntity = "Category";
                SceneManager.LoadScene("Category");
                break;
            case "1.Employee":
                SceneController.Instance.currentEntity = "Employee";
                SceneManager.LoadScene("Employee");
                break;
            case "1.Product":
                SceneController.Instance.currentEntity = "Product";
                SceneManager.LoadScene("Product");
                break;
            case "1.SProduct":
                SceneController.Instance.currentEntity = "Store_product";
                SceneManager.LoadScene("Store_product");
                break;
            case "1.Bill":
                SceneController.Instance.currentEntity = "Bill";
                SceneManager.LoadScene("Bill");
                break;
            case "1.Cards":
                SceneController.Instance.currentEntity = "Customer_card";
                SceneManager.LoadScene("Customer_card");
                break;
            case "1.AllRows":
                SceneController.Instance.currentEntity = "Check_row";
                SceneManager.LoadScene("Check_row");
                break;
            case "15.":
                SceneController.Instance.currentEntity = "Product";
                SceneController.Instance.whereHaving = 
                "WHERE discounted <> 0";
                SceneManager.LoadScene("Product");
                break;
            case "16.":
                SceneController.Instance.currentEntity = "Product";
                SceneController.Instance.whereHaving = 
                "WHERE discounted = 0";
                SceneManager.LoadScene("Product");
                break;
            case "Print":
                SceneManager.LoadScene("Print");                
                break;
            default:
                throw new System.NotImplementedException($"LoadScene for \"{code}\"");
        }
    }
}
