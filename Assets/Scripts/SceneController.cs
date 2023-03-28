using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Entities;
using System;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
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

    public Scrollbar scrollbar;
    public string currentEntity;


    private void Start()
    {
        scrollbar.value = 1;
        string query = @$"
        SELECT 
            *
        FROM
            {currentEntity};";

        
        switch(currentEntity)
        {
            case "Category":
                var categories = SQLController.Instance.ExecuteQuery<Category>(query);
                List<List<string>> categoriesData = new List<List<string>>();
                foreach (var category in categories)
                {
                    categoriesData.Add(category.ToList());
                }
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions);
                break;
        }
    }

    public void RepaintRow(string PK, Transform parent, bool even)
    {
        string pkName = "";
        switch (currentEntity)
        {
            case "Category":
                pkName = "category_number";
                break;
            case "Product":
                pkName = "id_product";
                break;
        }
        string query = @$"
        SELECT 
            *
        FROM
            {currentEntity}
        WHERE
            {pkName} = {PK};";
        switch (currentEntity)
        {
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                TableFiller.Instance.PaintRow(products[0].ToList(), Product.CellTypes(), parent, Product.dimensions, even);
                break;
        }
        
    }

    public void ReloadOrdered(string attr, bool desc)
    {
        TableFiller.Instance.DeleteAllChildren();
        string query = @$"
        SELECT 
            *
        FROM
            {currentEntity}
        ORDER BY
            {attr} {(desc ? "DESC" : "ASC")};";

        switch (currentEntity)
        {
            case "Category":
                var categories = SQLController.Instance.ExecuteQuery<Category>(query);
                List<List<string>> categoriesData = new List<List<string>>();
                foreach (var category in categories)
                {
                    categoriesData.Add(category.ToList());
                }
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions);
                break;
        }
    }

    public void UpdateRow(string attr, string value, string PK)
    {       
        string pkName = "";
        switch (currentEntity)
        {
            case "Category":
                pkName = "category_number";
                break;
            case "Product":
                pkName = "id_product";
                break;
        }
        string query = @$"
        UPDATE
            {currentEntity}
        SET
            {attr} = '{value}'
        WHERE
            {pkName} = {PK};";
        SQLController.Instance.ExecuteNonQuery(query);
    }

    internal void DeleteRow(int PK)
    {
        string pkName = "";
        switch (currentEntity)
        {
            case "Category":
                pkName = "category_number";
                break;
            case "Product":
                pkName = "id_product";
                break;
        }
        string query = @$"
        DELETE FROM
            {currentEntity}
        WHERE
            {pkName} = {PK};";
        SQLController.Instance.ExecuteNonQuery(query);
    }

    public void AddRow(List<string> values)
    {
        string query = @$"
        INSERT INTO
            {currentEntity}
        VALUES
            ({string.Join(", ", values)});";
        SQLController.Instance.ExecuteNonQuery(query);
    }
}
