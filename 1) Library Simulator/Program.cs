using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace OOP_Test
{
    internal class Program
    {
        public class Book
        {
            private string _title;
            private string _author;
            private int _year;
            private bool _isRead;

            public string Title
            {
                get { return _title; }
                private set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException("Title cannot be empty!");
                    _title = value;
                }
            }

            public string Author
            {
                get { return _author; }
                private set
                {
                    if (string.IsNullOrWhiteSpace(value))
                        throw new ArgumentNullException("Author cannot be empty!");
                    _author = value;
                }
            }

            public int Year
            {
                get { return _year; }
                private set
                {
                    if (value < 0 || value > 2100)
                        throw new ArgumentException("Year must be between 0 and 2100!");
                    _year = value;
                }
            }

            public bool IsRead { get { return _isRead; } }

            public Book(string title, string author, int year)
            {
                Title = title;
                Author = author;
                Year = year;
                _isRead = false;
            }

            public void MarkAsRead() => _isRead = true;
            public void MarkAsUnread() => _isRead = false;

            public override string ToString()
            {   
                string status = _isRead ? "[x]" : "[ ]";
                return $"{status} {Year,-4} - {Title,-15} | Author: {Author,-15}";
            }
        }

        public class Library
        {
            private readonly List<Book> _books = new List<Book>();
            public IReadOnlyList<Book> Books => _books;

            public void AddBook(Book book)
            {
                if (book is null) throw new ArgumentNullException("Book is null!");
                _books.Add(book);
            }

            public void Clear() => _books.Clear();

            public void RemoveBookByIndex(int index)
            {
                if (index < 1 || index > _books.Count)
                {
                    Console.WriteLine("Index is out of range!");
                    return;
                }

                _books.RemoveAt(index - 1);
                Console.WriteLine("Book removed.");
            }

            public Book GetBookByIndex(int index)
            {
                if (index < 1 || index > _books.Count)
                    return null;

                return _books[index - 1];
            }

            public void PrintAll()
            {
                if (_books.Count == 0)
                {
                    Console.WriteLine("Library is empty.");
                    return;
                }

                for (int i = 0; i < _books.Count; i++)
                    Console.WriteLine($"{i + 1}) {_books[i]}");
            }
        }

        // ======== TXT SAVE / LOAD (one book = one line) ========

        static void SaveToTxt(Library library, string filePath)
        {
            var lines = library.Books.Select(b =>
                $"{b.Title,-10}|{b.Author,-10}|{b.Year,-4}|{b.IsRead}"
            );

            File.WriteAllLines(filePath, lines);
        }

        static void LoadFromTxt(Library library, string filePath)
        {
            if (!File.Exists(filePath))
                return;

            library.Clear();

            foreach (var line in File.ReadAllLines(filePath))
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length != 4)
                    continue;

                string title = parts[0];
                string author = parts[1];

                if (!int.TryParse(parts[2], out int year))
                    continue;

                if (!bool.TryParse(parts[3], out bool isRead))
                    continue;

                Book b;
                try
                {
                    b = new Book(title, author, year);
                }
                catch
                {
                    continue;
                }

                if (isRead) b.MarkAsRead();
                else b.MarkAsUnread();

                library.AddBook(b);
            }
        }

        // ======== MAIN ========

        static void Main(string[] args)
        {
            string projectRootPath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\.."));
            string dataDirectoryPath = Path.Combine(projectRootPath, "Data");
            Directory.CreateDirectory(dataDirectoryPath);

            string saveFilePath = Path.Combine(dataDirectoryPath, "Saved.txt");

            Library library = new Library();
            Book current = null;

            // Auto-load at start
            LoadFromTxt(library, saveFilePath);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("====== Library box ======");
                Console.WriteLine($"Current book: {(current == null ? "(none)" : current.ToString())}");
                Console.WriteLine(new string('-', 26));

                Console.WriteLine(
                    "1) Add new book\n" +
                    "2) Show all\n" +
                    "3) Remove a book by index\n" +
                    "4) Set current book by index\n" +
                    "5) Mark Unread (by index)\n" +
                    "6) Mark Read (by index)\n" +
                    "7) Save to file\n" +
                    "8) Load from file\n" +
                    "0) Exit"
                );

                int choice = ReadInt("Choice: ");
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("====== Add new book ======");

                        string title = ReadString("Enter Title: ");
                        string author = ReadString("Enter Author's name: ");
                        int year = ReadInt("Enter year of production: ");

                        try
                        {
                            Book book = new Book(title, author, year);
                            library.AddBook(book);
                            Console.WriteLine("Book added <3");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Error: {ex.Message}");
                        }

                        Console.ReadKey();
                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("====== All books ======");
                        library.PrintAll();
                        Console.ReadKey();
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("====== Remove by index ======");
                        library.PrintAll();
                        {
                            int index = ReadInt("Enter an index: ");
                            library.RemoveBookByIndex(index);

                            // If current was removed, reset it (simple check)
                            if (current != null && !library.Books.Contains(current))
                                current = null;
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("====== Set current by index ======");
                        library.PrintAll();
                        {
                            int index = ReadInt("Enter an index: ");
                            current = library.GetBookByIndex(index);

                            if (current == null)
                                Console.WriteLine("No such book!");
                            else
                                Console.WriteLine($"Current set: {current}");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;

                    case 5:
                        Console.Clear();
                        Console.WriteLine("====== Mark Unread ======");
                        library.PrintAll();
                        {
                            int index = ReadInt("Enter an index: ");
                            var b = library.GetBookByIndex(index);
                            if (b == null)
                            {
                                Console.WriteLine("No such book!");
                                Console.ReadKey();
                                break;
                            }

                            b.MarkAsUnread();
                            current = b;
                            Console.WriteLine("Marked as unread.");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;

                    case 6:
                        Console.Clear();
                        Console.WriteLine("====== Mark Read ======");
                        library.PrintAll();
                        {
                            int index = ReadInt("Enter an index: ");
                            var b = library.GetBookByIndex(index);
                            if (b == null)
                            {
                                Console.WriteLine("No such book!");
                                Console.ReadKey();
                                break;
                            }

                            b.MarkAsRead();
                            current = b;
                            Console.WriteLine("Marked as read.");
                        }
                        Console.WriteLine("Press any key...");
                        Console.ReadKey();
                        break;

                    case 7:
                        Console.Clear();
                        Console.WriteLine("====== Save to file ======");
                        SaveToTxt(library, saveFilePath);
                        Console.WriteLine("Saved!");
                        Console.WriteLine($"Path: {saveFilePath}");
                        Console.ReadKey();
                        break;

                    case 8:
                        Console.Clear();
                        Console.WriteLine("====== Load from file ======");
                        LoadFromTxt(library, saveFilePath);
                        current = null;
                        Console.WriteLine("Loaded!");
                        Console.WriteLine($"Path: {saveFilePath}");
                        Console.ReadKey();
                        break;

                    case 0:
                        // Optional: auto-save on exit
                        SaveToTxt(library, saveFilePath);
                        return;

                    default:
                        Console.WriteLine("Invalid option! Try again!");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static int ReadInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                string number = Console.ReadLine();

                if (int.TryParse(number, out int value))
                    return value;

                Console.WriteLine("Enter a number please!");
            }
        }

        static string ReadString(string text)
        {
            while (true)
            {
                Console.Write(text);
                string input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Enter correct string!");
                    continue;
                }

                // IMPORTANT: because file format uses '|'
                if (input.Contains("|"))
                {
                    Console.WriteLine("Symbol '|' is not allowed. Try again.");
                    continue;
                }

                return input;
            }
        }
    }
}
