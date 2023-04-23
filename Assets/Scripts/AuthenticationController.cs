using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.Search;
using System;
using Entities;

public class AuthenticationController : MonoBehaviour
{
    [SerializeField] private TMP_InputField loginInput;
    [SerializeField] private TMP_InputField passwordInput;

    [SerializeField] private Button loginButton;

    private void Start()
    {
        loginButton.onClick.AddListener(() => Login());
    }

    private void AuthenticationFailed()
    {
        //TODO: Show error message
    }

    private void Login()
    {
        string login = loginInput.text;
        string password = InputPassword.Encrypt(passwordInput.text);
        Console.WriteLine(password);
        var users = SQLController.Instance.ExecuteQuery<Employee>(@$"SELECT * FROM Employee WHERE phone_number='{login}' AND password='{password}'");

        List<List<string>> loginsData = new List<List<string>>();
        foreach (var user in users)
        {
            loginsData.Add(user.ToList());
        }

        if (loginsData.Count != 0)
        {
            //TODO: Define user's access
            Screen.SetResolution(1366, 768, FullScreenMode.Windowed);
            SceneManager.LoadScene("Menu");
        }
        else
        {
            AuthenticationFailed();
        }
    }

}