using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;

namespace Student_Management_V2
{
    class FileHelper
    {

        public static string dbPath(string fileName)
        {
            string path = Path.Combine(Environment.CurrentDirectory, @"dbFiles\", fileName);
            DbFileExists(path);
            return path;
        }
        
        // dosya yoksa oluşturuyor
        private static void DbFileExists(string path)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { };
            }
        }

        // dosyanın içeriğine yeni veri ekleniyor
        public static void AppendFile(mStudent data, string fileName)
        {
            string path = dbPath(fileName);
            List<mStudent> fileJson = ReadFile(path);
            fileJson.Add(data);
            // Indented verileri tek satır yerine girintili olarak yazdırıyor.
            var serialize = JsonConvert.SerializeObject(fileJson, Formatting.Indented);
            File.WriteAllText(path, serialize);
        }

        // dosyanın içeriğindeki json verisini okuyup liste olarak mStudent class ına çeviriyor
        public static List<mStudent> ReadFile(string filePath)
        {
            var fileText = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<mStudent>>(fileText) ?? new List<mStudent>();
        }

        public static List<mStudent> ApplyFilter(List<mStudent> stdList, string propertyIndex, string value)
        {
            string prop = "";
            switch (propertyIndex)
            {
                case "0":
                    prop = "TC";
                    break;
                case "1":
                    prop = "FirstName";
                    break;
                case "2":
                    prop = "LastName";
                    break;
            }
            return stdList.Where(x => x.GetType().GetProperty(prop).GetValue(x,null).ToString().ToLower()==value.ToLower()).ToList();//.GetValue(x, null).Equals(value, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
