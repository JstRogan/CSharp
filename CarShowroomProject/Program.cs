using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace CarShowroomProject
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Make { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public DateTime Year { get; set; }
    }

    public class Showroom
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = string.Empty;
        public List<Car> Cars { get; set; } = new List<Car>();
        public int CarCapacity { get; set; }
        public int CarCount => Cars.Count;
        public int SalesCount { get; set; }
    }

    public class User
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ShowroomId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public class Sale
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ShowroomId { get; set; }
        public Guid CarId { get; set; }
        public Guid UserId { get; set; }
        public DateTime SaleDate { get; set; }
        public decimal Price { get; set; }
        public string CarMake { get; set; } = string.Empty; 
    }

    public class Database
    {
        public List<User> Users { get; set; } = new List<User>();
        public List<Showroom> Showrooms { get; set; } = new List<Showroom>();
        public List<Sale> Sales { get; set; } = new List<Sale>();
    }

    class Program
    {
        static Database db = new Database();
        static string dataFile = "data.json";
        static User currentUser = null;

        static void Main(string[] args)
        {
            LoadData();

            while (true)
            {
                if (currentUser == null)
                {
                    ShowAuthMenu();
                }
                else
                {
                    ShowMainMenu();
                }
            }
        }

        static void ShowAuthMenu()
        {
            Console.Clear();
            Console.WriteLine("=== АВТОСАЛОН: ВХОД ===");
            Console.WriteLine("1. Вход");
            Console.WriteLine("2. Регистрация");
            Console.WriteLine("0. Выход");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            if (choice == "1") Login();
            else if (choice == "2") Register();
            else if (choice == "0")
            {
                SaveData();
                Environment.Exit(0);
            }
        }

        static void Login()
        {
            Console.Write("Имя пользователя: ");
            string username = Console.ReadLine();
            Console.Write("Пароль: ");
            string password = Console.ReadLine();

            var user = db.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
            if (user != null)
            {
                currentUser = user;
                Console.WriteLine("Вход выполнен.");
            }
            else
            {
                Console.WriteLine("Неверный логин или пароль.");
                Console.ReadKey();
            }
        }

        static void Register()
        {
            Console.Write("Новое имя пользователя: ");
            string username = Console.ReadLine();
            
            if (db.Users.Any(u => u.Username == username))
            {
                Console.WriteLine("Пользователь уже существует.");
                Console.ReadKey();
                return;
            }

            Console.Write("Новый пароль: ");
            string password = Console.ReadLine();

            User newUser = new User { Username = username, Password = password };
            db.Users.Add(newUser);
            SaveData();
            Console.WriteLine("Регистрация успешна.");
            Console.ReadKey();
        }

        static void ShowMainMenu()
        {
            Console.Clear();
            Console.WriteLine($"=== ГЛАВНОЕ МЕНЮ (Пользователь: {currentUser.Username}) ===");
            Console.WriteLine("1. Создать автосалон");
            Console.WriteLine("2. Управление автосалонами");
            Console.WriteLine("3. Статистика");
            Console.WriteLine("4. Выход из аккаунта");
            Console.Write("Выбор: ");
            string choice = Console.ReadLine();

            if (choice == "1") CreateShowroom();
            else if (choice == "2") ManageShowrooms();
            else if (choice == "3") ShowStatistics();
            else if (choice == "4") currentUser = null;
        }

        static void CreateShowroom()
        {
            Console.Write("Название салона: ");
            string name = Console.ReadLine();
            Console.Write("Вместимость машин: ");
            if (!int.TryParse(Console.ReadLine(), out int capacity)) return;

            Showroom s = new Showroom { Name = name, CarCapacity = capacity, Cars = new List<Car>() };
            db.Showrooms.Add(s);
            SaveData();
            Console.WriteLine("Салон создан.");
            Console.ReadKey();
        }

        static void ManageShowrooms()
        {
            if (db.Showrooms.Count == 0)
            {
                Console.WriteLine("Нет доступных салонов.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nСписок салонов:");
            for (int i = 0; i < db.Showrooms.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {db.Showrooms[i].Name} (Машин: {db.Showrooms[i].CarCount}/{db.Showrooms[i].CarCapacity})");
            }
            Console.WriteLine("0. Назад");
            Console.Write("Выберите салон для управления: ");
            
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 0 || index > db.Showrooms.Count) return;
            if (index == 0) return;

            Showroom currentShowroom = db.Showrooms[index - 1];
            ManageSingleShowroom(currentShowroom);
        }

        static void ManageSingleShowroom(Showroom showroom)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== САЛОН: {showroom.Name} ===");
                Console.WriteLine("1. Просмотр машин");
                Console.WriteLine("2. Добавить машину");
                Console.WriteLine("3. Редактировать салон");
                Console.WriteLine("4. Удалить салон");
                Console.WriteLine("5. Продать машину");
                Console.WriteLine("0. Назад");
                Console.Write("Выбор: ");
                string choice = Console.ReadLine();

                if (choice == "0") break;
                else if (choice == "1") ListCars(showroom);
                else if (choice == "2") AddCar(showroom);
                else if (choice == "3") EditShowroom(showroom);
                else if (choice == "4") 
                {
                    DeleteShowroom(showroom);
                    break;
                }
                else if (choice == "5") SellCar(showroom);
            }
        }

        static void ListCars(Showroom showroom)
        {
            Console.WriteLine("\nМашины в салоне:");
            int idx = 1;
            foreach (var car in showroom.Cars)
            {
                Console.WriteLine($"{idx}. {car.Make} {car.Model} ({car.Year.Year})");
                idx++;
            }
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        static void AddCar(Showroom showroom)
        {
            if (showroom.CarCount >= showroom.CarCapacity)
            {
                Console.WriteLine("Салон переполнен!");
                Console.ReadKey();
                return;
            }

            Console.Write("Марка: ");
            string make = Console.ReadLine();
            Console.Write("Модель: ");
            string model = Console.ReadLine();
            Console.Write("Год выпуска (гггг): ");
            if (!int.TryParse(Console.ReadLine(), out int year)) return;

            Car car = new Car { Make = make, Model = model, Year = new DateTime(year, 1, 1) };
            showroom.Cars.Add(car);
            SaveData();
            Console.WriteLine("Машина добавлена.");
        }

        static void EditShowroom(Showroom showroom)
        {
            Console.Write("Новое название: ");
            string name = Console.ReadLine();
            showroom.Name = name;
            SaveData();
            Console.WriteLine("Сохранено.");
        }

        static void DeleteShowroom(Showroom showroom)
        {
            db.Showrooms.Remove(showroom);
            SaveData();
            Console.WriteLine("Салон удален.");
            Console.ReadKey();
        }

        static void SellCar(Showroom showroom)
        {
            if (showroom.Cars.Count == 0)
            {
                Console.WriteLine("Нет машин для продажи.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("\nВыберите машину для продажи:");
            for (int i = 0; i < showroom.Cars.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {showroom.Cars[i].Make} {showroom.Cars[i].Model}");
            }
            
            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > showroom.Cars.Count) return;

            Car carToSell = showroom.Cars[index - 1];

            Console.Write("Дата продажи (гггг-мм-дд, Enter для сегодня): ");
            string dateStr = Console.ReadLine();
            DateTime date = string.IsNullOrEmpty(dateStr) ? DateTime.Now : DateTime.Parse(dateStr);

            Console.Write("Цена продажи: ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal price)) return;

            Sale sale = new Sale
            {
                ShowroomId = showroom.Id,
                CarId = carToSell.Id,
                UserId = currentUser.Id,
                SaleDate = date,
                Price = price,
                CarMake = carToSell.Make
            };

            db.Sales.Add(sale);
            showroom.Cars.Remove(carToSell);
            showroom.SalesCount++;
            SaveData();

            Console.WriteLine("Машина продана!");
            Console.ReadKey();
        }

        static void ShowStatistics()
        {
            Console.Clear();
            Console.WriteLine("=== СТАТИСТИКА ===");
            
            Console.WriteLine("\n--- Продажи салонов (За все время) ---");
            foreach (var s in db.Showrooms)
            {
                Console.WriteLine($"{s.Name}: {s.SalesCount} продаж");
            }

            Console.WriteLine("\n--- Продажи за периоды ---");
            var now = DateTime.Now;
            var daySales = db.Sales.Where(s => s.SaleDate.Date == now.Date).Count();
            var weekSales = db.Sales.Where(s => s.SaleDate >= now.AddDays(-7)).Count();
            var monthSales = db.Sales.Where(s => s.SaleDate >= now.AddMonths(-1)).Count();
            var yearSales = db.Sales.Where(s => s.SaleDate >= now.AddYears(-1)).Count();

            Console.WriteLine($"За сегодня: {daySales}");
            Console.WriteLine($"За неделю: {weekSales}");
            Console.WriteLine($"За месяц: {monthSales}");
            Console.WriteLine($"За год: {yearSales}");

            Console.WriteLine("\n--- По маркам автомобилей ---");
            var byMake = db.Sales.GroupBy(s => s.CarMake)
                                 .Select(g => new { Make = g.Key, Count = g.Count() })
                                 .OrderByDescending(x => x.Count);

            foreach (var item in byMake)
            {
                Console.WriteLine($"{item.Make}: {item.Count}");
            }

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }

        static void SaveData()
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(db, options);
            File.WriteAllText(dataFile, json);
        }

        static void LoadData()
        {
            if (File.Exists(dataFile))
            {
                try
                {
                    string json = File.ReadAllText(dataFile);
                    var loaded = JsonSerializer.Deserialize<Database>(json);
                    if (loaded != null) db = loaded;
                }
                catch { }
            }
        }
    }
}
