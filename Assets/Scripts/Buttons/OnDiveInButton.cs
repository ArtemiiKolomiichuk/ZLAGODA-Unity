using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static AccessController;

public class OnDiveInButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        var id = int.Parse(transform.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text);
        SceneController.Instance.selectFrom = "";
        SceneController.Instance.currentEntity = "Check_row";
        SceneController.Instance.whereHaving = $"WHERE check_number = {id}";
        PersistentData.additionalData = id.ToString();
        if(PersistentData.isManager)
        {
            SceneController.Instance.accessRights = AccessRights.View;
        }
        SceneManager.LoadScene("Check_row");
    }
}
