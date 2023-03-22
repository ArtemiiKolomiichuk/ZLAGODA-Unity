using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputNumeric : InputField
{
    public override bool TryUpdate(string newText)
    {
        decimal value;
        if (decimal.TryParse(newText, out value))
        {
            SceneController.Instance.UpdateRow(
                attribute, 
                value.ToString(), 
                transform.parent.parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText);
            return true;
        }
        return false;
    }
}
