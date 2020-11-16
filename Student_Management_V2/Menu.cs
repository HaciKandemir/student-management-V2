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

                #region Öğenci ara
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

                #region Öğrenci düzenle
                case "2":
                    var allStds = FileHelper.ReadFile(FileHelper.dbPath(studentFileName));
                    StudentHelper.ShowStudentListToUser(allStds);
                    string stdInput = GetStudentIdValue();

                    if (!Validate.IsBetween0_X(stdInput, allStds.Count))
                    {
                        // Girdiği değer aralık dışı tekrar denemek istiyor mu
                        bool editTry = Validate.TryAgain(Error.WrongInputTryAgain());
                        Show(editTry ? "2" : "-1");
                        break;
                    }
                    int stdIndex = Int32.Parse(stdInput);
                    StudentHelper.ShowStudentToUser(allStds[stdIndex]);
                    (mStudent student, bool errorEdit) = StudentHelper.StudentCreate();
                    while (errorEdit)
                    {
                        // tekrar denemek istiyor musun
                        string errorValue = Error.WrongInputTryAgain();
                        if (!Validate.TryAgain(errorValue))
                        {
                            Show();
                            break;
                        }
                        StudentHelper.ShowStudentToUser(allStds[stdIndex]);
                        // kullanıcının önceden girdiği değerleri ekara yazdırıyor.
                        StudentHelper.WriteCacheStd(student);
                        (student, errorEdit) = StudentHelper.StudentCreate(student);
                    }
                    // yeni oluşturulan öğrenci düzenlenmek istenen öğrencinin yerine atanıyor.
                    allStds[stdIndex] = student;
                    FileHelper.WriteFile(allStds, studentFileName);
                    string tryValueForEdit = Successful.EditedSudentSaveFile();
                    Show(Validate.TryAgain(tryValueForEdit) ? "2" : "-1");
                    break;
                #endregion

                case "3":
                    var allStudents = FileHelper.ReadFile(FileHelper.dbPath(studentFileName));
                    StudentHelper.ShowStudentListToUser(allStudents);
                    string stdValueForRemove = GetStudentIdValue();
                    if (!Validate.IsBetween0_X(stdValueForRemove, allStudents.Count))
                    {
                        // Girdiği değer aralık dışı tekrar denemek istiyor mu
                        bool editTry = Validate.TryAgain(Error.WrongInputTryAgain());
                        Show(editTry ? "3" : "-1");
                        break;
                    }
                    // kullanıcının girdiği değerdeki veri siliniyor.
                    allStudents.RemoveAt(int.Parse(stdValueForRemove));
                    FileHelper.WriteFile(allStudents, studentFileName);
                    string tryValueForRemove = Successful.DeletedStutend();
                    Show(Validate.TryAgain(tryValueForRemove) ? "3" : "-1");
                    break;
                case "4":
                    List<mStudent> showedStd = FileHelper.ReadFile(FileHelper.dbPath(studentFileName));
                    StudentHelper.ShowStudentListToUser(showedStd);
                    // öğrenciler gösterildikten sonra ana menüye dönüyor.
                    Console.ReadLine();
                    Show();
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

        private static string GetStudentIdValue()
        {
            Console.Write("Seçmek istediğiniz öğrencinin numarası: ");
            return Console.ReadLine();
        }
    }
}
