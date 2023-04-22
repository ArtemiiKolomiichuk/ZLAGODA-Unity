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
