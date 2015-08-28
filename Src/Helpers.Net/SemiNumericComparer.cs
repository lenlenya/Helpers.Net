using System.Collections.Generic;

namespace System
{
    //http://stackoverflow.com/questions/6396378/c-sharp-linq-orderby-numbers-that-are-string-and-you-cannot-convert-them-to-int
    /// <summary>
    /// 将一个字符串视作数字的比较器。
    /// </summary>
    public class SemiNumericComparer : IComparer<string>
    {
        /// <summary>
        /// 实现比较器。
        /// </summary>
        /// <param name="s1">字符一</param>
        /// <param name="s2">字符二</param>
        /// <returns></returns>
        public int Compare(string s1, string s2)
        {
            if (IsNumeric(s1) && IsNumeric(s2))
            {
                if (Convert.ToInt32(s1) > Convert.ToInt32(s2)) return 1;
                if (Convert.ToInt32(s1) < Convert.ToInt32(s2)) return -1;
                if (Convert.ToInt32(s1) == Convert.ToInt32(s2)) return 0;
            }

            if (IsNumeric(s1) && !IsNumeric(s2))
                return -1;

            if (!IsNumeric(s1) && IsNumeric(s2))
                return 1;

            return string.Compare(s1, s2, true);
        }

        /// <summary>
        /// 判断对象是否为数字的。
        /// </summary>
        /// <param name="value">value</param>
        /// <returns></returns>
        public static bool IsNumeric(object value)
        {
            try
            {
                int i = Convert.ToInt32(value.ToString());
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
