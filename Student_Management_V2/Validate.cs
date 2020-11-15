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

        public static (bool karar, DateTime date) IsDateTime(string value)
        {
            return (DateTime.TryParse(value, out DateTime dateTime),dateTime);
        }

        // sayı sıfır ile x arasında mı
        public static bool IsBetween0_X(string value, int limit)
        {
            // value int olabiliyorsa ve 0 ile limit arasında ise true döndür
            return Int32.TryParse(value, out int number) && number < limit && number > -1 ? true : false; 
        }

        // kullanıcının girdiği değerlin türünü property türü ile karşılaştırıyor.
        public static bool StudentProperty(string value, string propertyName, ref object output)
        {
            output = value;
            if (propertyName == "TC")
            {
                return IsNumber(value);
            }
            else if (propertyName == "BirthDate")
            {
                output = IsDateTime(value).date;
                return IsDateTime(value).karar;
            }
            else if (!string.IsNullOrWhiteSpace(value))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // Tekrar demenemek istiyor mu
        public static bool TryAgain(string value)
        {
            // press enter for try again
            if (value == "")
                return true;
            // undefined input
            else
                return false;
        }
    }
}
