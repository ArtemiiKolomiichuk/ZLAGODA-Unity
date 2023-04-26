using Entities;
using System.Collections.Generic;

namespace Entities
{
    public enum CellType
    {
        InputField = 0,
        FKButton,
        Toggle,
        Date,
        Password
    }

    public abstract class Entity
    {
        public static readonly int dimensions;
        public abstract List<string> ToList();
        public static List<CellType> CellTypes()
        {
            return null;
        }
    }

    public class Bill : Entity
    {
        public static new readonly int dimensions = 7;
        public int check_number { get; set; }
        public int id_employee { get; set; }
        public int card_number { get; set; }
        public decimal discount { get; set; }
        public string print_date { get; set; }
        public decimal sum_total { get; set; }
        public decimal vat { get; set; }

        public override string ToString()
        {
            return $"{check_number}: employee {id_employee}, {print_date}";
        }
        public override List<string> ToList()
        {
            return new List<string> { check_number.ToString(), id_employee.ToString(), card_number.ToString(), discount.ToString(), print_date, sum_total.ToString(), vat.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.FKButton, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
        }
    }

    public class Category : Entity
    {
        public static new readonly int dimensions = 2;
        public int category_number { get; set; }
        public string category_name { get; set; }

        public override string ToString()
        {
            return $"{category_number}: {category_name}";
        }
        public override List<string> ToList()
        {
            return new List<string> { category_number.ToString(), category_name };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField };
        }
    }


    public class Check_row : Entity
    {
        public static new readonly int dimensions = 6;
        public int id_row { get; set; }
        public decimal row_amount { get; set; }
        public decimal piece_price { get; set; }
        public int check_number { get; set; }
        public int id_product { get; set; }
        public decimal row_price { get; set; }

        public override string ToString()
        {
            return $"Check_row: \n\aid_row: {id_row}, \n\arow_amount: {row_amount}, \n\apiece_price: {piece_price}, \n\acheck_number: {check_number}, \n\aid_product: {id_product}, \n\arow_price: {row_price}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_row.ToString(), row_amount.ToString(), piece_price.ToString(), check_number.ToString(), id_product.ToString(), row_price.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.FKButton, CellType.FKButton, CellType.InputField };
        }
    }

    public class Customer_card : Entity
    {
        public static new readonly int dimensions = 9;
        public int card_number { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string phone_number { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int zipcode { get; set; }
        public decimal percent { get; set; }

        public override string ToString()
        {
            return $"{card_number}: {last_name} {name} {patronymic}";
        }

        public override List<string> ToList()
        {
            return new List<string> { card_number.ToString(), last_name, name, patronymic, phone_number, city, street, zipcode.ToString(), percent.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
        }
    }

    public class Employee : Entity
    {
        public static new readonly int dimensions = 13;
        public int id_employee { get; set; }
        public string last_name { get; set; }
        public string name { get; set; }
        public string patronymic { get; set; }
        public string role { get; set; }
        public decimal salary { get; set; }
        public string date_of_start { get; set; }
        public string date_of_birth { get; set; }
        public string phone_number { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int zipcode { get; set; }
        public string password { get; set; }

        public override string ToString()
        {
            return $"{id_employee}: {name} {last_name} {patronymic}";
        }

        public override List<string> ToList()
        {
            return new List<string> { id_employee.ToString(), last_name, name, patronymic, role, salary.ToString(), date_of_start, date_of_birth, phone_number, city, street, zipcode.ToString(), password };
        }

        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.FKButton, CellType.InputField,CellType.Date, CellType.Date, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.Password};
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
            return $"{id_product}: {product_name}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_product.ToString(), product_name, charachteristics, manufacturer, category_number.ToString(), discounted.ToString(), base_price.ToString(), real_price.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.FKButton, CellType.Toggle, CellType.InputField, CellType.InputField };
        }
    }

    public class Store_product : Entity
    {
        public static new readonly int dimensions = 4;
        public int id_store_product { get; set; }
        public int id_product { get; set; }
        public decimal amount { get; set; }
        public string best_before { get; set; }

        public override string ToString()
        {
            return $"Store_product: \n\aid_store_product: {id_store_product}, \n\aid_product: {id_product}, \n\aamount: {amount}, \n\abest_before: {best_before}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_store_product.ToString(), id_product.ToString(), amount.ToString(), best_before };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.InputField, CellType.Date };
        }
    }

    public class Sales_report : Entity
    {
        public static new readonly int dimensions = 4;
        public int id_product { get; set; }
        public string product_name { get; set; }
        public decimal total_amount { get; set; }
        public decimal total_revenue { get; set; }

        public override string ToString()
        {
            return $"SalesReport: \n\aid_product: {id_product}, \n\aproduct_name: {product_name}, \n\atotal_amount: {total_amount}, \n\atotal_revenue: {total_revenue}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_product.ToString(), product_name, total_amount.ToString(), total_revenue.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
        }
    }
    
    public class Report 
    {
        public decimal total_revenue { get; set; }
        public decimal total_amount { get; set; }


        public decimal price { get; set; }
        public decimal amount { get; set; }
        public string product_name { get; set; }
        public string charachteristics { get; set; }
    }
}

