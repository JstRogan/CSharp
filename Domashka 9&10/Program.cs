using System;
using System.Collections.Generic;
using System.IO;

namespace CalculatorApp
{
    public interface ICalculatorOperation
    {
        string Name { get; }
        double Execute(double a, double b);
    }

    public class Addition : ICalculatorOperation
    {
        public string Name => "Сложение";
        public double Execute(double a, double b) => a + b;
    }

    public class Subtraction : ICalculatorOperation
    {
        public string Name => "Вычитание";
        public double Execute(double a, double b) => a - b;
    }

    public class Multiplication : ICalculatorOperation
    {
        public string Name => "Умножение";
        public double Execute(double a, double b) => a * b;
    }

    public class Division : ICalculatorOperation
    {
        public string Name => "Деление";
        public double Execute(double a, double b)
        {
            if (b == 0) throw new DivideByZeroException("Деление на ноль невозможно.");
            return a / b;
        }
    }

    public class CustomOperation : ICalculatorOperation
    {
        public string Name { get; }
        private Func<double, double, double> _func;

        public CustomOperation(string name, Func<double, double, double> func)
        {
            Name = name;
            _func = func;
        }

        public double Execute(double a, double b) => _func(a, b);
    }

    class Program
    {
        static List<ICalculatorOperation> operations = new List<ICalculatorOperation>
        {
            new Addition(),
            new Subtraction(),
            new Multiplication(),
            new Division()
        };

        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n=== КАЛЬКУЛЯТОР ===");
                for (int i = 0; i < operations.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {operations[i].Name}");
                }
                Console.WriteLine($"{operations.Count + 1}. Добавить свою операцию");
                Console.WriteLine("0. Выход");
                Console.Write("Выберите действие: ");

                string choice = Console.ReadLine();

                if (choice == "0") break;

                try
                {
                    if (choice == (operations.Count + 1).ToString())
                    {
                        AddNewOperation();
                        continue;
                    }

                    int opIndex = int.Parse(choice) - 1;
                    if (opIndex < 0 || opIndex >= operations.Count)
                    {
                        Console.WriteLine("Неверный выбор операции.");
                        continue;
                    }

                    var op = operations[opIndex];

                    Console.Write("Введите первое число: ");
                    double a = double.Parse(Console.ReadLine());

                    Console.Write("Введите второе число: ");
                    double b = double.Parse(Console.ReadLine());

                    double result = op.Execute(a, b);
                    Console.WriteLine($"Результат: {result}");
                }
                catch (DivideByZeroException ex)
                {
                    HandleError("Ошибка математики: " + ex.Message);
                }
                catch (FormatException)
                {
                    HandleError("Ошибка ввода: введите корректное число.");
                }
                catch (Exception ex)
                {
                    HandleError("Ошибка: " + ex.Message);
                }
            }
        }

        static void AddNewOperation()
        {
            try
            {
                Console.Write("Введите название операции: ");
                string name = Console.ReadLine();

                Console.WriteLine("Выберите тип операции:");
                Console.WriteLine("1. a ^ b (Степень)");
                Console.WriteLine("2. (a + b) / 2 (Среднее)");
                Console.WriteLine("3. a % b (Остаток)");
                string type = Console.ReadLine();

                Func<double, double, double> func;

                if (type == "1") func = Math.Pow;
                else if (type == "2") func = (a, b) => (a + b) / 2;
                else if (type == "3") func = (a, b) => a % b;
                else
                {
                    Console.WriteLine("Неверный тип.");
                    return;
                }

                operations.Add(new CustomOperation(name, func));
                Console.WriteLine("Операция добавлена.");
            }
            catch (Exception ex)
            {
                HandleError("Не удалось добавить операцию: " + ex.Message);
            }
        }

        static void HandleError(string message)
        {
            Console.WriteLine(message);
            File.AppendAllText("errors.log", $"{DateTime.Now}: {message}\n");
        }
    }
}
