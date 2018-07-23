using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Diagnostics.Contracts;

namespace Windy.Reflection
{
    public static class ReflectionExtension
    {
        public static IEnumerable<Type> GetTypesByAncestor<T>(this Assembly target, bool ignoreAbstract = true)
        {
            Contract.Ensures(Contract.Result<IEnumerable<Type>>() != null);

            if (target == null)
                target = Assembly.GetCallingAssembly();

            var type = typeof(T);

            if (type.IsValueType)
                throw new NotSupportedException("It is impossible to derived from Value Type");

            var result = from fo in target.GetTypes()
                         where type.IsAssignableFrom(fo)
                         select fo;

            if (ignoreAbstract)
            {
                result = from fo in result
                         where !(fo.IsAbstract || fo.IsInterface)
                         select fo;
            }

            return result;
        }

        public static IEnumerable<T> CreateInstance<T>(this IEnumerable<Type> types)
        {
            Contract.Requires<ArgumentNullException>(types != null, "types");
            Contract.Ensures(Contract.Result<IEnumerable<T>>() != null);

            var result = from fo in types
                         select Activator.CreateInstance(fo);

            return result.OfType<T>();
        }
    }
}
