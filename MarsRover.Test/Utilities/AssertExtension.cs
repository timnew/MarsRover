using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace ThoughtWorks.CodingTests.MarsRovers
{
    public static class AssertExtension
    {
        /// <summary>
        /// Validates the equality.
        /// </summary>
        /// <typeparam name="T">The type of the data to be validated</typeparam>
        /// <param name="cases">The input cases stored as Tuple. First Item is expected value and Second is the actual value.</param>
        public static void ValidateEquality<T>(this IEnumerable<Tuple<T, T>> cases)
        {
            foreach (var fo in cases)
            {
                Assert.Equal(fo.Item1, fo.Item2);
            }
        }
    }
}
