using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TableFiller : MonoBehaviour
{
    public static TableFiller Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public GameObject row;
    public GameObject table;
    public RectTransform layout;

    public void FillTable(List<List<string>> data)
    {
        int dimensions = data[0].Count;
        foreach (var dataRow in data)
        {
            var newRow = Instantiate(row, table.transform);
            for(int i = 0; i < dimensions; i++)
            {
                newRow.transform.GetChild(i).GetChild(0).GetComponent<TMPro.TMP_InputField>().text = dataRow[i];
            }
        }
        layout.sizeDelta = new Vector2(layout.sizeDelta.x, data.Count * 63.38f);
        var posY = layout.sizeDelta.y <= 532f ? -268 : -layout.sizeDelta.y / 2;
        layout.localPosition = new Vector3(564, posY, layout.position.z);
    }   
}
