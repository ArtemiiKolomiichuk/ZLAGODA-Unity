using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static AccessController.AccessRights;

public class BackButton : MonoBehaviour
{
    private bool manager => AccessController.isManager;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GoBack());
    }

    public void GoBack()
    {
        if(SceneManager.GetActiveScene().name == "Check_row")
        {
            SceneController.Instance.currentEntity = "Bill";
            if(manager)
            {
                SceneController.Instance.accessRights = ViewDelete;
                SceneController.Instance.whereHaving = "";
            }     
            if(!manager)
            {
                SceneController.Instance.accessRights = Edit;
                SceneController.Instance.whereHaving = $"WHERE id_employee = '{PersistentData.userId}'";
            }
            SceneManager.LoadScene("Bill");  
        }
        else
        {
            if(PersistentData.tableHeader != null)
            {
                Destroy(PersistentData.tableHeader);
            }
            if(PersistentData.tableContent != null)
            {
                Destroy(PersistentData.tableContent);
            }
            if(SceneManager.GetActiveScene().name == "Menu-Manager" || SceneManager.GetActiveScene().name == "Menu-Seller")
            {
                SceneManager.LoadScene("Authentication");
            }
            else if (SceneManager.GetActiveScene().name == "Authentication")
            {
                Application.Quit();
            }
            else
            {
                if(AccessController.isManager)
                {
                    SceneManager.LoadScene("Menu-Manager");
                }
                else
                {
                    SceneManager.LoadScene("Menu-Seller");
                }
            }
        }
    }
}
