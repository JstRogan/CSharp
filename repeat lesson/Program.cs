using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieSearchApp
{
    public class Movie
    {
        public string Title { get; set; } = string.Empty;
        public int Year { get; set; }
        public string Genre { get; set; } = string.Empty;
        public double Rating { get; set; }

        public override string ToString() => $"{Title} ({Year}) - {Genre} [{Rating}]";
    }

    public class SearchHistoryItem
    {
        public DateTime SearchDate { get; set; }
        public string Query { get; set; } = string.Empty;
        public int ResultsCount { get; set; }
    }

    public interface IFileService
    {
        void Save(string data, string fileName);
        void Delete(string fileName);
        string Load(string fileName);
    }

    public class FileService : IFileService
    {
        public void Save(string data, string fileName)
        {
            try
            {
                File.AppendAllText(fileName, data + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[LOG] Данные успешно сохранены в {fileName}");
                Console.ResetColor();
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Ошибка сохранения файла: {ex.Message}");
                Console.ResetColor();
            }
        }

        public void Delete(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"[LOG] Файл {fileName} был удален.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine($"[WARN] Файл {fileName} не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"[ERROR] Ошибка удаления файла: {ex.Message}");
                Console.ResetColor();
            }
        }

        public string Load(string fileName)
        {
            if (!File.Exists(fileName)) return string.Empty;
            return File.ReadAllText(fileName);
        }
    }

    public class MovieService
    {
        private List<Movie> _database;

        public MovieService()
        {
            _database = new List<Movie>
            {
                new Movie { Title = "Inception", Year = 2010, Genre = "Sci-Fi", Rating = 8.8 },
                new Movie { Title = "The Dark Knight", Year = 2008, Genre = "Action", Rating = 9.0 },
                new Movie { Title = "Interstellar", Year = 2014, Genre = "Sci-Fi", Rating = 8.6 },
                new Movie { Title = "Parasite", Year = 2019, Genre = "Thriller", Rating = 8.5 },
                new Movie { Title = "The Matrix", Year = 1999, Genre = "Sci-Fi", Rating = 8.7 },
                new Movie { Title = "Avengers: Endgame", Year = 2019, Genre = "Action", Rating = 8.4 }
            };
        }

        public List<Movie> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return new List<Movie>();
            
            return _database.FindAll(m => 
                m.Title.Contains(query, StringComparison.OrdinalIgnoreCase) || 
                m.Genre.Contains(query, StringComparison.OrdinalIgnoreCase));
        }
    }

    class Program
    {
        private static readonly IFileService _fileService = new FileService();
        private static readonly MovieService _movieService = new MovieService();
        private const string HISTORY_FILE = "search_history.txt";

        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("=== Movie Searcher v2.0 (Fixed & Improved) ===");
            
            while (true)
            {
                Console.WriteLine("\n--- Меню ---");
                Console.WriteLine("1. Поиск фильма");
                Console.WriteLine("2. История поиска");
                Console.WriteLine("3. Очистить историю");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        PerformSearch();
                        break;
                    case "2":
                        ShowHistory();
                        break;
                    case "3":
                        ClearHistory();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Неверный ввод, попробуйте снова.");
                        break;
                }
            }
        }

        static void PerformSearch()
        {
            Console.Write("\nВведите название или жанр для поиска: ");
            string query = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(query))
            {
                Console.WriteLine("Пустой запрос отменен.");
                return;
            }

            var results = _movieService.Search(query);

            Console.WriteLine($"\nРезультаты поиска (найдено {results.Count}):");
            if (results.Count > 0)
            {
                foreach (var movie in results)
                {
                    Console.WriteLine("- " + movie);
                }
            }
            else
            {
                Console.WriteLine("Ничего не найдено.");
            }

            var logEntry = $"{DateTime.Now}: Запрос '{query}' -> Найдено {results.Count} фильмов";
            _fileService.Save(logEntry, HISTORY_FILE);
        }

        static void ShowHistory()
        {
            Console.WriteLine("\n=== История поиска ===");
            string history = _fileService.Load(HISTORY_FILE);
            
            if (string.IsNullOrWhiteSpace(history))
            {
                Console.WriteLine("История пуста.");
            }
            else
            {
                Console.WriteLine(history);
            }
        }

        static void ClearHistory()
        {
            Console.WriteLine("Вы уверены, что хотите удалить историю? (y/n)");
            if (Console.ReadLine()?.ToLower() == "y")
            {
                _fileService.Delete(HISTORY_FILE);
            }
        }
    }
}
