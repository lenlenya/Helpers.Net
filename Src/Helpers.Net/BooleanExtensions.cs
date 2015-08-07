using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class BooleanExtensions
    {
        /// <summary>
        /// 当Boolean值为true时执行Action。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Boolean WhenTrue(this Boolean input, Action action)
        {
            if (input) action();
            return input;
        }

        /// <summary>
        /// 当Boolean值为false时执行Action。
        /// </summary>
        /// <param name="input"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Boolean WhenFalse(this Boolean input, Action action)
        {
            if (!input) action();
            return input;
        }
    }
}
