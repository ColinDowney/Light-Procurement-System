using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPS.Utility
{
    public static class ConstantValue
    {

            //"未审核", "审核通过","审核不通过","取消","进入销售","完成"
        

        public enum Admin_information
        {
            Admin_id_PK, Admin_name,Admin_password
        }

        public enum Class_information
        {
            Class_id_PK, College_name, Class_contact, Class_contact_phone, Class_contact_email, Class_password
        }

        public enum DataViewParams
        {
            AdminAccount, SupplierAccount, StaffAccount, ClassAccount,
            Inventory, Product_information,
            Order, Order_to_supplier, Order_to_supplier_information, Order_information,
            Pick_up_order, Pick_up_order_information,
            Purchase, Purchase_information,
            Quatation, Quatation_information,
            RFQ, RFQ_information,
            Sales_batch, Sales_order,
        }

        public static string[] Order_informationColName = {"货品类目", "货品名称", "货品型号", "数量" };

        public static string[] Order_informationCategory = { "办公用品类", "书籍类", "实验器材类", "家具类", "其他" };

        //        <DataGridTextColumn Binding = "{Binding Path=NO}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=UID}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=User}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=College}" Width="100" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=Notes}" Width="150" IsReadOnly="True"/>
        //<DataGridCheckBoxColumn Width = "30" x:Name="Check" IsReadOnly="False" />
        public static string[] Order_formColName = { "单号", "用户账号", "用户名", "学院", "备注" };

    }
}
