using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace _2__Product_Catalog_App
{
    internal class Menu
    {
        static void Main()
        {
            string folderPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"../../.."));
            string dataFolder = Path.Combine(folderPath, "Data");
            
            Directory.CreateDirectory(dataFolder);           
            string filePath = Path.Combine(dataFolder, "Storage.json");


            Storage storage = new Storage();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== \"Product Catalog App\" ======\n" +
                    "1) Add Product\n" +
                    "2) Remove Product\n" +
                    "3) Increase amount of product\n" +
                    "4) Decrease amount of product\n" +
                    "5) Save Storage\n" +
                    "6) Load Storage\n" +
                    "0) Exit");

                int choice = Methods.ReadInt("Choice: ");

                switch (choice)
                {
                    case 1:
                        
                            Console.Clear();
                            Console.WriteLine("====== Add Product ======");
                        
                            string name = Methods.ReadSting("Enter product's name: ");
                            decimal price = Methods.ReadDecimal("Enter product's price: ");
                            int amount = Methods.ReadInt("Enter product's amount: ");
                            int sData = Methods.ReadInt("Enter product's storage data: ");
                        
                            Product product = new Product(name, price, amount, sData);

                            storage.AddProduct(product);

                            Methods.PauseAndExit();
                            break;
                        

                    case 2:
                        {
                            Console.Clear();
                            Console.WriteLine("====== Remove Product ======");

                            Methods.PrintAll(storage);

                            int id = Methods.ReadInt("Enter Id: ");
                            storage.RemoveProductById(id);

                            Console.WriteLine("\n---- Updated ----\n");
                            Methods.PrintAll(storage);
                            Methods.PauseAndExit();
                            break;
                        }

                    case 3:
                        {
                            Console.Clear();
                            if (storage.Product.Count == 0)
                            {
                                Console.WriteLine("Storage is empty!");
                                Methods.PauseAndExit();
                                break;
                            }
                            Console.WriteLine("====== Increase by index ======");
                            Methods.PrintAll(storage);

                            int id = Methods.ReadInt("Enter Id: ");
                            int increaseAmount = Methods.ReadInt("Increase product's amount by: ");

                            Console.WriteLine("\n---- Updated ----\n");
                            storage.IncreaseAmountById(id, increaseAmount);
                            Methods.PrintAll(storage);

                            Methods.PauseAndExit();
                            break;
                        }
                    case 4:
                        {
                            Console.Clear();
                            if (storage.Product.Count == 0)
                            {
                                Console.WriteLine("Storage is empty!");
                                Methods.PauseAndExit();
                                break;
                            }

                            Console.WriteLine("====== Decrease by index ======");
                            Methods.PrintAll(storage);

                            int id = Methods.ReadInt("Enter Id: ");
                            int decreaseAmount = Methods.ReadInt("Decrease product's amount by: ");

                            Console.WriteLine("\n---- Updated ----\n");
                            storage.DecreaseAmountById(id, decreaseAmount);
                            Methods.PrintAll(storage);

                            Methods.PauseAndExit();
                            break;
                        }
                    case 5:
                        {
                            Console.Clear();
                            Console.WriteLine("====== Save File ======");

                            SavingInFile.SaveInFile(storage, filePath);
                            Console.WriteLine("Saved to: " + filePath);

                            Methods.PauseAndExit();
                            break;
                        }

                    case 6:
                        {
                            Console.Clear();
                            Console.WriteLine("====== Load File ======");

                            storage = SavingInFile.LoadStorage(filePath);
                            

                            storage = SavingInFile.LoadStorage(filePath);

                            Methods.PrintAll(storage);
                            Methods.PauseAndExit();
                            break;
                        }
                    case 0:
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again!");
                        continue;
                }
            }
        }
    }
}