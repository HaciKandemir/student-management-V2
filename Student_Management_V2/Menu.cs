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
                    string _showMenu = ShowMenu(mainMenuItems);
                    // value bir sayı ise ve item uzunluğundan büyük değil ise true dönüyor.
                    bool validate = Validate.IsBetween0_X(_showMenu, mainMenuItems.Length);
                    if (validate)
                    {
                        Show(_showMenu);
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
                        if (!Validate.TryAgain(errorValue))
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
                    // Kaydedildiği bilgisini ekrana yazdırıp tekrar denemek istediği soruluyor.
                    string successfulValue = Successful.SudentSaveFile();
                    // true ise Öğrenci ekleme menüsüne git değil ise ana menüye git
                    Show(Validate.TryAgain(successfulValue) ? "0" : "-1");
                    break;
                case "5":
                    Environment.Exit(0);
                    break;
            }
        }

        // Kullanıcıya gösterilecek menü seçenekleri yazdırıyor.
        private static string ShowMenu(string[] menuItems)
        {
            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine("{0}. {1:d}", i, menuItems[i]);
            }
            return Console.ReadLine();
        }


    }
}
