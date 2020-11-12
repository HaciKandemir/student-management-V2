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
            Console.Clear();
        }
    }
}
