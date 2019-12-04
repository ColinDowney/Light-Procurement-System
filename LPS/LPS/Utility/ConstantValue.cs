using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LPS.Utility
{
    public static class ConstantValue
    {
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
    }
}
