using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SQLite4Unity3d;
using Entities;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    /*
    void Method(int id, string name, bool order = false, string attr = "", bool desc = false)
    {
        var q = @$"
        SELECT 
            * 
        FROM 
            product
        WHERE 
            id = {id}
        AND 
            name = {name}
        {(order ? $"ORDER BY {attr} {(desc ? "DESC" : "ASC")}" : "" )}";
    }
    */
}
