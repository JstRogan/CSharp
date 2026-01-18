using System;
using System.Linq;

namespace Domashka2
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите задание (1-9) или 0 для выхода:");
                string choice = Console.ReadLine();

                if (choice == "0") break;
                else if (choice == "1") Task1();
                else if (choice == "2") Task2();
                else if (choice == "3") Task3();
                else if (choice == "4") Task4();
                else if (choice == "5") Task5();
                else if (choice == "6") Task6();
                else if (choice == "7") Task7();
                else if (choice == "8") Task8();
                else if (choice == "9") Task9();
                else Console.WriteLine("Неверный выбор");
            }
        }

        static void Task1()
        {
            int[] arr = { 1, 2, 3, 4, 5, 2, 3, 8, 10, 11 };
            Console.WriteLine("Массив: " + string.Join(", ", arr));
            
            int even = 0;
            int odd = 0;
            int unique = arr.Distinct().Count();

            foreach (var item in arr)
            {
                if (item % 2 == 0) even++;
                else odd++;
            }

            Console.WriteLine("Четных: " + even);
            Console.WriteLine("Нечетных: " + odd);
            Console.WriteLine("Уникальных: " + unique);
        }

        static void Task2()
        {
            int[] arr = { 5, 8, 2, 9, 1, 10, 4, 7 };
            Console.WriteLine("Массив: " + string.Join(", ", arr));

            Console.Write("Введите значение: ");
            int limit = Convert.ToInt32(Console.ReadLine());
            
            int count = 0;
            foreach (var item in arr)
            {
                if (item < limit) count++;
            }

            Console.WriteLine("Количество меньше " + limit + ": " + count);
        }

        static void Task3()
        {
            int[] arr = { 7, 6, 5, 3, 4, 7, 6, 5, 8, 7, 6, 5 };
            Console.WriteLine("Массив: " + string.Join(" ", arr));

            Console.Write("Введите 3 числа через пробел: ");
            var parts = Console.ReadLine().Split(' ');
            int n1 = Convert.ToInt32(parts[0]);
            int n2 = Convert.ToInt32(parts[1]);
            int n3 = Convert.ToInt32(parts[2]);

            int count = 0;
            for (int i = 0; i < arr.Length - 2; i++)
            {
                if (arr[i] == n1 && arr[i+1] == n2 && arr[i+2] == n3)
                {
                    count++;
                }
            }

            Console.WriteLine("Количество повторений: " + count);
        }

        static void Task4()
        {
            int[] arr1 = { 1, 2, 3, 4, 5 };
            int[] arr2 = { 3, 4, 5, 6, 7 };

            Console.WriteLine("Массив 1: " + string.Join(", ", arr1));
            Console.WriteLine("Массив 2: " + string.Join(", ", arr2));

            var common = arr1.Intersect(arr2).ToArray();
            
            Console.WriteLine("Общие элементы без повторений: " + string.Join(", ", common));
        }

        static void Task5()
        {
            int[,] arr = {
                { 1, 5, 3 },
                { 8, 2, 9 }
            };

            int min = arr[0, 0];
            int max = arr[0, 0];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (arr[i, j] < min) min = arr[i, j];
                    if (arr[i, j] > max) max = arr[i, j];
                    Console.Write(arr[i, j] + " ");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nМин: " + min);
            Console.WriteLine("Макс: " + max);
        }

        static void Task6()
        {
            Console.Write("Введите предложение: ");
            string text = Console.ReadLine();
            
            var words = text.Split(new char[] { ' ', '.', ',' }, StringSplitOptions.RemoveEmptyEntries);
            Console.WriteLine("Количество слов: " + words.Length);
        }

        static void Task7()
        {
            Console.Write("Введите предложение: ");
            string text = Console.ReadLine();
            
            var words = text.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                var chars = words[i].ToCharArray();
                Array.Reverse(chars);
                words[i] = new string(chars);
            }

            Console.WriteLine(string.Join(" ", words));
        }

        static void Task8()
        {
            Console.Write("Введите предложение: ");
            string text = Console.ReadLine();
            
            string vowels = "aeiouyAEIOUY";
            int count = 0;

            foreach (var c in text)
            {
                if (vowels.Contains(c)) count++;
            }

            Console.WriteLine("Количество гласных: " + count);
        }

        static void Task9()
        {
            Console.Write("Введите строку: ");
            string text = Console.ReadLine();
            Console.Write("Введите слово для поиска: ");
            string sub = Console.ReadLine();

            int count = 0;
            int index = 0;

            while ((index = text.IndexOf(sub, index)) != -1)
            {
                count++;
                index += sub.Length;
            }

            Console.WriteLine("Результат поиска: " + count);
        }
    }
}
