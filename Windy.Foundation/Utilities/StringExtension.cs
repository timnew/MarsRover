using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Windy
{
    public static class StringExtension
    {
        public static string ApplyFormat(this string format, params object[] args)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Requires<ArgumentNullException>(args != null, "args");
            Contract.Ensures(Contract.Result<string>() != null);

            return string.Format(format, args);
        }

        public static string ApplyFormat(this string format, object arg0, object arg1, object arg2)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Ensures(Contract.Result<string>() != null);

            return string.Format(format, arg0, arg1, arg2);
        }

        public static string ApplyFormat(this string format, object arg0, object arg1)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Ensures(Contract.Result<string>() != null);

            return string.Format(format, arg0, arg1);
        }

        public static string ApplyFormat(this string format, object arg0)
        {
            Contract.Requires<ArgumentNullException>(format != null, "format");
            Contract.Ensures(Contract.Result<string>() != null);

            return string.Format(format, arg0);
        }
    }
}
