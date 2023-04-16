using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnInsertButton : MonoBehaviour
{
    public static OnInsertButton Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            GetComponent<Button>().onClick.AddListener(Instance.GetComponent<Button>().onClick.Invoke);
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }
}
