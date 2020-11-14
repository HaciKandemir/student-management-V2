using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Student_Management_V2
{
    class Menu
    {
        // indis numarlarına göre yönlendirme yapacağım menüler
        readonly static string[] mainMenuItems = {
            "Öğenci Ekle", "Arama Yap", "Öğrenci Bilgisi Düzenle", "Öğrenci Sil", "Öğrencileri Göster", "Çıkış Yap" };
        readonly static string studentFileName = "sudents.txt";
        // Menülere yönlendirme yapacağım fonksiyon
        public static void Show(string menuIndex="-1")
        {
            Console.Clear();
            switch (menuIndex)
            {
                #region main menü
                case "-1":
                    // itemler gösterilip kullanıcıdan girdi alınıyor.
                    string _mainMenu = MainMenu();
                    // value bir sayı ise ve item uzunluğundan büyük değil ise true dönüyor.
                    bool validate = Validate.IsBetween0_X(_mainMenu, mainMenuItems.Length);
                    if (validate)
                    {
                        Show(_mainMenu);
                    }
                    else
                    {
                        Error.WrongNumberMainMenu();
                        Show();
                    }
                    break;
                #endregion
                case "0":
                    (mStudent std, bool error) = StudentHelper.StudentCreate();
                    while (error)
                    {
                        // tekrar denemek istiyor musun
                        string errorValue = Error.WrongInputStudentProperty();
                        if (!Validate.StudentCreateTryAgain(errorValue))
                        { 
                            Show();
                            break;
                        }
                        Console.Clear();
                        // kullanıcının önceden girdiği değerleri ekara yazdırıyor.
                        StudentHelper.WriteCacheStd(std);
                        (std, error) = StudentHelper.StudentCreate(std);
                    }
                    // file save student
                    FileHelper.AppendFile(std, studentFileName);
                    break;
                case "5":
                    Environment.Exit(0);
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
