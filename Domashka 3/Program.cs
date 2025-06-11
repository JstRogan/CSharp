using System;

namespace AutoParkManager
{
    enum FuelType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }

    enum BodyType
    {
        Sedan,
        Hatchback,
        SUV,
        Minivan,
        Bus
    }

    class Transport
    {
        public string Model { get; set; }
        public int Year { get; set; }
        public FuelType Fuel { get; set; }
        public BodyType Body { get; set; }

        public virtual void PrintInfo()
        {
            Console.WriteLine($"Модель: {Model}, Год: {Year}, Топливо: {Fuel}, Кузов: {Body}");
        }
    }

    class Car : Transport
    {
        public int NumberOfDoors { get; set; }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Количество дверей: {NumberOfDoors}");
        }
    }

    class Bus : Transport
    {
        public int PassengerSeats { get; set; }

        public override void PrintInfo()
        {
            base.PrintInfo();
            Console.WriteLine($"Пассажирских мест: {PassengerSeats}");
        }
    }

    class Program
    {
        static Transport[] park = new Transport[100];
        static int count = 0;

        static void Main(string[] args)
        {
            bool exit = false;
            while (!exit)
            {
                Console.WriteLine("\nМенеджер автопарка:");
                Console.WriteLine("1. Добавить транспорт");
                Console.WriteLine("2. Показать автопарк");
                Console.WriteLine("3. Редактировать транспорт");
                Console.WriteLine("4. Удалить транспорт");
                Console.WriteLine("0. Выход");
                Console.Write("Выбор: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        AddTransport();
                        break;
                    case "2":
                        ShowPark();
                        break;
                    case "3":
                        EditTransport();
                        break;
                    case "4":
                        DeleteTransport();
                        break;
                    case "0":
                        exit = true;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор.");
                        break;
                }
            }
        }

        static void AddTransport()
        {
            Console.Write("Введите тип (car/bus): ");
            string type = Console.ReadLine().ToLower();

            Console.Write("Модель: ");
            string model = Console.ReadLine();

            Console.Write("Год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Топливо (Petrol, Diesel, Electric, Hybrid): ");
            FuelType fuel = Enum.Parse<FuelType>(Console.ReadLine());

            Console.Write("Кузов (Sedan, Hatchback, SUV, Minivan, Bus): ");
            BodyType body = Enum.Parse<BodyType>(Console.ReadLine());

            if (type == "car")
            {
                Console.Write("Количество дверей: ");
                int doors = int.Parse(Console.ReadLine());

                Car car = new Car
                {
                    Model = model,
                    Year = year,
                    Fuel = fuel,
                    Body = body,
                    NumberOfDoors = doors
                };
                park[count++] = car;
            }
            else if (type == "bus")
            {
                Console.Write("Пассажирских мест: ");
                int seats = int.Parse(Console.ReadLine());

                Bus bus = new Bus
                {
                    Model = model,
                    Year = year,
                    Fuel = fuel,
                    Body = body,
                    PassengerSeats = seats
                };
                park[count++] = bus;
            }
            else
            {
                Console.WriteLine("Неизвестный тип транспорта.");
            }

            Console.WriteLine("Транспорт добавлен.");
        }

        static void ShowPark()
        {
            if (count == 0)
            {
                Console.WriteLine("Автопарк пуст.");
                return;
            }

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\n[{i}]");
                park[i].PrintInfo();
            }
        }

        static void EditTransport()
        {
            ShowPark();
            Console.Write("Введите номер транспорта для редактирования: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < count)
            {
                AddTransportAtIndex(index);
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }

        static void AddTransportAtIndex(int index)
        {
            Console.Write("Введите тип (car/bus): ");
            string type = Console.ReadLine().ToLower();

            Console.Write("Модель: ");
            string model = Console.ReadLine();

            Console.Write("Год выпуска: ");
            int year = int.Parse(Console.ReadLine());

            Console.Write("Топливо (Petrol, Diesel, Electric, Hybrid): ");
            FuelType fuel = Enum.Parse<FuelType>(Console.ReadLine());

            Console.Write("Кузов (Sedan, Hatchback, SUV, Minivan, Bus): ");
            BodyType body = Enum.Parse<BodyType>(Console.ReadLine());

            if (type == "car")
            {
                Console.Write("Количество дверей: ");
                int doors = int.Parse(Console.ReadLine());

                Car car = new Car
                {
                    Model = model,
                    Year = year,
                    Fuel = fuel,
                    Body = body,
                    NumberOfDoors = doors
                };
                park[index] = car;
            }
            else if (type == "bus")
            {
                Console.Write("Пассажирских мест: ");
                int seats = int.Parse(Console.ReadLine());

                Bus bus = new Bus
                {
                    Model = model,
                    Year = year,
                    Fuel = fuel,
                    Body = body,
                    PassengerSeats = seats
                };
                park[index] = bus;
            }
            else
            {
                Console.WriteLine("Неизвестный тип транспорта.");
            }

            Console.WriteLine("Транспорт обновлён.");
        }

        static void DeleteTransport()
        {
            ShowPark();
            Console.Write("Введите номер транспорта для удаления: ");
            if (int.TryParse(Console.ReadLine(), out int index) && index >= 0 && index < count)
            {
                for (int i = index; i < count - 1; i++)
                {
                    park[i] = park[i + 1];
                }
                count--;
                Console.WriteLine("Удалено.");
            }
            else
            {
                Console.WriteLine("Неверный индекс.");
            }
        }
    }
}
