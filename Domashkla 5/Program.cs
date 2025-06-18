using System;
using System.Collections.Generic;

namespace LibraryApp
{
    class Book
    {
        public string Title;
        public string Author;
        public int Year;

        public Book(string title, string author, int year)
        {
            Title = title;
            Author = author;
            Year = year;
        }

        public override string ToString()
        {
            return "\"" + Title + "\" - " + Author + " (" + Year + ")";
        }
    }

    class Program
    {
        static List<Book> library = new List<Book>();

        static void Main(string[] args)
        {
            Console.WriteLine("Привет! Это мини-библиотека");

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Показать все книги");
                Console.WriteLine("4. Выход");

                Console.Write("Ваш выбор: ");
                string action = Console.ReadLine();

                if (action == "1")
                {
                    AddBook();
                }
                else if (action == "2")
                {
                    DeleteBook();
                }
                else if (action == "3")
                {
                    ShowBooks();
                }
                else if (action == "4")
                {
                    isRunning = false;
                    Console.WriteLine("Выход из программы...");
                }
                else
                {
                    Console.WriteLine("Такого варианта нет. Попробуйте снова.");
                }
            }
        }

        static void AddBook()
        {
            Console.Write("Название книги: ");
            string name = Console.ReadLine();

            Console.Write("Автор: ");
            string writer = Console.ReadLine();

            Console.Write("Год выпуска: ");
            string yearText = Console.ReadLine();

            int year;
            if (int.TryParse(yearText, out year))
            {
                Book b = new Book(name, writer, year);
                library.Add(b);
                Console.WriteLine("Книга добавлена!");
            }
            else
            {
                Console.WriteLine("Год введён неправильно.");
            }
        }

        static void DeleteBook()
        {
            if (library.Count == 0)
            {
                Console.WriteLine("Нет книг для удаления.");
                return;
            }

            ShowBooks();

            Console.Write("Введите номер книги, которую хотите удалить: ");
            string indexText = Console.ReadLine();

            int index;
            if (int.TryParse(indexText, out index))
            {
                index = index - 1;

                if (index >= 0 && index < library.Count)
                {
                    library.RemoveAt(index);
                    Console.WriteLine("Книга удалена.");
                }
                else
                {
                    Console.WriteLine("Такой книги нет.");
                }
            }
            else
            {
                Console.WriteLine("Введите число.");
            }
        }

        static void ShowBooks()
        {
            if (library.Count == 0)
            {
                Console.WriteLine("Книг пока нет.");
                return;
            }

            Console.WriteLine("Список книг:");

            int num = 1;
            foreach (var item in library)
            {
                string text = num.ToString() + ". " + item; // простое склеивание
                Console.WriteLine(text);
                num = num + 1;
            }
        }
    }
}
