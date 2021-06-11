using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.HelpMethods
{
    public class Utils
    {
        public static int GetRandomNumber(int maxValue)
        {
            Random random = new Random();
            int number = random.Next(maxValue);

            return number;
        }
    }
}
