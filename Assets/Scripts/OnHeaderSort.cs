using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnHeaderSort : MonoBehaviour
{
    public string attribute;
    private static bool desc = true;

    public void HideSortImage()
    {
        transform.parent.GetChild(1).GetComponent<Image>().enabled = false;
    }

    public static void ResetSorting()
    {
        desc = true;
        foreach (var header in FindObjectsOfType<OnHeaderSort>())
        {
            header.HideSortImage();
        }
    }

    private void OnMouseUpAsButton()
    {
        foreach (var header in FindObjectsOfType<OnHeaderSort>())
        {
            header.HideSortImage();
        }
        desc = !desc;
        SceneController.Instance.ReloadOrdered(attribute, desc);
        transform.parent.GetChild(1).GetComponent<Image>().enabled = true;
        transform.parent.GetChild(1).localRotation = Quaternion.Euler(0, 0, desc ? 0 : 180);
    }
}
