using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;
using System.IO;

namespace Windy.IO
{
    public static class UserInputHelper
    {
        public delegate bool TryParseDelegate<T>(string input, out T result);

        public static Func<string[], Tuple<T1>> CreateToTuple<T1>(TryParseDelegate<T1> converter1)
        {
            Contract.Requires<ArgumentNullException>(converter1 != null, "converter1");

            Contract.Ensures(Contract.Result<Func<string[], Tuple<T1>>>() != null);

            return (s) =>
            {
                if (s.Length != 1)
                    return null;

                T1 t1;


                if (!converter1(s[0], out t1))
                    return null;

                return Tuple.Create(t1);
            };
        }

        public static Func<string[], Tuple<T1, T2>> CreateToTuple<T1, T2>(TryParseDelegate<T1> converter1, TryParseDelegate<T2> converter2)
        {
            Contract.Requires<ArgumentNullException>(converter1 != null, "converter1");
            Contract.Requires<ArgumentNullException>(converter2 != null, "converter2");

            Contract.Ensures(Contract.Result<Func<string[], Tuple<T1, T2>>>() != null);

            return (s) =>
            {
                if (s.Length != 2)
                    return null;

                T1 t1;
                T2 t2;

                if (!converter1(s[0], out t1) ||
                    !converter2(s[1], out t2))
                    return null;

                return Tuple.Create(t1, t2);
            };
        }

        public static Func<string[], Tuple<T1, T2, T3>> CreateToTuple<T1, T2, T3>(TryParseDelegate<T1> converter1, TryParseDelegate<T2> converter2, TryParseDelegate<T3> converter3)
        {
            Contract.Requires<ArgumentNullException>(converter1 != null, "converter1");
            Contract.Requires<ArgumentNullException>(converter2 != null, "converter2");
            Contract.Requires<ArgumentNullException>(converter3 != null, "converter3");

            Contract.Ensures(Contract.Result<Func<string[], Tuple<T1, T2, T3>>>() != null);

            return (s) =>
            {
                if (s.Length != 3)
                    return null;

                T1 t1;
                T2 t2;
                T3 t3;

                if (!converter1(s[0], out t1) ||
                    !converter2(s[1], out t2) ||
                    !converter3(s[2], out t3))
                    return null;

                return Tuple.Create(t1, t2, t3);
            };
        }

        public static T Query<T>(Func<string[], T> parser, string userHint = null, bool optional = false, TextReader input = null, TextWriter output = null, params char[] splitters)
        {
            Contract.Requires<ArgumentNullException>(parser != null, "parser");
            Contract.Requires(splitters != null);

            if (splitters.Length == 0)
                splitters = new char[] { ' ', ',', ';', '\t' };

            input = input ?? Console.In;
            output = output ?? Console.Out;

            while (true)
            {
                output.Write(userHint ?? string.Empty);
                var line = input.ReadLine();

                if (optional && string.IsNullOrWhiteSpace(line))
                    return default(T);

                var parts = line.Split(splitters, StringSplitOptions.RemoveEmptyEntries);

                var result = parser(parts);

                if (!object.Equals(result, default(T)))
                    return result;

                output.WriteLine("Invalid Input\r\n");
            };
        }

        public static string QueryString(string userHint = null, bool optional = false, TextReader input = null, TextWriter output = null)
        {
            input = input ?? Console.In;
            output = output ?? Console.Out;

            while (true)
            {
                output.Write(userHint ?? string.Empty);
                var line = input.ReadLine();

                if (string.IsNullOrWhiteSpace(line))
                {
                    if (optional)
                        return null;
                }
                else
                {
                    return line.Trim();
                }

                output.WriteLine("Invalid Input\r\n");
            };
        }
    }
}
