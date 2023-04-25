using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(() => GoBack());
    }

    public void GoBack()
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
