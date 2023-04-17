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
            case "1.Product":
                SceneController.Instance.currentEntity = "Product";
                SceneManager.LoadScene("Product");
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
