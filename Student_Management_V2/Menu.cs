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
        readonly static string[] searchMenuItems = {
            "TCye Göre Ara", "İsme Göre Ara", "Soyisme Göre Ara", "Ana Menüye Dön" };
        readonly static string studentFileName = "students.txt";
        // Menülere yönlendirme yapacağım fonksiyon
        public static void Show(string menuIndex="-1")
        {
            Console.Clear();
            switch (menuIndex)
            {
                #region main menü
                case "-1":
                    // itemler gösterilip kullanıcıdan girdi alınıyor.
                    string resMainMenu = ShowMenu(mainMenuItems);
                    // resMainMenu bir sayı ise ve item uzunluğundan büyük değil ise true dönüyor.
                    if (!Validate.IsBetween0_X(resMainMenu, mainMenuItems.Length))
                    {
                        Error.WrongNumber();
                        Show();
                        break;
                    }
                    Show(resMainMenu);
                    break;
                #endregion

                #region Öğrenci ekle
                case "0":
                    (mStudent std, bool error) = StudentHelper.StudentCreate();
                    while (error)
                    {
                        // tekrar denemek istiyor musun
                        string errorValue = Error.WrongInputTryAgain();
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
                #endregion

                #region Arama
                case "1":
                    string resSearchMenu = ShowMenu(searchMenuItems);
                    // kullanıcınıngirdiği değer aralıkta değil ise tekrar değer girmesi isteniyor.
                    if (!Validate.IsBetween0_X(resSearchMenu, searchMenuItems.Length))
                    {
                        Error.WrongNumber();
                        Show("1");
                        break;
                    }
                    // Arama menüsünün içeriğindeki seçenek menüsü
                    switch (resSearchMenu)
                    {
                        case "3":
                            Show();
                            break;
                        default:
                            Console.Clear();
                            string searchValue = GetSearchValue();
                            // filtereleme yapmak için veriyi çekiyor.
                            var toBeFilteredStds = FileHelper.ReadFile(FileHelper.dbPath(studentFileName));
                            // veri filtreleniyor.
                            List<mStudent> filteredStds = FileHelper.ApplyFilter(toBeFilteredStds, resSearchMenu, searchValue);
                            StudentHelper.ShowStudentListToUser(filteredStds);
                            break;
                    }
                    Show(Validate.TryAgain(Successful.TryAgain()) ? "1" : "-1");
                    break;
                #endregion

                case "2":
                    var allStds = FileHelper.ReadFile(FileHelper.dbPath(studentFileName));
                    StudentHelper.ShowStudentListToUser(allStds);
                    Console.Write("Düzenlemek istediğiniz öğrencinin numarası: ");
                    string stdInput = Console.ReadLine();

                    if (!Validate.IsBetween0_X(stdInput, allStds.Count))
                    {
                        // Tekrar denemek istiyor (Enter)
                        if (Validate.TryAgain(Error.WrongInputTryAgain()))
                        {
                            Show("2");
                            break;
                        }
                        Show();
                        break;
                    }
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
            Console.WriteLine();
            Console.Write("Menü numarasını girin: ");
            return Console.ReadLine();
        }

        private static string GetSearchValue()
        {
            Console.Write("Aramak istediğiniz değeri girin: ");
            return Console.ReadLine();
        }

        

    }
}
