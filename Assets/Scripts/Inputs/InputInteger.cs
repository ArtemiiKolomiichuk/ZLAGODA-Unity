using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputInteger : InputField
{
    public override bool TryUpdate(string newText)
    {
        int value;
        if (int.TryParse(newText, out value))
        {
            if (value >= 0)
            {
                if (!SceneController.Instance.TryUpdateRow(
                    attribute,
                    value.ToString(),
                    parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        else {
            return false;
        }
    }
}
