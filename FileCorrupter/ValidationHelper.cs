using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class ValidationHelper
    {
        public static void ValidateChoice(string? choice, ref bool validBool, out int chosenOption)
        {
            int cache = 0;

            bool succeeded = int.TryParse(choice, out cache) && cache > 0 && cache <= Shared.choiceAmount;

            validBool = succeeded;
            chosenOption = succeeded ? cache : 0;
        }

        public static void ValidateNumber(string inp, out int res)
        {
            int.TryParse(inp, out res);
            return;
        }

        public static void ValidateNumber(string inp, out float res)
        {
            float.TryParse(inp, out res);
            return;
        }

        public static void ValidateNumber(string inp, out long res)
        {
            long.TryParse(inp, out res);
            return;
        }
    }
}
