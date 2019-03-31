using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace BootcampCoreServices.ViewModel
{
    public class AlphanumericComparer : IComparer<string>
    {
        /***************************************************************************************
        *    Original code by Alex Zhukovskiy on Stackoverflow
        *    Source: https://stackoverflow.com/questions/5093842/alphanumeric-sorting-using-linq
        *    Author: https://stackoverflow.com/users/2559709/alex-zhukovskiy
        *    Date: 2016-01-28
        ***************************************************************************************/

        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode)]
        static extern int StrCmpLogicalW(string s1, string s2);

        public int Compare(string x, string y) => StrCmpLogicalW(x, y);
    }
}