using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared
{
    public static class StringHelper
    {
        public static bool  ContainsOrDefault(this string str,string value)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return str.Contains(value);
        }
    }
}
