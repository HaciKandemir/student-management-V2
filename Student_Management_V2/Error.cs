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
            Console.Write("Yanlış değer. Tekrar denemek için ENTER'e bas. ");
            Console.ResetColor();
            Console.ReadLine();
            //Console.Clear();
        }

        public static void WrongInputTryAgain()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Yanlış veri girişi.");
            Console.ResetColor();
            //Console.Clear();
        }
    }
}
