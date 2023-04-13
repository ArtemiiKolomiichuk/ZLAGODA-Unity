using System.Collections.Generic;

namespace Entities
{
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
        public static List<CellType> CellTypes()
        {
            return null;
        }
    }

    public class Address : Entity
    {
        public static new readonly int dimensions = 4;
        public int id_address { get; set; }
        public string city { get; set; }
        public string street { get; set; }
        public int index { get; set; }

        public override string ToString()
        {
            return $"Address: \n\aid_address: {id_address}, \n\acity: {city}, \n\astreet: {street}, \n\aindex: {index}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_address.ToString(), city, street, index.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
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
            return $"Bill: \n\acheck_number: {check_number}, \n\aid_employee: {id_employee}, \n\acard_number: {card_number}, \n\adiscount: {discount}, \n\aprint_date: {print_date}, \n\asum_total: {sum_total}, \n\avat: {vat}";
        }
        public override List<string> ToList()
        {
            return new List<string> { check_number.ToString(), id_employee.ToString(), card_number.ToString(), discount.ToString(), print_date, sum_total.ToString(), vat.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
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
        public static new readonly int dimensions = 5;
        public int card_number { get; set; }
        public int cust_pib { get; set; }
        public string phone_number { get; set; }
        public int cust_address { get; set; }
        public decimal percent { get; set; }

        public override string ToString()
        {
            return $"Customer_card: \n\acard_number: {card_number}, \n\acust_pib: {cust_pib}, \n\aphone_number: {phone_number}, \n\acust_address: {cust_address}, \n\apercent: {percent}";
        }
        public override List<string> ToList()
        {
            return new List<string> { card_number.ToString(), cust_pib.ToString(), phone_number, cust_address.ToString(), percent.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.InputField, CellType.FKButton, CellType.InputField };
        }
    }

    public class Employee : Entity
    {
        public static new readonly int dimensions = 8;
        public int id_employee { get; set; }
        public int empl_pib { get; set; }
        public string role { get; set; }
        public decimal salary { get; set; }
        public string date_of_start { get; set; }
        public string date_of_birth { get; set; }
        public string phone_number { get; set; }
        public int empl_address { get; set; }

        public override string ToString()
        {
            return $"Employee: \n\aid_employee: {id_employee}, \n\aempl_pib: {empl_pib}, \n\arole: {role}, \n\asalary: {salary}, \n\adate_of_start: {date_of_start}, \n\adate_of_birth: {date_of_birth}, \n\aphone_number: {phone_number}, \n\aempl_address: {empl_address}";
        }
        public override List<string> ToList()
        {
            return new List<string> { id_employee.ToString(), empl_pib.ToString(), role, salary.ToString(), date_of_start, date_of_birth, phone_number, empl_address.ToString() };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField, CellType.FKButton };
        }
    }

    public class PIB : Entity
    {
        public static new readonly int dimensions = 4;
        public int id_pib { get; set; }
        public string name { get; set; }
        public string last_name { get; set; }
        public string patronymic { get; set; }

        public override string ToString()
        {
            return $"PIB: \n\aid_pib: {id_pib}, \n\aname: {name}, \n\alast_name: {last_name}, \n\apatronymic: {patronymic}";
        }

        public override List<string> ToList()
        {
            return new List<string> { id_pib.ToString(), name, last_name, patronymic };
        }
        public static new List<CellType> CellTypes()
        {
            return new List<CellType> { CellType.InputField, CellType.InputField, CellType.InputField, CellType.InputField };
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
            return new List<CellType> { CellType.InputField, CellType.FKButton, CellType.InputField, CellType.InputField };
        }
    }
}
