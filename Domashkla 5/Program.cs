using System;
using System.Collections.Generic;

namespace SimpleAutoPark
{
    class Transport
    {
        public string Model;
        public int Year;
        public string Fuel;
        public string Body;

        public void ShowInfo()
        {
            Console.WriteLine("Модель: " + Model);
            Console.WriteLine("Год выпуска: " + Year);
            Console.WriteLine("Тип топлива: " + Fuel);
            Console.WriteLine("Тип кузова: " + Body);
        }
    }

    class Car : Transport
    {
        public int Doors;

        public new void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine("Количество дверей: " + Doors);
        }
    }

    class Bus : Transport
    {
        public int Seats;

        public void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine("Пассажирских мест: " + Seats);
        }
    }

    class AutoParkManager
    {
        List<Transport> park = new List<Transport>();

        public void Run()
        {
            bool work = true;

            while (work)
            {
                Console.WriteLine();
                Console.WriteLine("Меню:");
                Console.WriteLine("1 - Добавить транспорт");
                Console.WriteLine("2 - Показать все");
                Console.WriteLine("3 - Редактировать транспорт");
                Console.WriteLine("4 - Удалить транспорт");
                Console.WriteLine("0 - Выход");
                Console.Write("Выбор: ");

                string answer = Console.ReadLine();

                if (answer == "1")
                {
                    AddTransport();
                }
                else if (answer == "2")
                {
                    ShowAll();
                }
                else if (answer == "3")
                {
                    EditTransport();
                }
                else if (answer == "4")
                {
                    DeleteTransport();
                }
                else if (answer == "0")
                {
                    work = false;
                }
                else
                {
                    Console.WriteLine("Неверный выбор");
                }
            }
        }

        void AddTransport()
        {
            Console.Write("Введите тип транспорта (car или bus): ");
            string type = Console.ReadLine();

            Console.Write("Введите модель: ");
            string model = Console.ReadLine();

            Console.Write("Введите год выпуска: ");
            int year = Convert.ToInt32(Console.ReadLine());

            Console.Write("Введите тип топлива: ");
            string fuel = Console.ReadLine();

            Console.Write("Введите тип кузова: ");
            string body = Console.ReadLine();

            if (type == "car")
            {
                Console.Write("Сколько дверей: ");
                int doors = Convert.ToInt32(Console.ReadLine());

                Car car = new Car();
                car.Model = model;
                car.Year = year;
                car.Fuel = fuel;
                car.Body = body;
                car.Doors = doors;

                park.Add(car);
            }
            else if (type == "bus")
            {
                Console.Write("Сколько мест: ");
                int seats = Convert.ToInt32(Console.ReadLine());

                Bus bus = new Bus();
                bus.Model = model;
                bus.Year = year;
                bus.Fuel = fuel;
                bus.Body = body;
                bus.Seats = seats;

                park.Add(bus);
            }
            else
            {
                Console.WriteLine("Неизвестный тип транспорта");
            }

            Console.WriteLine("Транспорт добавлен.");
        }

        void ShowAll()
        {
            if (park.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < park.Count; i++)
            {
                Console.WriteLine();
                Console.WriteLine("Транспорт №" + i);
                var item = park[i];

                if (item is Car)
                {
                    Car car = (Car)item;
                    car.ShowInfo();
                }
                else if (item is Bus)
                {
                    Bus bus = (Bus)item;
                    bus.ShowInfo();
                }
                else
                {
                    item.ShowInfo();
                }
            }
        }

        void EditTransport()
        {
            ShowAll();

            Console.Write("Введите номер транспорта для редактирования: ");
            int index = Convert.ToInt32(Console.ReadLine());

            if (index >= 0 && index < park.Count)
            {
                park.RemoveAt(index);
                Console.WriteLine("Введите новые данные для транспорта:");
                AddTransport();
            }
            else
            {
                Console.WriteLine("Неверный номер.");
            }
        }

        void DeleteTransport()
        {
            ShowAll();

            Console.Write("Введите номер транспорта для удаления: ");
            int index = Convert.ToInt32(Console.ReadLine());

            if (index >= 0 && index < park.Count)
            {
                park.RemoveAt(index);
                Console.WriteLine("Удалено.");
            }
            else
            {
                Console.WriteLine("Неверный номер.");
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {
            AutoParkManager manager = new AutoParkManager();
            manager.Run();
        }
    }
}
