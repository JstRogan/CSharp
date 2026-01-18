using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TransportPark
{
    [JsonDerivedType(typeof(Car), typeDiscriminator: "Car")]
    [JsonDerivedType(typeof(Truck), typeDiscriminator: "Truck")]
    [JsonDerivedType(typeof(Bike), typeDiscriminator: "Bike")]
    [JsonDerivedType(typeof(Bus), typeDiscriminator: "Bus")]
    public class Transport
    {
        public string Type { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public int MaxSpeed { get; set; }

        public Transport() { }

        public virtual void ShowInfo()
        {
            Console.WriteLine($"Тип: {Type}, Марка: {Brand}, Модель: {Model}, Год: {Year}, Макс. скорость: {MaxSpeed} км/ч");
        }

        public virtual void Move()
        {
            Console.WriteLine("Транспорт начинает движение.");
        }
    }

    public class Car : Transport
    {
        public string FuelType { get; set; }

        public Car() { Type = "Car"; }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Тип топлива: {FuelType}");
            Console.WriteLine($"Расход топлива: {CalculateConsumption()} л/100км");
        }

        public override void Move()
        {
            Console.WriteLine($"Автомобиль {Brand} {Model} едет по дороге со скоростью до {MaxSpeed} км/ч.");
        }

        public double CalculateConsumption()
        {
            return MaxSpeed * 0.15;
        }
    }

    public class Truck : Transport
    {
        public double LoadCapacity { get; set; }

        public Truck() { Type = "Truck"; }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Грузоподъемность: {LoadCapacity} т");
            CheckWeight();
        }

        public override void Move()
        {
            Console.WriteLine($"Грузовик {Brand} {Model} перевозит груз.");
        }

        public void CheckWeight()
        {
            if (LoadCapacity > 20) Console.WriteLine("Внимание: Тяжелый грузовик!");
            else Console.WriteLine("Легкий грузовик.");
        }
    }

    public class Bike : Transport
    {
        public bool HasSidecar { get; set; }

        public Bike() { Type = "Bike"; }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Коляска: {(HasSidecar ? "Да" : "Нет")}");
        }

        public override void Move()
        {
            Console.WriteLine($"Мотоцикл {Brand} {Model} мчится по дороге.");
        }
    }

    public class Bus : Transport
    {
        public int PassengerCapacity { get; set; }

        public Bus() { Type = "Bus"; }

        public override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine($"Мест для пассажиров: {PassengerCapacity}");
            Console.WriteLine($"Потенциальный доход за рейс: {CalculateIncome(50)} руб");
        }

        public override void Move()
        {
            Console.WriteLine($"Автобус {Brand} {Model} перевозит пассажиров.");
        }

        public int CalculateIncome(int ticketPrice)
        {
            return PassengerCapacity * ticketPrice;
        }
    }

    class Program
    {
        static List<Transport> text = new List<Transport>();
        static string filePath = "transport_data.json";

        static void Main(string[] args)
        {
            bool work = true;
            while (work)
            {
                Console.WriteLine("\n=== МЕНЮ ===");
                Console.WriteLine("1. Добавить транспорт");
                Console.WriteLine("2. Показать все");
                Console.WriteLine("3. Запустить транспорт");
                Console.WriteLine("4. Удалить транспорт");
                Console.WriteLine("5. Фильтр по типу");
                Console.WriteLine("6. Сохранить в файл");
                Console.WriteLine("7. Загрузить из файла");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string choice = Console.ReadLine();

                if (choice == "1") AddTransport();
                else if (choice == "2") ShowAll();
                else if (choice == "3") StartTransport();
                else if (choice == "4") DeleteTransport();
                else if (choice == "5") FilterTransport();
                else if (choice == "6") SaveToFile();
                else if (choice == "7") LoadFromFile();
                else if (choice == "0") work = false;
                else Console.WriteLine("Неверный выбор");
            }
        }

        static void AddTransport()
        {
            Console.WriteLine("Выберите тип (1-Car, 2-Truck, 3-Bike, 4-Bus):");
            string type = Console.ReadLine();

            Transport t = null;

            if (type == "1") t = new Car();
            else if (type == "2") t = new Truck();
            else if (type == "3") t = new Bike();
            else if (type == "4") t = new Bus();
            else
            {
                Console.WriteLine("Неверный тип");
                return;
            }

            Console.Write("Марка: ");
            t.Brand = Console.ReadLine();
            Console.Write("Модель: ");
            t.Model = Console.ReadLine();
            Console.Write("Год: ");
            t.Year = int.Parse(Console.ReadLine());
            Console.Write("Макс. скорость: ");
            t.MaxSpeed = int.Parse(Console.ReadLine());

            if (t is Car c)
            {
                Console.Write("Тип топлива: ");
                c.FuelType = Console.ReadLine();
            }
            else if (t is Truck tr)
            {
                Console.Write("Грузоподъемность (т): ");
                tr.LoadCapacity = double.Parse(Console.ReadLine());
            }
            else if (t is Bike b)
            {
                Console.Write("Есть коляска (true/false): ");
                b.HasSidecar = bool.Parse(Console.ReadLine());
            }
            else if (t is Bus bu)
            {
                Console.Write("Количество пассажиров: ");
                bu.PassengerCapacity = int.Parse(Console.ReadLine());
            }

            text.Add(t);
            Console.WriteLine("Добавлено.");
        }

        static void ShowAll()
        {
            if (text.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }
            for (int i = 0; i < text.Count; i++)
            {
                Console.WriteLine($"\n#{i + 1}");
                text[i].ShowInfo();
            }
        }

        static void StartTransport()
        {
            ShowAll();
            Console.Write("Введите номер для запуска: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < text.Count)
            {
                text[index].Move();
            }
            else
            {
                Console.WriteLine("Неверный номер");
            }
        }

        static void DeleteTransport()
        {
            ShowAll();
            Console.Write("Введите номер для удаления: ");
            int index = int.Parse(Console.ReadLine()) - 1;

            if (index >= 0 && index < text.Count)
            {
                text.RemoveAt(index);
                Console.WriteLine("Удалено.");
            }
        }

        static void FilterTransport()
        {
            Console.WriteLine("Введите тип для фильтра (Car, Truck, Bike, Bus):");
            string type = Console.ReadLine();

            foreach (var t in text)
            {
                if (t.Type.Equals(type, StringComparison.OrdinalIgnoreCase))
                {
                    t.ShowInfo();
                    Console.WriteLine("---");
                }
            }
        }

        static void SaveToFile()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(text, options);
            File.WriteAllText(filePath, json);
            Console.WriteLine("Сохранено в " + filePath);
        }

        static void LoadFromFile()
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                try
                {
                    var loaded = JsonSerializer.Deserialize<List<Transport>>(json);
                    if (loaded != null)
                    {
                        text = loaded;
                        Console.WriteLine("Загружено " + text.Count + " объектов.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка загрузки: " + ex.Message);
                }
            }
            else
            {
                Console.WriteLine("Файл не найден.");
            }
        }
    }
}
