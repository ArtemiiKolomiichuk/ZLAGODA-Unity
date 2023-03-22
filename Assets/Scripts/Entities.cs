using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities
{
    public enum ColumnType
    {
        Editable = 0,
        FK,
        ReadOnly 
    }

    public enum CellType
    {
        InputField = 0,
        FKButton,
        Toggle
    }

    public abstract class Entity
    {
        public static readonly int dimensions;
        public abstract List<string> ToList();
        public virtual string OrderQuery(string attr, bool desc = false)
        {
            return null;
        }
        public abstract List<ColumnType> ColumnTypes();
        public static List<CellType> CellTypes()
        {
            return null;
        }
    }

    public class Product : Entity
    {
        public static new readonly int dimensions = 8;
        public int id_product { get; set; }
        public string product_name { get; set; }
        public string charachteristics { get; set; }
        public string manufacturer { get; set; }
        public int category_number { get; set; }
        
        public int discounted { get; set; }
        public decimal base_price { get; set; }
        public decimal real_price { get; set; }

        public override string ToString()
        {
            return $"Product: \n\aid_product: {id_product}, \n\aproduct_name: {product_name}, \n\amanufacturer: {manufacturer}, \n\acharacteristics: {charachteristics}, \n\acategory_number: {category_number}, \n\adiscounted: {discounted}, \n\abase_price: {base_price}, \n\areal_price: {real_price}";
        }

        public override List<string> ToList()
        {
            return new List<string> { id_product.ToString(), product_name, charachteristics, manufacturer, category_number.ToString(), discounted.ToString(), base_price.ToString(), real_price.ToString() };
        }

        public static new string OrderQuery(string attr, bool desc = false)
        {
            return @$"
            SELECT 
                *
            FROM
                Product
            ORDER BY {attr} {(desc ? "DESC" : "ASC")}";
        }

        public override List<ColumnType> ColumnTypes()
        {
            return new List<ColumnType> { ColumnType.Editable, ColumnType.Editable, ColumnType.Editable, ColumnType.Editable, ColumnType.FK, ColumnType.Editable, ColumnType.Editable, ColumnType.ReadOnly };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.FKButton, CellType.Toggle, CellType.InputField, CellType.InputField };
        }
    }

    

    public class Category : Entity
    {
        public static new readonly int dimensions = 2;
        public int category_number { get; set; }
        public string category_name { get; set; }
        public override string ToString()
        {
            return $"Category: \n\acategory_number: {category_number}, \n\acategory_name: {category_name}";
        }

        public override List<string> ToList()
        {
            return new List<string> { category_number.ToString(), category_name };
        }
        public static new string OrderQuery(string attr, bool desc = false)
        {
            return @$"
            SELECT 
                *
            FROM
                category
            ORDER BY {attr} {(desc ? "DESC" : "ASC")}";
        }
        public override List<ColumnType> ColumnTypes()
        {
            return new List<ColumnType> { ColumnType.Editable, ColumnType.Editable };
        }

        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField };
        }
    }

    //TODO: 
    public class Bill
    {
        public int id { get; set; }
        public decimal total_price { get; set; }

        public override string ToString()
        {
            return $"Bill: --id: {id}, total_price: {total_price}";
        }
    }

    public class Row
    {
        public int id { get; set; }
        public decimal amount { get; set; }
        public int product_id { get; set; }
        public int bill_id { get; set; }
        public decimal price { get; set; }

        public override string ToString()
        {
            return $"Row: --id: {id}, amount: {amount}, product_id: {product_id}, bill_id: {bill_id}, price: {price}";
        }

    }
}
