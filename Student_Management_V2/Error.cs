using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Management_V2
{
    class Error
    {
        public static void WrongNumber()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Yanlış değer. Tekrar denemek için ENTER'e bas.");
            Console.ResetColor();
            Console.ReadLine();
            //Console.Clear();
        }

        public static string WrongInputStudentProperty()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("Desteklenmeyen format girişi. \nTekrar denemek için ENTER'a, çıkmak için başka bir tuşa basın");
            Console.ResetColor();
            string value = Console.ReadLine();
            //Console.Clear();
            return value;
        }
    }
}
