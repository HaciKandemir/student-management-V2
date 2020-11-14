using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Student_Management_V2
{
    class FileHelper
    {

        private static string dbPath(string fileName)
        {
             return Path.Combine(Environment.CurrentDirectory, @"dbFiles\", fileName);
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
            var serialize = JsonConvert.SerializeObject(fileJson);
            File.WriteAllText(path, serialize);
        }

        // dosyanın içeriğindeki json verisini okuyup liste olarak mStudent class ına çeviriyor
        public static List<mStudent> ReadFile(string filePath)
        {
            DbFileExists(filePath);
            var fileText = File.ReadAllText(filePath);
            return JsonConvert.DeserializeObject<List<mStudent>>(fileText) ?? new List<mStudent>();
        }
    }
}
