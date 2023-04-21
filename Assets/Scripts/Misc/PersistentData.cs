using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    public static string dateFrom = "\"1111-01-01\"";
    public static string dateTo = "\"2024-04-04\"";
    public static string chosenDate = System.DateTime.Now.ToString("yyyy-MM-dd");
}
