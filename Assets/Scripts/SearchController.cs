using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchController : MonoBehaviour
{
    public static SearchController Instance { get; private set; }
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button searchButton;
    [SerializeField] private Button clearButton;
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

    public void SetWhereHaving(string whereHaving)
    {
        oldWhereHaving = SceneController.Instance.whereHaving;
        SceneController.Instance.whereHaving = whereHaving;
    }

    public void ClearWhereHaving()
    {
        SceneController.Instance.whereHaving = oldWhereHaving;
        oldWhereHaving = "";
    }

}
