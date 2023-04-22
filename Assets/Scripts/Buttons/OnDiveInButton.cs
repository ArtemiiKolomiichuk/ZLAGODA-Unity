using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnDiveInButton : MonoBehaviour
{
    private void OnMouseUpAsButton()
    {
        var id = int.Parse(transform.parent.GetChild(0).GetChild(0).GetComponent<TMPro.TMP_InputField>().text);
        SceneController.Instance.selectFrom = "";
        SceneController.Instance.currentEntity = "Check_row";
        SceneController.Instance.whereHaving = $"WHERE check_number = {id}";
        SceneManager.LoadScene("Check_row");
    }
}
