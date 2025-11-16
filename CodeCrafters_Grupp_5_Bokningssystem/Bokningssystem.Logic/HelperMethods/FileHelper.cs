using System.Reflection;
using System.Text.Json;


namespace Bokningssystem.Logic.HelperMethods
{
    public static class FileHelper
    {
        private static bool _initialized = false;
        private static Assembly _assembly = null!;
        private static string _rootPath = string.Empty;

        private static readonly Dictionary<Type, string> _bookableDirectories = new Dictionary<Type, string>();

        public static void Initialize(Assembly? assembly)
        {
            if (_initialized)
                throw new Exception("FileHelper is already initialized.");

            ArgumentNullException.ThrowIfNull(assembly, nameof(assembly));

            _assembly = assembly;
            _rootPath = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    assembly.GetName().Name!);

            _initialized = true;
        }

        /// <summary>
        /// Chain a type as key and create a folderpath as value
        /// </summary>
        public static void AddFolderPath(Type type, string name)
        {
            string fullFolderPath = Path.Combine(_rootPath, name);

            _bookableDirectories.Add(type, fullFolderPath);
        }

        /// <summary>
        /// Get directory of the given type
        /// </summary>
        public static string GetDirectory(Type type)
        {
            if (_bookableDirectories.TryGetValue(type, out string? path) && !string.IsNullOrEmpty(path))
            {
                return path;
            }

            return Path.Combine(_rootPath, "Annat");
        }

        private static void CreateFolders()
        {
            foreach(string path in _bookableDirectories.Values)
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Try save an IBookable object as json file
        /// </summary>
        public static bool TrySave(IBookable bookable)
        {
            CreateFolders();

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true
            };

            string directoryPath = GetDirectory(bookable.GetType());
            string name = bookable.Name.Replace(' ', '_');
            string filePath = Path.Combine(directoryPath, $"{name}.json");

            try
            {
                string content = JsonSerializer.Serialize(bookable, bookable.GetType(), options);

                File.WriteAllText(filePath, content);
            }
            catch (Exception)
            {
                Console.WriteLine($"Kunde inte spara '{filePath}'.");
                Console.Write("Tryck [Enter] för att fortsätta.");
                Console.ReadLine();
                return false;
            }

            return true;
        }
        
        /// <summary>
        /// Try load a generic type of IBookable from json file
        /// </summary>
        public static bool TryLoad<T>(out List<T> loaded) where T : IBookable
        {
            CreateFolders();

            loaded = new List<T>();

            string directoryPath = GetDirectory(typeof(T));
            string[] files = [];

            try
            {
                files = Directory.GetFiles(directoryPath);
            }
            catch(Exception ex)
            {
                return false;
            }

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = false
            };

            foreach (string file in files)
            {
                try
                {
                    string json = File.ReadAllText(file);
                    T? bookable = JsonSerializer.Deserialize<T>(json, options);

                    if (bookable is null)
                        continue;

                    loaded.Add(bookable);
                }
                catch(Exception ex)
                {
                    Console.WriteLine($"Kunde inte läsa in filen {file}: {ex.Message}");
                    Console.Write("Tryck [Enter] för att fortsätta.");
                    Console.ReadLine();
                }
            }

            return true;
        }

        public static string ReadFromEmbeddedFile(string fileName)
        {
            string str = string.Empty;

            try
            {
                string filePath = $"{_assembly.GetName().Name}.{fileName}";

                using (Stream stream = _assembly.GetManifestResourceStream(filePath))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        str = reader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Det var ett problem med att läsa in filen '{fileName}'.");
                Console.WriteLine();
                Console.Write("Tryck [ENTER] för att fortsätta.");
                Console.ReadLine();
            }

            return str;
        }
    }
}
