using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class InputPassword : InputField
{
    protected override void Start2()
    {
        inputField.onSelect.AddListener(OnSelect);
        inputField.contentType = TMPro.TMP_InputField.ContentType.Password;
    }
    public override bool TryUpdate(string newText)
    {
        if(SceneController.Instance.TryUpdateRow(
        attribute,
        Encrypt(newText),
        parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text))
        {
            inputField.contentType = TMPro.TMP_InputField.ContentType.Password;
            return true;
        }
        return false;
    }

    public static string Encrypt(string s, string salt = "")
    {
        if (String.IsNullOrEmpty(s))
        {
            return String.Empty;
        }
        // Encrypt using SHA256 and salt
        var sha = new System.Security.Cryptography.SHA256Managed();
        {
            // Convert the string to a byte array first, to be processed
            byte[] textBytes = System.Text.Encoding.UTF8.GetBytes(s + salt);
            byte[] hashBytes = sha.ComputeHash(textBytes);
            string hash = BitConverter
                .ToString(hashBytes)
                .Replace("-", String.Empty);

            return hash;
        }
    }

    private new void OnSelect(string newText)
    {
        inputField.text = "";
        inputField.contentType = TMPro.TMP_InputField.ContentType.Standard;
    }
}
