using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ExceptionHandler : MonoBehaviour
{
    public static ExceptionHandler Instance { get; private set; }
    private Canvas canvas;
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;
    private GameObject imageBack;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        canvas = GetComponent<Canvas>();
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            canvas.worldCamera = Camera.main;
        };
    }

    private void Start()
    {
        title = GameObject.Find("ExceptionTitle").GetComponent<TextMeshProUGUI>();
        description = GameObject.Find("ExceptionDescription").GetComponent<TextMeshProUGUI>();
        imageBack = GameObject.Find("ImageBack");
        imageBack.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Hide);
        GameObject.Find("ButtonOK").GetComponent<UnityEngine.UI.Button>().onClick.AddListener(Hide);
        Hide();
    }

    private void Hide()
    {
        canvas.enabled = false;
        imageBack.SetActive(false);
    }

    public void ShowException(System.Exception e, string message = "Exception")
    {
        canvas.enabled = true;
        imageBack.SetActive(true);
        this.title.text = e.GetType().ToString();
        description.text = message;
    }
}
