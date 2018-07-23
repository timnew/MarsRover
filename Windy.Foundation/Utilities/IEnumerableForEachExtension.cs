using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Windy
{
    public static class IEnumerableForEachExtension
    {
        public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
        {
            Contract.Requires<ArgumentNullException>(items != null, "items");
            Contract.Requires<ArgumentNullException>(action != null, "action");

            foreach (var fo in items)
                action(fo);
        }
    }
}
