using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputText : InputField
{
    public override bool TryUpdate(string newText)
    {
        SceneController.Instance.UpdateRow(
            attribute, 
            newText, 
            parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text);
        return true;
    }
}
