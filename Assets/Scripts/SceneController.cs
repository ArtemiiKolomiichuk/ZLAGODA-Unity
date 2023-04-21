using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Entities;
using System;
using UnityEngine.SceneManagement;
using static AccessController;

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
                if (scene.name != "Menu" && scene.name != "Print" && scene.name != "Authentication")
                {
                    Load();
                }
                else
                {
                    currentEntity = "";
                    whereHaving = "";
                    selectFrom = "";
                    accessRights = AccessRights.View;
                }
            };
    }
    public AccessRights accessRights = AccessRights.View;
    public string currentEntity;
    public string whereHaving = "";
    private string _selectFrom;
    public string selectFrom
    {
        get
        {
            if(!string.IsNullOrEmpty(_selectFrom))
            {
                return _selectFrom;
            }
            switch (currentEntity)
            {
                case "Category":
                case "Product":
                case "Employee":
                case "Store_product":
                    return @$"
                    SELECT 
                        * 
                    FROM 
                        {currentEntity}";
                default:
                    throw new NotImplementedException($"SelectFrom for \"{currentEntity}\"");
            }
        }
        set
        {
            _selectFrom = value;
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
                case "Employee":
                    return "id_employee";
                case "Store_product":
                    return "id_store_product";
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
            case "Employee":
                return Employee.CellTypes();
            case "Store_product":
                return Store_product.CellTypes();
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
            case "Store_product":
                return new List<string> { "Product" };
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
            case "Role":
                return new List<string> {"1:Manager", "2:Seller"};
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>("SELECT * FROM Product");
                List<string> productFKs = new List<string>();
                foreach (var product in products)
                {
                    productFKs.Add(product.ToString());
                }
                return productFKs;
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
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions, null, accessRights);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions, new List<List<string>>{GetFKs("Category")}, accessRights);
                break;
            case "Employee":
                var employees = SQLController.Instance.ExecuteQuery<Employee>(query);
                List<List<string>> employeesData = new List<List<string>>();
                foreach (var employee in employees)
                {
                    employeesData.Add(employee.ToList());
                }
                TableFiller.Instance.FillTable(employeesData, Employee.CellTypes(), Employee.dimensions, new List<List<string>>{GetFKs("Role")}, accessRights);
                break;
            case "Store_product":
                var store_products = SQLController.Instance.ExecuteQuery<Store_product>(query);
                List<List<string>> store_productsData = new List<List<string>>();
                foreach (var store_product in store_products)
                {
                    store_productsData.Add(store_product.ToList());
                }
                TableFiller.Instance.FillTable(store_productsData, Store_product.CellTypes(), Store_product.dimensions, new List<List<string>>{GetFKs("Product")}, accessRights);
                break;
            default:
                throw new NotImplementedException($"Loading the table of \"{currentEntity.ToString()}\"");
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
                TableFiller.Instance.PaintRow(products[0].ToList(), Product.CellTypes(), parent, Product.dimensions, even, new List<List<string>>{GetFKs("Category")}, accessRights);
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
                TableFiller.Instance.FillTable(categoriesData, Category.CellTypes(), Category.dimensions, null, accessRights);
                break;
            case "Product":
                var products = SQLController.Instance.ExecuteQuery<Product>(query);
                List<List<string>> productsData = new List<List<string>>();
                foreach (var product in products)
                {
                    productsData.Add(product.ToList());
                }
                TableFiller.Instance.FillTable(productsData, Product.CellTypes(), Product.dimensions, new List<List<string>>{GetFKs("Category")}, accessRights);
                break;
            case "Employee":
                var employees = SQLController.Instance.ExecuteQuery<Employee>(query);
                List<List<string>> employeesData = new List<List<string>>();
                foreach (var employee in employees)
                {
                    employeesData.Add(employee.ToList());
                }
                TableFiller.Instance.FillTable(employeesData, Employee.CellTypes(), Employee.dimensions, new List<List<string>>{GetFKs("Role")}, accessRights);
                break;
            case "Store_product":
                var store_products = SQLController.Instance.ExecuteQuery<Store_product>(query);
                List<List<string>> store_productsData = new List<List<string>>();
                foreach (var store_product in store_products)
                {
                    store_productsData.Add(store_product.ToList());
                }
                TableFiller.Instance.FillTable(store_productsData, Store_product.CellTypes(), Store_product.dimensions, new List<List<string>>{GetFKs("Product")}, accessRights);
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
