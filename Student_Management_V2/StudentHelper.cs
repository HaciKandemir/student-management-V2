using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Student_Management_V2
{
    class StudentHelper
    {
        // Yeni öğrenci oluşturuyor.
        public static (mStudent createdStd, bool error) StudentCreate(mStudent cacheStd = null)
        {
            // cacheStd null ise yeni nesne oluşturyor dolu ise cacheStd yi kopyalıyor.
            mStudent std = cacheStd ?? new mStudent();

            // Daha değer atanmamış mStudent propertilerini seçiyor.
            var emptyStdProperty = std.GetType().GetProperties().Where(x => x.GetValue(std) == null);
            // kullanıcın girdiği değerler validate işleminden geçirildikten sonra propertysinin türüne göre 
            object output;
            foreach (var stdProp in emptyStdProperty)
            {
                string propName = stdProp.Name;
                Console.Write(propName + ": ");
                output = null;
                // kullanıcının girdiği değerin property türü ile uyuşmasını kontrol ediyor ve dönüştürülen değeri output a atıyor.
                bool validate = Validate.StudentProperty(Console.ReadLine(), propName, ref output);
                if (validate)
                {
                    // mStudent den oluşturulan std nesnesinin stdProp propertisine kullanıcının girdiği değeri atıyor.
                    stdProp.SetValue(std, output);
                }
                else // girilen değer property nin türüne çevrilemiyorsa geri dönüyor.
                {
                    return (std, true);
                }
            }
            return (std, false);
        }

        public static void WriteCacheStd(mStudent std)
        {
            std.FilledProperty().ForEach(Console.WriteLine);
        }

        public static void ShowStudentListToUser(List<mStudent> stds)
        {
            if (stds.Count > 0)
            {
                for (int i = 0; i < stds.Count; i++)
                {
                    Console.WriteLine(i+". "+stds[i]);
                }
            }
            else
            {
                Console.WriteLine("Eşleşen değer bulunamadı.");
            }
            Console.WriteLine();
        }


    }
}
