using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Kae.GraphLibrary
{
    public static class GenericHelper<T> 
        where T : struct, IComparable
    {
        public static readonly T MinValue = ReadStaticField("MinValue");
        public static readonly dynamic Zero;
        public static readonly dynamic MaxValue;

        private static T ReadStaticField(string name)
        {
            FieldInfo field = typeof(T).GetField(name,
                BindingFlags.Public | BindingFlags.Static);
            if (field == null)
            {
                // There's no TypeArgumentException, unfortunately. You might want
                // to create one :)
                throw new InvalidOperationException
                    ("Invalid type argument for GenericHelper<T>: " +
                     typeof(T).Name);
            }


            return (T)field.GetValue(null);
        }

        public static T Add(T lhs, T rhs)
        {
            return lhs + (dynamic)rhs;
        }

        public static bool LessThan(T lhs, T rhs)
        {
            return lhs < (dynamic)rhs;
        }

        static GenericHelper()
        {
            if (typeof(T) == typeof(float))
                Zero = (float)0;
            else if (typeof(T) == typeof(int))
                Zero = (int)0;
            else if (typeof(T) == typeof(Int64))
                Zero = (Int64)0;
            else if (typeof(T) == typeof(Int16))
                Zero = (Int16)0;
            else
                Zero = ReadStaticField("Zero");   // decimal, TimeSpan

            MaxValue = ReadStaticField("MaxValue");

        }

    }
}
