using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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

    private void Login()
    {
        string login = loginInput.text;
        string password = InputPassword.Encrypt(passwordInput.text);
        var users = SQLController.Instance.ExecuteQuery<Employee>(@$"SELECT * FROM Employee WHERE phone_number='{login}' AND password='{password}'");

        List<List<string>> loginsData = new List<List<string>>();
        foreach (var user in users)
        {
            loginsData.Add(user.ToList());
        }

        if (loginsData.Count != 0)
        {
            AccessController.isManager = (loginsData[0][4] == "1");
            PersistentData.userId = int.Parse(loginsData[0][0]);
            PersistentData.userString = $"{loginsData[0][0]}: {loginsData[0][1]} {loginsData[0][2]} {loginsData[0][3]}";
            //FIXME: resolution
            Screen.SetResolution(1920, 1080, FullScreenMode.Windowed);
            if(AccessController.isManager)
                SceneManager.LoadScene("Menu-Manager");
            else
                SceneManager.LoadScene("Menu-Seller");
        }
        else
        {
            ExceptionHandler.Instance.ShowMessage("No user found","Incorrect login or password");
        }
    }

}