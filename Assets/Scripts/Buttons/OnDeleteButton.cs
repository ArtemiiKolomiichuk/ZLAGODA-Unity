using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnDeleteButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        if(SceneController.Instance.TryDeleteRow(int.Parse(transform.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text)))
        {
            Destroy(transform.parent.gameObject);
        }
    }
}
