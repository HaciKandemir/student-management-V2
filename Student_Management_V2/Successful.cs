using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Management_V2
{
    class Successful
    {
        public static void Added()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Ekleme Başarılı.");
            Console.ResetColor();
        }

        public static void Edited()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Düzenleme Başarılı.");
            Console.ResetColor();
        }

        public static void Deleted()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Silme işlemi başarılı");
            Console.ResetColor();
        }
    }
}
