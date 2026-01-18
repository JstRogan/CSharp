using System;

namespace Domashka1
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\nВыберите задание (1-7) или 0 для выхода:");
                string choice = Console.ReadLine();

                if (choice == "0") break;
                else if (choice == "1") Task1();
                else if (choice == "2") Task2();
                else if (choice == "3") Task3();
                else if (choice == "4") Task4();
                else if (choice == "5") Task5();
                else if (choice == "6") Task6();
                else if (choice == "7") Task7();
                else Console.WriteLine("Неверный выбор");
            }
        }

        static void Task1()
        {
            Console.Write("Введите число от 1 до 100: ");
            int num = Convert.ToInt32(Console.ReadLine());

            if (num < 1 || num > 100)
            {
                Console.WriteLine("Ошибка: Введите число в диапазоне от 1 до 100");
                return;
            }

            if (num % 3 == 0 && num % 5 == 0) Console.WriteLine("Fizz Buzz");
            else if (num % 3 == 0) Console.WriteLine("Fizz");
            else if (num % 5 == 0) Console.WriteLine("Buzz");
            else Console.WriteLine(num);
        }

        static void Task2()
        {
            Console.Write("Введите число: ");
            double num = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введите процент: ");
            double percent = Convert.ToDouble(Console.ReadLine());

            double result = num * percent / 100;
            Console.WriteLine(result);
        }

        static void Task3()
        {
            Console.Write("Введите первую цифру: ");
            string d1 = Console.ReadLine();
            Console.Write("Введите вторую цифру: ");
            string d2 = Console.ReadLine();
            Console.Write("Введите третью цифру: ");
            string d3 = Console.ReadLine();
            Console.Write("Введите четвертую цифру: ");
            string d4 = Console.ReadLine();

            string numberStr = d1 + d2 + d3 + d4;
            int number = Convert.ToInt32(numberStr);
            Console.WriteLine(number);
        }

        static void Task4()
        {
             Console.Write("Введите шестизначное число: ");
             string input = Console.ReadLine();

             if (input.Length != 6)
             {
                 Console.WriteLine("Ошибка: введите шестизначное число");
                 return;
             }

             Console.Write("Введите первый номер разряда для обмена: ");
             int pos1 = Convert.ToInt32(Console.ReadLine()) - 1;
             Console.Write("Введите второй номер разряда для обмена: ");
             int pos2 = Convert.ToInt32(Console.ReadLine()) - 1;

             char[] digits = input.ToCharArray();
             char temp = digits[pos1];
             digits[pos1] = digits[pos2];
             digits[pos2] = temp;

             string resultStr = new string(digits);
             int result = Convert.ToInt32(resultStr);
             Console.WriteLine(result);
        }

        static void Task5()
        {
            Console.Write("Введите дату (дд.мм.гггг): ");
            DateTime date = DateTime.Parse(Console.ReadLine());
            
            string season = "";
            if (date.Month == 12 || date.Month == 1 || date.Month == 2) season = "Winter";
            else if (date.Month >= 3 && date.Month <= 5) season = "Spring";
            else if (date.Month >= 6 && date.Month <= 8) season = "Summer";
            else season = "Autumn";

            Console.WriteLine(season + " " + date.DayOfWeek);
        }

        static void Task6()
        {
            Console.WriteLine("Выберите перевод (1: F->C, 2: C->F):");
            string choice = Console.ReadLine();

            Console.Write("Введите температуру: ");
            double temp = Convert.ToDouble(Console.ReadLine());

            if (choice == "1")
            {
                double c = (temp - 32) * 5 / 9;
                Console.WriteLine(c);
            }
            else if (choice == "2")
            {
                double f = temp * 9 / 5 + 32;
                Console.WriteLine(f);
            }
            else
            {
                Console.WriteLine("Ошибка выбора");
            }
        }

        static void Task7()
        {
            Console.Write("Введите начало диапазона: ");
            int start = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите конец диапазона: ");
            int end = Convert.ToInt32(Console.ReadLine());

            if (start > end)
            {
                int temp = start;
                start = end;
                end = temp;
            }

            for (int i = start; i <= end; i++)
            {
                if (i % 2 == 0)
                {
                    Console.WriteLine(i);
                }
            }
        }
    }
}
