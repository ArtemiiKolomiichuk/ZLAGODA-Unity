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

    public static string Encrypt(string s)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            sb.Append((char)(s[i] ^ "TyNdeEaWRigo8lYuFyvWEiosrtET4q80"[(i + 3) % 17 + 2]));
        }
        return sb.ToString();
    }
}
