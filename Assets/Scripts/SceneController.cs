using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using System;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        SceneManager.sceneLoaded += 
            (scene, mode) =>
            {
                if (scene.name != "Menu")
                {
                    Load();
                }
                else
                {
                    currentEntity = "";
                    whereHaving = "";
                }
            };
    }
    public string currentEntity;
    public string whereHaving = "";
    public string selectFrom
    {
        get
        {
            switch (currentEntity)
            {
                case "Category":
                case "Product":
                    return @$"
                    SELECT 
                        * 
                    FROM 
                        {currentEntity}";
                default:
                    throw new NotImplementedException($"SelectFrom for \"{currentEntity}\"");
            }
        }
    }
    private string pkName
    {
        get
        {
            switch (currentEntity)
            {
                case "Category":
                    return "category_number";
                case "Product":
                    return "id_product";
                default:
                    throw new NotImplementedException($"PK name for \"{currentEntity}\"");
            }
        }
    }

    public List<CellType> CellTypes()
    {
        switch (currentEntity)
        {
            case "Category":
                return Category.CellTypes();
            case "Product":
                return Product.CellTypes();
            default:
                throw new NotImplementedException($"CellTypes for \"{currentEntity}\"");
        }
    }

    public List<string> FKEntities()
    {
        switch (currentEntity)
        {
            case "Product":
                return new List<string> { "Category" };
            default:
                throw new NotImplementedException($"FKEntities for \"{currentEntity}\"");
        }
    }

    public List<string> GetFKs(string entity)
    {
        switch (entity)
        {
            case "Category":
                var categories = SQLController.Instance.ExecuteQuery<Category>("SELECT * FROM Category");
                List<string> categoryFKs = new List<string>();
                foreach (var category in categories)
                {
                    categoryFKs.Add(category.ToString());
                }
                return categoryFKs;
            default:
                throw new NotImplementedException($"FKs for \"{entity}\"");
        }
    }

    public void Load()
    {
        string query = @$"
        {selectFrom}
        {whereHaving};";

        switch(currentEntity)
        {
            case "Category":
                var categories = SQLController.Instance.ExecuteQuery<Category>(query);
                List<List<string>> categoriesData = new List<List<string>>();
                foreach (var category in categories)
                {
                    categoriesData.Add(category.ToList());
                }
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions, null);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions, new List<List<string>>{GetFKs("Category")});
                break;
        }
    }

    public void RepaintRow(string PK, Transform parent, bool even)
    {
        string query = @$"
        {selectFrom}
        WHERE
            {pkName} = {PK};";
        switch (currentEntity)
        {
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                TableFiller.Instance.PaintRow(products[0].ToList(), Product.CellTypes(), parent, Product.dimensions, even, new List<List<string>>{GetFKs("Category")});
                break;
            default:
                throw new NotImplementedException($"Repainting the row of \"{currentEntity.ToString()}\"");
        }
    }

    public void ReloadOrdered(string attr, bool desc)
    {
        TableFiller.Instance.DeleteAllChildren();
        string query = @$"
        {selectFrom}
        {whereHaving}
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
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions, null);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions, new List<List<string>>{GetFKs("Category")});
                break;
            default:
                throw new NotImplementedException($"Ordering the table of \"{currentEntity.ToString()}\"");
        }
    }

    public bool TryUpdateRow(string attr, string value, string PK)
    {       
        string query = @$"
        UPDATE
            {currentEntity}
        SET
            {attr} = '{value}'
        WHERE
            {pkName} = {PK};";
        return SQLController.Instance.TryExecuteNonQuery(query);
    }

    internal bool TryDeleteRow(int PK)
    {
        string query = @$"
        DELETE FROM
            {currentEntity}
        WHERE
            {pkName} = {PK};";
        return SQLController.Instance.TryExecuteNonQuery(query);
    }

    public bool TryAddRow(List<string> values)
    {
        string query = @$"
        INSERT INTO
            {currentEntity}
        VALUES
            ({string.Join(", ", values)});";
        return SQLController.Instance.TryExecuteNonQuery(query);
    }
}
