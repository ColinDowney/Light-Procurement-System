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

        //        <DataGridTextColumn Binding = "{Binding Path=NO}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=Date}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=Notes}" Width="150" IsReadOnly="True"/>
        //<DataGridCheckBoxColumn Binding = "{Binding Path=isSelected}" Width="30" x:Name="Check" IsReadOnly="False" />
        public static string[] Quotation_formColName = { "单号", "日期", "备注", "发送报价单" };
        public static string[] Quotation_informationColName = { "货品类目", "货品名称", "货品型号", "数量", "单价" };

        //        <DataGridTextColumn Binding = "{Binding Path=NO}" Width="80" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=Date}" Width="150" IsReadOnly="True"/>
        //<DataGridTextColumn Binding = "{Binding Path=Notes}" Width="150" IsReadOnly="True"/>
        public static string[] RFQColName = { "单号", "日期", "备注" };

                //        <DataGridTextColumn Binding = "{Binding Path=NO}" Width="30" IsReadOnly="True"/>
                //<DataGridTextColumn Binding = "{Binding Path=Source}" Width="80" IsReadOnly="True"/>
                //<DataGridTextColumn Binding = "{Binding Path=Supplier}" Width="80" IsReadOnly="True"/>
                //<DataGridTextColumn Binding = "{Binding Path=Date}" Width="50" IsReadOnly="True"/>
                //<DataGridTextColumn Binding = "{Binding Path=Notes}" Width="50" IsReadOnly="False"/>
                //<DataGridCheckBoxColumn Binding = "{Binding Path=isSelected}" Width="80" x:Name="Check" IsReadOnly="False" />
        public static string[] QuotationAudit_formColName = { "单号", "来源", "供货商", "日期", "备注", "审核通过"};

        public static string[] Sale_informationColName = { "销售详单号", "销售批次单号", "货品编号", "数量", "单价", "备注" };
        public static string[] Sale_formColName = { "销售批次单号","用户编号","订购单编号","货品来源",
            "管理员编号","总价","创建日期","销售单状态","备注" };

        public static string[] OrderViewColName = { "单号", "供货商编号", "总价", "日期", "备注" };
        public static string[] OrderViewInfoColName = { "货品类别", "货品名称", "货品型号", "数量", "单价" };
    }
}
