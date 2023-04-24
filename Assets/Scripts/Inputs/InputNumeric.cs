using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumeric : InputField
{
    public override bool TryUpdate(string newText)
    {
        decimal value;
        if (decimal.TryParse(newText.Replace('.', ','), out value))
        {
            if (value >= 0)
            {
                if (!SceneController.Instance.TryUpdateRow(
                    attribute,
                    value.ToString().Replace(',', '.'),
                    parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText))
                {
                    return false;
                }

                if (updateRowOnEdit)
                {
                    SceneController.Instance.RepaintRow(parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText, parent,
                    gameObject.GetComponent<Image>().color.r < 0.98f);
                }
                return true;
            } 
            else { return false; }
        }
        return false;
    }
}
