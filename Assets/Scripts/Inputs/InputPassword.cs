using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class InputPassword : InputField
{
    public override bool TryUpdate(string newText)
    {
        return SceneController.Instance.TryUpdateRow(
            attribute,
            Encrypt(newText),
            parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text);
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

}
