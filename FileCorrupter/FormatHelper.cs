using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class FormatHelper
    {
        public static string FormatTime(int inp)
        {
            string prefix;

            prefix = inp > 1000 ? "s" : "ms";

            if (prefix == "s")
            {
                return (float)inp / 1000 + prefix;
            }
            else
            {
                return inp + prefix;
            }
        }
    }
}
