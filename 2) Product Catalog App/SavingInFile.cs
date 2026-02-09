using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace _2__Product_Catalog_App
{
    internal class SavingInFile
    {
        public static void SaveInFile(Storage storage, string filePath)
        {
            if (storage == null)
                throw new ArgumentNullException(nameof(storage));

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            string json = JsonSerializer.Serialize(storage, options);
            File.WriteAllText(filePath, json);
        }

        public static Storage LoadStorage(string filePath)
        {
            if (!File.Exists(filePath))
                return new Storage();

            string json = File.ReadAllText(filePath);

            if (string.IsNullOrWhiteSpace(json))
                return new Storage();

            return JsonSerializer.Deserialize<Storage>(json)
                   ?? new Storage();
        }

    }
}
