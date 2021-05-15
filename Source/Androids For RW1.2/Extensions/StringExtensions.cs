namespace MOARANDROIDS
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    static class StringExtensions
    {
        public static bool Contains(this string str, string target, StringComparison comparison)
        {
            return str.IndexOf(target, comparison) > 0;
        }
    }
}
