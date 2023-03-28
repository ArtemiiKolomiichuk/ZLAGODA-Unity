using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputNumeric : InputField
{
    public override bool TryUpdate(string newText)
    {
        decimal value;
        if (decimal.TryParse(newText, out value))
        {
            SceneController.Instance.UpdateRow(
                attribute, 
                value.ToString().Replace(',', '.'),
                parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText);

            if(updateRowOnEdit)
            {
                SceneController.Instance.RepaintRow(parent.GetChild(0).GetChild(0).GetComponent<InputField>().oldText, parent, 
                gameObject.GetComponent<Image>().color.r < 0.98f);
            }
            return true;
        }
        return false;
    }
}
