using LPS.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security;
using System.Text;

namespace LPS.Manager
{
    public static class Functions
    {
        public class product
        {
            public int Product_ID { get; set; }
            public string Product_name { get; set; }
            public string Product_category { get; set; }
            public string Product_modle { get; set; }
            public int Num { get; set; }

            public product(int product_id = 0, int num = 0, string product_name = "", string product_category = "", string product_modle = "")
            {
                Product_ID = product_id;
                Product_name = product_name;
                Product_category = product_category;
                Product_modle = product_modle;
                Num = num;
            }

            public void Set(int product_id = 0, int num = 0, string product_name = "", string product_category = "", string product_modle = "")
            {
                Product_ID = product_id;
                Product_name = product_name;
                Product_category = product_category;
                Product_modle = product_modle;
                Num = num;
            }
        }

        /// <summary>
        /// 更新订购单的状态
        /// </summary>
        /// <param name="NewStatus">新状态的名称:"未审核", "审核通过","审核不通过","取消","进入销售","完成","已询价"</param>
        /// <param name="Order_id">要更新的订单编号Order_id_PK</param>
        public static void UpdateOrderStatus(string NewStatus, int Order_id)
        {
            string command = string.Format("UPDATE Order_form SET Order_form_status='{0}' WHERE Order_form_id_PK={1}", NewStatus, Order_id);
            int re = Database.ExecuteSqlCommand(command);
            if (re <= 0)
                throw new Exception("Error occur when updating '" + Order_id.ToString() + "' in Order form.");

            if (NewStatus == "审核通过")
            {
                command = string.Format("UPDATE Order_information SET Order_information_Status='{0}' WHERE Order_form_id_FK={1}",
    1, Order_id);
                re = Database.ExecuteSqlCommand(command);
                if (re <= 0)
                    throw new Exception("Error occur when updating '" + Order_id.ToString() + "' in Order_information form.");
            }
        }

        /// <summary>
        /// 更新询价单的状态
        /// </summary>
        /// <param name="Order_id">要更新的订单编号Order_id_PK</param>
        public static void UpdateRFQStatus(int Order_id)
        {
            string command = string.Format("UPDATE RFQ SET RFQ_status='等待' WHERE Order_form_id_PK={0}", Order_id);
            int re = Database.ExecuteSqlCommand(command);
            if (re <= 0)
                throw new Exception("Error occur when updating '" + Order_id.ToString() + "' in RFQ form.");

        }

        /// <summary>
        /// 更新订购单细单的状态
        /// </summary>
        /// <param name="toUpdate">对应于要更新的订购单细单的货品</param>
        public static void UpdateOrderInformationStatus(product toUpdate)
        {
            string command = string.Format("UPDATE Order_information SET Order_information_Status={0} " +
                "WHERE Product_category='{1}' AND Product_name='{2}' AND Product_modle='{3}'",
                2, toUpdate.Product_category, toUpdate.Product_name, toUpdate.Product_modle);
            int re = Database.ExecuteSqlCommand(command);
            if (re <= 0)
                throw new Exception("Error occur when updating '" + toUpdate.Product_name + "' in Order_information form.");
        }

        /// <summary>
        /// 获取货品编号（在货物表中查找货品编号），如果没有会抛出异常
        /// </summary>
        /// <param name="Product_category"></param>
        /// <param name="Product_name"></param>
        /// <param name="Product_modle"></param>
        /// <returns>货品编号</returns>
        public static int ProductID(string Product_category, string Product_name, string Product_modle)
        {
            string command = string.Format("SELECT Product_id_PK FROM Product_information " +
                "WHERE Product_category='{0}' AND Product_name='{1}' AND Product_modle='{2}'",
                Product_category, Product_name, Product_modle);
            var re = Database.Query(command);
            if (re == null)//不应该没有的哈
            {
                throw new Exception(string.Format("The Product_information in database doesn't have the id of product[name:{0}, category:{1}, modle:{2}]"
                    , Product_name, Product_category, Product_modle));
            }
            return (int)re;
        }

