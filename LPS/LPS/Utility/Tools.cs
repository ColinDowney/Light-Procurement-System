using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace LPS.Utility
{
    public static class Tools
    {
        public static void DataTableToExcel(System.Data.DataTable tmDataTable, string strFileName)
        {
            if (strFileName == null) { return; }
            int RowNum = tmDataTable.Rows.Count;
            int ColumnNum = tmDataTable.Columns.Count;
            int RowIndex = 1;
            int ColumnIndex = 0;
            Microsoft.Office.Interop.Excel.Application xlapp = new Microsoft.Office.Interop.Excel.Application();           //打开Excel应用。
            if (xlapp == null)
                throw new Exception("未安装Excel。");
            xlapp.DefaultFilePath = "";
            xlapp.DisplayAlerts = true;
            Microsoft.Office.Interop.Excel.Range range = null;//Excel的格式设置
            Microsoft.Office.Interop.Excel.Workbooks workbooks = xlapp.Workbooks;
            Microsoft.Office.Interop.Excel.Workbook workbook = workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);//创建一个Excel文件
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)workbook.Worksheets[1];//拿到那个工作表 
            foreach (DataColumn dc in tmDataTable.Columns)
            {
                ColumnIndex++;
                worksheet.Cells[RowIndex, ColumnIndex] = dc.ColumnName;
            }
            for (int i = 0; i < RowNum; i++)
            {
                RowIndex++;
                ColumnIndex = 0;
                for (int j = 0; j < ColumnNum; j++)
                {
                    ColumnIndex++;
                    range = (Microsoft.Office.Interop.Excel.Range)xlapp.Cells[RowIndex, ColumnIndex];
                    range.NumberFormatLocal = "@";//写入到表中的数据格式以文本形式存在
                    worksheet.Cells[RowIndex, ColumnIndex] = tmDataTable.Rows[i][j].ToString();
                }
            }
            workbook.SaveCopyAs(strFileName);
            //workbook.Close();//这里关闭会有Excel弹窗
            // xlapp.Quit();
            //KillExcel.KillExcelApp(xlapp);
            KillExcelApp(xlapp);
        }

        [System.Runtime.InteropServices.DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int ProcessId);

        public static void KillExcelApp(this Microsoft.Office.Interop.Excel.Application app)
        {
            app.Quit();
            IntPtr intptr = new IntPtr(app.Hwnd);
            int id;
            GetWindowThreadProcessId(intptr, out id);
            var p = System.Diagnostics.Process.GetProcessById(id);
            //if (p != null)
            p.Kill();
        }

        public static bool checkLetters(string input)
        {
            string pattern = "([a-z]|[A-Z])+";
            Regex rgx = new Regex(pattern);
            return rgx.IsMatch(input);
        }

        public static String SecureStringToString(SecureString value)
        {
            IntPtr valuePtr = IntPtr.Zero;
            try
            {
                valuePtr = Marshal.SecureStringToGlobalAllocUnicode(value);
                return Marshal.PtrToStringUni(valuePtr);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(valuePtr);
            }
        }

        public static bool CheckNumberSequence(string Sequence)
        {
            Regex numbers = new Regex("^[0-9]+$");
            Match ma = numbers.Match(Sequence);
            if (ma.Success)
            {
                return true;
            }
            else
            {
                return false;  
            }
        }

        public static string IDIncrement(string ID)
        {
            string re = String.Empty;
            try
            {
                long id = Convert.ToInt64(ID);
                ++id;
                re = id.ToString().PadLeft(ID.Length, '0');
            }
            catch
            {

            }
            return re;
        }

        private static Dictionary<Type, SqlDbType> _SqlDbTypeMap;
        private static Dictionary<SqlDbType, Type> __typeMap;
        // Create and populate the dictionary in the static constructor
        static Tools()
        {
            _SqlDbTypeMap = new Dictionary<Type, SqlDbType>();

            _SqlDbTypeMap[typeof(string)] = SqlDbType.NVarChar;
            _SqlDbTypeMap[typeof(char[])] = SqlDbType.NVarChar;
            _SqlDbTypeMap[typeof(byte)] = SqlDbType.TinyInt;
            _SqlDbTypeMap[typeof(short)] = SqlDbType.SmallInt;
            _SqlDbTypeMap[typeof(int)] = SqlDbType.Int;
            _SqlDbTypeMap[typeof(long)] = SqlDbType.BigInt;
            _SqlDbTypeMap[typeof(byte[])] = SqlDbType.Image;
            _SqlDbTypeMap[typeof(bool)] = SqlDbType.Bit;
            _SqlDbTypeMap[typeof(DateTime)] = SqlDbType.DateTime2;
            _SqlDbTypeMap[typeof(DateTimeOffset)] = SqlDbType.DateTimeOffset;
            _SqlDbTypeMap[typeof(decimal)] = SqlDbType.Money;
            _SqlDbTypeMap[typeof(float)] = SqlDbType.Real;
            _SqlDbTypeMap[typeof(double)] = SqlDbType.Float;
            _SqlDbTypeMap[typeof(TimeSpan)] = SqlDbType.Time;
            _SqlDbTypeMap[typeof(bool)] = SqlDbType.Bit;
            /* ... and so on ... */

            __typeMap = new Dictionary<SqlDbType, Type>();

            __typeMap[SqlDbType.Char] = typeof(string);
            __typeMap[SqlDbType.NChar] = typeof(string);
            __typeMap[SqlDbType.NVarChar] = typeof(string);
            __typeMap[SqlDbType.VarChar] = typeof(string);
            __typeMap[SqlDbType.Int] = typeof(int);
            __typeMap[SqlDbType.SmallInt] = typeof(int);
            __typeMap[SqlDbType.BigInt] = typeof(long);
            __typeMap[SqlDbType.TinyInt] = typeof(short);
            __typeMap[SqlDbType.DateTime] = typeof(DateTime);
            __typeMap[SqlDbType.Float] = typeof(double);
            __typeMap[SqlDbType.Real] = typeof(float);
            __typeMap[SqlDbType.Bit] = typeof(bool);
            __typeMap[SqlDbType.Money] = typeof(decimal);
        }

        // Non-generic argument-based method
        public static SqlDbType GetSqlDbType(Type giveType)
        {
            // Allow nullable types to be handled
            giveType = Nullable.GetUnderlyingType(giveType) ?? giveType;

            if (_SqlDbTypeMap.ContainsKey(giveType))
            {
                return _SqlDbTypeMap[giveType];
            }

            throw new ArgumentException($"{giveType.FullName} haven't been add to the map dictionary.");
        }

        // Generic version
        public static SqlDbType GetDbType<T>()
        {
            return GetSqlDbType(typeof(T));
        }

        public static Type GetType(SqlDbType giveType)
        {
            if (__typeMap.ContainsKey(giveType))
            {
                return __typeMap[giveType];
            }

            throw new ArgumentException($"{giveType.ToString()} haven't been add to the map dictionary.");
        }

        public static void ShowMessageBox(string Message)
        {
            System.Windows.MessageBox.Show(Message);
        }
    }
}
