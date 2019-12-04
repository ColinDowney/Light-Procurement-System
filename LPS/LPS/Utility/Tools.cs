using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;

namespace LPS.Utility
{
    public static class Tools
    {
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
    }
}
