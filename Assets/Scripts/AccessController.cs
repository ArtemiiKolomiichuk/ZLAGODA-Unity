using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccessController : MonoBehaviour
{
    public static AccessController Instance { get; private set; }
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
    public enum AccessRights
    {
        View = 0,
        Edit,
        ViewDelete
    } 
    public static bool isManager = false;
}
