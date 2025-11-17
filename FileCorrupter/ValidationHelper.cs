using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCorrupter
{
    internal class ValidationHelper
    {

        public static void ValidateChoice(string? choice, in int optionAmount, ref bool validBool, out int chosenOption)
        {
            int cache = 0;

            bool succeeded = int.TryParse(choice, out cache) && cache > 0 && cache <= optionAmount;

            validBool = succeeded;
            chosenOption = succeeded ? cache : 0;
        }
    }
}
