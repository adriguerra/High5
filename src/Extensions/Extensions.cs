using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parse5.Extensions
{
    public class List<T> : System.Collections.Generic.List<T>
    {
        public int length => Count;
    }
    public static class Extensions
    {
        public static string toLowerCase(this String str)
        {
            return str.ToLower();
        }

        public static T[] concat<T>(this T[] first, T[] second)
        {
            return first.Concat(second).ToArray();
        }

        public static int indexOf<T> (this T[] first, T e)
        {
            return Array.IndexOf(first, e);
        }

        public static void push<T>(this List<T> list, T elem)
        {
            list.Insert(0, elem);
        }

        public static T pop<T>(this List<T> list)
        {
            var temp = list[list.length - 1];
            list.RemoveAt(list.length - 1);
            return temp;
        }

        public static void splice<T>(this List<T> list, int index, int count)
        {
            list.RemoveRange(index, count);
        }


    }
}
