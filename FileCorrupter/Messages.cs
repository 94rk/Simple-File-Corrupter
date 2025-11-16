using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class Messages
    {
        public static string[] options = new string[]
        {
            "1. Corrupt",
            "2. Corrupt and change size",
        };

        public static void DisplayAllOptions()
        {
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }
    }
}
