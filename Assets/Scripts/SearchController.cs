using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SearchController : MonoBehaviour
{
    public static SearchController Instance { get; private set; }
    private string oldWhereHaving = "";

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Hide();
    }

    public void SetWhereHaving(string whereHaving)
    {
        oldWhereHaving = SceneController.Instance.whereHaving;
        SceneController.Instance.whereHaving = whereHaving;
        SceneController.Instance.ReloadOrdered(SceneController.Instance.pkName, false);
        Hide();
    }

    public void ClearWhereHaving()
    {
        SceneController.Instance.whereHaving = "";
        oldWhereHaving = "";
        if(SceneManager.GetActiveScene().name == "Bill" && PersistentData.isManager)
        {
            SceneController.Instance.whereHaving = $"WHERE id_employee = '{PersistentData.userId}'";
            oldWhereHaving = $"WHERE id_employee = '{PersistentData.userId}'";
        }
        else
        {
            SceneController.Instance.ReloadOrdered(SceneController.Instance.pkName, false);
        }
    }

    public void Show()
    {
        GetComponent<Canvas>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void Hide()
    {
        GetComponent<Canvas>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

}
