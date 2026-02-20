using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildExeBasic.Extentions
{
    public static class BasicExtentions
    {
        public static bool IsValueTypeOrString(this Type type)
        {
            return type.IsValueType || type == typeof(string);
        }

        public static string ToStringValueType(this object value)
        {
            return value switch
            {
                DateTime dateTime => dateTime.ToString("o"),
                bool boolean => boolean.ToStringLowerCase(),
                _ => value.ToString()
            };
        }

        public static bool IsIEnumerable(this Type type)
        {
            return type.IsAssignableFrom(typeof(IEnumerable));
        }

        public static string ToStringLowerCase(this bool boolean)
        {
            return boolean ? "true" : "false";
        }

    }
}
