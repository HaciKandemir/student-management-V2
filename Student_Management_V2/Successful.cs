using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Management_V2
{
    class Successful
    {
        public static string SudentSaveFile()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Ekleme Başarılı.\nTekrarlamak için ENTER'a, çıkmak için başka bir tuşa basın ");
            Console.ResetColor();
            string value = Console.ReadLine();
            //Console.Clear();
            return value;
        }
    }
}
