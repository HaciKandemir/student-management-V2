using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Management_V2
{
    public class Validate
    {
        public static bool IsNumber(string value)
        {
            return Int32.TryParse(value, out int number);
        }

        // sayı sıfır ile x arasında mı
        public static bool IsBetween0_X(string value, int limit)
        {
            // value int olabiliyorsa ve 0 ile limit arasında ise true döndür
            return Int32.TryParse(value, out int number) && number < limit && number > -1 ? true : false; 
        }

    }
}
