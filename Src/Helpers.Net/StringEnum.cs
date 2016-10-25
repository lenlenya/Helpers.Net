using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Collections.Concurrent;

namespace Helpers.Net
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    /// NOTE: I borrowed this code from somewhere a really long time ago.. don't know from where.
    /// </summary>
    public class StringValueAttribute : Attribute
    {
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string StringValue { get; protected set; }

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringValueAttribute(string value)
        {
            this.StringValue = value;
        }
    }

    /// <summary>
    /// Enum's StringValue.
    /// </summary>
    public static class StringEnumExtensions
    {
        private static ConcurrentDictionary<Type, IDictionary<string, string>> enums = new ConcurrentDictionary<Type, IDictionary<string, string>>();

        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            if (value == null) return null;
            // Get the type
            Type type = value.GetType();
            if (Enum.IsDefined(type, value))
            {
                var name = Enum.GetName(type, value);
                return GetAttributes(type)[name];
            }
            return value.ToString();
        }

        /// <summary>
        /// 获取枚举值的描述说明，通过缓存优化性能。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IDictionary<string, string> GetAttributes(Type type)
        {
            IDictionary<string, string> dict = null;
            if (!enums.TryGetValue(type, out dict))
            {
                dict = new Dictionary<string, string>();
                foreach (var item in type.GetFields())
                {
                    if (!item.IsLiteral) continue;//此处过滤了编译过程中产生的一个Field，一开始还没发现，感觉很奇怪为啥会在编译的时候多了一个Field
#if NET40
                    var attrs = (StringValueAttribute[])item.GetCustomAttributes(typeof(StringValueAttribute), false);
#else
                    var attrs = item.GetCustomAttributes<StringValueAttribute>(false);
#endif
                    dict.Add(item.Name, attrs.FirstOrDefault().IfNotNull(item.Name, x => x.StringValue));
                }
                enums.TryAdd(type, dict);
            }
            return dict;
        }
    }
}
