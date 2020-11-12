using System;
using System.Collections.Generic;
using System.Text;

namespace Student_Management_V2
{
    class Menu
    {
        // indis numarlarına göre yönlendirme yapacağım menüler
        readonly static string[] mainMenuItems = {
            "Öğenci Ekle", "Arama Yap", "Öğrenci Bilgisi Düzenle", "Öğrenci Sil", "Öğrencileri Göster", "Çıkış Yap" };
        
        // Menülere yönlendirme yapacağım fonksiyon
        public static void Show(string menuIndex="-1")
        {
            switch (menuIndex)
            {
                case "-1":
                    // itemler gösterilip kullanıcıdan girdi alınıyor.
                    string value = MainMenu();
                    // value bir sayı ise ve item uzunluğundan büyük değil ise true dönüyor.
                    bool ok = Validate.IsBetween0_X(value, mainMenuItems.Length);
                    if (ok)
                        Show(value);
                    else
                    {
                        Error.WrongNumber();
                        Show();
                    }
                    break;
            }
        }

        // Anasayfada gösterilecek bilgileri yazdırıyor.
        private static string MainMenu()
        {
            //Array.ForEach(mainMenuItems, Console.WriteLine);
            for (int i = 0; i < mainMenuItems.Length; i++)
            {
                Console.WriteLine("{0}. {1:d}", i, mainMenuItems[i]);
            }
            return Console.ReadLine();
        }
    }
}