        /// <summary>
        /// 将审核通过的订购单中未记录在货品信息表的货品信息插入到货品信息表
        /// </summary>
        public static void InsertGoodsInOrderToInventory()
        {
            try
            {
                DataTable Table = Database.FillDataTable(
                    "SELECT Product_category, Product_name, Product_modle FROM Product_information");
                List<product> inventorys = new List<product>();
                foreach(DataRow row in Table.Rows)
                {
                    inventorys.Add(new product(product_name: (string)row["Product_name"],
                        product_category: (string)row["Product_category"], product_modle: (string)row["Product_modle"]));
                }

                string command = string.Format("SELECT Order_form_id_PK FROM Order_form WHERE Order_form_status='{0}'"
                    , "审核通过");
                Table = Database.FillDataTable(command);
                DataTable infoTable;
                List<product> toInsert = new List<product>();
                product tempro;
                foreach (DataRow row in Table.Rows)
                {
                    command = string.Format("SELECT * FROM Order_information WHERE Order_form_id_FK={0}", row["Order_form_id_PK"]);
                    infoTable = Database.FillDataTable(command);
                    foreach(DataRow infoRow in infoTable.Rows)
                    {
                        if (!inventorys.Exists(x => (x.Product_category == (string)infoRow["Product_category"] &&
                         x.Product_modle == (string)infoRow["Product_modle"] && x.Product_name == (string)infoRow["Product_name"])))
                        {
                            tempro = new product(product_name: (string)infoRow["Product_name"], product_category: (string)infoRow["Product_category"],
                                product_modle: (string)infoRow["Product_modle"]);
                            toInsert.Add(tempro);
                            inventorys.Add(tempro);
                        }
                    }
                }
                InsertIntoProduct_information(toInsert);
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// 对于审核通过的订购单，对相同的商品进行汇总，并返回每一种商品的总数（本来是要先查找库存然后再生成缺货单的）
        /// </summary>
        /// <returns>合并现有的订购单后每一种商品的总数</returns>
        public static List<product> checkInventory()
        {
            try
            {
                DataTable orderTable = Database.FillDataTable("SELECT * FROM Order_form WHERE Order_form_status='审核通过'");
                string command;
                DataTable infoTable;
                List<product> backList = new List<product>();
                foreach (DataRow row in orderTable.Rows)
                {
                    //对于该审核通过的订购单里面每一项具体的订购进行处理
                    command = string.Format("SELECT * FROM Order_information WHERE Order_form_id_FK={0} AND Order_information_Status=1", row["Order_form_id_PK"]);
                    infoTable = Database.FillDataTable(command);
                    foreach(DataRow infoRow in infoTable.Rows)
                    {
                        //对于每一项货品，在货物表中查找其编号
                        //似乎这样效率很低，是否应该先全部读出来，再在内存中查找？
                        int productID = ProductID((string)infoRow["Product_category"], 
                            (string)infoRow["Product_name"], (string)infoRow["Product_modle"]);
                        //目前没有查找库存就直接出缺货单
                        if (backList.Exists(x => x.Product_ID == productID))
                        {
                            int index = backList.FindIndex(x => x.Product_ID == productID);
                            backList[index].Num = backList[index].Num + (int)infoRow["Num_of_product"];
                        }
                        else
                        {
                            backList.Add(new product(productID, (int)infoRow["Num_of_product"], 
                                (string)infoRow["Product_name"], (string)infoRow["Product_category"], 
                                (string)infoRow["Product_modle"]));
                        }
                    }
                }
                return backList;
            }
            catch (Exception ex)
            {
                Tools.ShowMessageBox(ex.Message);
            }
            return null;
        }
        /*
        /// <summary>
        /// 创建这一笔货品对应的询价单及询价细单
        /// </summary>
        /// <param name="productList">在同一笔询价单中的货品</param>
        /// <param name="Notes">询价单备注</param>
        public static void CreateRFQ(List<product> productList, string Notes = "")
        {
            try
            {
                if (Notes == null)
                    Notes = "";
                string tableName = "RFQ";
                string comInsert = "INSERT INTO " +
                    tableName + "(RFQ_createdate, RFQ_status, RFQ_notes)" +
                    "values(@DATE, @STATUS, @NOTES)";
                SqlDbType[] types = { SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                string[] keys = { "@DATE", "@STATUS", "@NOTES" };//上面写的参数名

                List<object> values = new List<object>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                bool returnVal = false;//判断是否成功执行

                values.Add(System.DateTime.Now);
                values.Add("等待");
                values.Add(Notes);

                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把三个参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert(parameters, comInsert);
                //这里设计有问题哦 既然之前handle了exception为什么这里还要throw哦
                if (!returnVal)
                    throw new Exception("Error occur when inserting the RFQ form.");

                //获取这条记录在RFQ的编号
                int rfqID = (int)Database.Query(string.Format("SELECT MAX(RFQ_id_PK) from {0}",
                    tableName));

                //插入询价细单
                foreach (product item in productList)
                {
                    values.Clear();
                    tableName = "RFQ_information";
                    comInsert = "INSERT INTO " +
                        tableName + "(RFQ_id_FK, Product_category, Product_name, Product_modle, Num_of_product)" +
                        "values(@ID, @CATE, @NAME, @MODLE, @NUM)";
                    SqlDbType[] itypes = { SqlDbType.Int, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int };//数据类型
                    string[] ikeys = { "@ID", "@CATE", "@NAME", "@MODLE", "@NUM" };//上面写的参数名

                    Dictionary<string, List<object>> iparameters = new Dictionary<string, List<object>>();//用来传参的
                    returnVal = false;//判断是否成功执行

                    values.Add(rfqID);
                    values.Add(item.Product_category);
                    values.Add(item.Product_name);
                    values.Add(item.Product_modle);
                    values.Add(item.Num);

                    for (int j = 0; j < values.Count; ++j)
                    {
                        //依次把三个参数放入字典中
                        iparameters[ikeys[j]] = new List<object> { itypes[j], values[j] };
                    }
                    returnVal = Database.Insert(iparameters, comInsert);
                    //这里设计有问题哦 既然之前handle了exception为什么这里还要throw哦
                    if (!returnVal)
                        throw new Exception("Error occur when inserting the RFQ_information form.");

                    UpdateOrderInformationStatus(item);
                    //MessageBox.Show("添加成功！");
                }
            }
            //嘛 实际上后端不应该弹消息才对的……随便了……
            catch (Exception ex)
            {
                throw ex;
                //System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
        }    
        */

        /// <summary>
        /// 创建这一笔订购单对应的询价单
        /// </summary>
        /// <param name="Order_id">订购单编号</param>
        /// <param name="Notes">询价单备注</param>
        public static void CreateRFQ(int Order_id, string Notes = "")
        {
            try
            {
                if (Notes == null)
                    Notes = "";
                string tableName = "RFQ";
                string comInsert = "INSERT INTO " +
                    tableName + "(RFQ_createdate, RFQ_status, RFQ_notes, Order_id_FK)" +
                    "values(@DATE, @STATUS, @NOTES, @ORDERID)";
                SqlDbType[] types = { SqlDbType.DateTime, SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.Int };//数据类型
                string[] keys = { "@DATE", "@STATUS", "@NOTES", "ORDERID" };//上面写的参数名

                List<object> values = new List<object>();//用来临时存参数的
                Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                bool returnVal = false;//判断是否成功执行

                values.Add(System.DateTime.Now);
                values.Add("等待");
                values.Add(Notes);
                values.Add(Order_id);

                for (int j = 0; j < values.Count; ++j)
                {
                    //依次把参数放入字典中
                    parameters[keys[j]] = new List<object> { types[j], values[j] };
                }
                returnVal = Database.Insert(parameters, comInsert);
                //这里设计有问题哦 既然之前handle了exception为什么这里还要throw哦
                if (!returnVal)
                    throw new Exception("Error occur when inserting the RFQ form.");
            }
            //嘛 实际上后端不应该弹消息才对的……随便了……
            catch (Exception ex)
            {
                throw ex;
                //System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 生成货品缺货单
        /// </summary>
        /// <param name="BackList">缺货货品</param>
        public static void BackProduct(List<product> BackList)
        {
            try
            {
                foreach (product item in BackList)
                {

                    string tableName = "Backorder_information";
                    string comInsert = "INSERT INTO " +
                        tableName + "(Product_id, Num, isHandled)" +
                        "values(@ID, @NUM, @STATUS)";
                    SqlDbType[] types = { SqlDbType.Int, SqlDbType.Int, SqlDbType.Bit };//数据类型
                    string[] keys = { "@ID", "@NUM", "@STATUS" };//上面写的参数名

                    List<object> values = new List<object>();//用来临时存参数的
                    Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                    bool returnVal = false;//判断是否成功执行

                    values.Add(item.Product_ID);
                    values.Add(item.Num);
                    values.Add(0);

                    for (int j = 0; j < values.Count; ++j)
                    {
                        //依次把三个参数放入字典中
                        parameters[keys[j]] = new List<object> { types[j], values[j] };
                    }
                    returnVal = Database.Insert(parameters, comInsert);
                    //这里设计有问题哦 既然之前handle了exception为什么这里还要throw哦
                    if (!returnVal)
                        throw new Exception("Error occur when inserting the Backorder_information form.");

                    //MessageBox.Show("添加成功！");
                }
            }
            //嘛 实际上后端不应该弹消息才对的……随便了……
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Error: " + ex.Message);
            }
        }

        /// <summary>
        /// 将不在货品信息表中的货品插入到信息表
        /// </summary>
        /// <param name="toInsert">要插入的货品</param>
        public static void InsertIntoProduct_information(List<product> toInsert)
        {
            foreach(product pd in toInsert)
            {
                try
                {
                    string tableName = "Product_information";
                    //插入数据库的命令-（列名）+（参数名-自己起的）
                    string comInsert = "INSERT INTO " +
                        tableName + "(Product_category, Product_name, Product_modle)" +
                        "values(@CATE, @NAME, @MODLE)";
                    SqlDbType[] types = { SqlDbType.VarChar, SqlDbType.VarChar, SqlDbType.VarChar };//数据类型
                    string[] keys = { "@CATE", "@NAME", "@MODLE" };//上面写的参数名

                    List<object> values = new List<object>();//用来临时存参数的
                    Dictionary<string, List<object>> parameters = new Dictionary<string, List<object>>();//用来传参的
                    bool returnVal = false;//判断是否成功执行

                    values.Add(pd.Product_category);
                    values.Add(pd.Product_name);
                    values.Add(pd.Product_modle);

                    for (int j = 0; j < values.Count; ++j)
                    {
                        //依次把三个参数放入字典中
                        parameters[keys[j]] = new List<object> { types[j], values[j] };
                    }
                    returnVal = Database.Insert(parameters, comInsert);

                    if (!returnVal)
                        throw new Exception("Error occur when inserting Product_information.");
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error: " + ex.Message);
                }

            }
        }

        public static bool Login(string userid, SecureString password)
        {
            try
            {
                Database.OnTest();
                var temp = Database.Query("SELECT Admin_password FROM Admin_information WHERE Admin_name='" + userid + "'");
                var uno = Database.Query("SELECT Admin_id_PK FROM Admin_information WHERE Admin_name='" + userid + "'");
                string admin = "Colin";
                SecureString pas = new SecureString();
                pas.AppendChar('1');                pas.AppendChar('2');                pas.AppendChar('3');                pas.AppendChar('4');
                if (temp == null)
                {
                    temp = Database.Query("SELECT Customer_password FROM Customer_information WHERE Customer_contact='" + userid + "'");
                    uno = Database.Query("SELECT Customer_id_PK FROM Customer_information WHERE Customer_contact='" + userid + "'");
                    if (temp == null)
                    {
                        temp = Database.Query("SELECT Supplier_password FROM Supplier_information WHERE Supplier_name='" + userid + "'");
                        uno = Database.Query("SELECT Supplier_id_PK FROM Supplier_information WHERE Supplier_name='" + userid + "'");
                    }else
                            throw new Exception("No such account.");
                }
                string pw = (string)temp;
                if (pw.Trim() != Tools.SecureStringToString(password))
                    throw new Exception("Wrong password.");
                //Database.Login(admin, pas);
                Database.UNO = (int)uno;
                return true;
            }catch(Exception ex)
            {
                return false;
            }
            finally
            {
                //Database.Clear();
            }
        }
    }
}
