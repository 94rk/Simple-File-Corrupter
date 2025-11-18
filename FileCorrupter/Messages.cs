using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    public class Messages
    {
        private static string[] header = new string[]
        {
            " #####  #######  #####  ",
            "#     # #       #     # ",
            "#       #       #       ",
            " #####  #####   #       ",
            "      # #       #       ",
            "#     # #       #     # ",
            " #####  #        #####  ",
            "",
        };

        private static string[] options = new string[]
        {
            "Corruption",
            "  1. Corrupt with random bytes",
            "  2. Corrupt with null bytes",
            "  3. Corrupt and change size (buggy, unstable)",
            "",
            "Destroying/Deleting",
            "  4. Destroy triple-pass",
            "  5. Extreme destroy (use at your own risk; may take longer depending on your system)",
        };

        public static void DisplayAllOptions()
        {
            foreach (string option in options)
            {
                Console.WriteLine(option);
            }
        }

        public static void DisplayHeader()
        {
            Console.ForegroundColor = ConsoleColor.Blue;

            foreach (string option in header)
            {
                Console.WriteLine(option);
            }

            Console.ResetColor();
        }
    }
}
