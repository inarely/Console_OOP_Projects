using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2__Product_Catalog_App
{
    internal class Methods
    {
        public static int ReadInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                string number = Console.ReadLine();

                if (int.TryParse(number, out int value))
                    return value;

                Console.WriteLine("Enter a number!");
            }
        }
        public static decimal ReadDecimal(string text)
        {
            while (true)
            {
                Console.Write(text);
                string number = Console.ReadLine();

                if (decimal.TryParse(number, out decimal value))
                    return value;

                Console.WriteLine("Enter a valid input (nn,nn)!");
            }
        }
        public static string ReadSting(string text)
        {
            while (true)
            {
                Console.Write(text);
                string value = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(value))
                    return value;

                Console.WriteLine("Enter a valid text!");
            }
        }
        public static void PrintAll(Storage storage)
        {
            foreach (var pair in storage.Product)
            {
                Console.WriteLine($"ID: {pair.Key,-5}| Name: {pair.Value.Name,-15}| Amount: {pair.Value.Amount,-5}| Price: {pair.Value.Price,-5}| Storage Data: {pair.Value.storageData,-5} Days");
            }
        }
        public static void PauseAndExit()
        {
            Console.Write("\nEnter any button to exit");
            Console.ReadKey();
        }
    }
}
