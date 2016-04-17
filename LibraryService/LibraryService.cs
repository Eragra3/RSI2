using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.ServiceModel;
using System.Text;
using System.Text.RegularExpressions;

namespace Library
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "LibraryService" in both code and config file together.
    public class LibraryService : ILibraryService
    {
        private string _mainDirectory => ConfigurationManager.AppSettings["fileRepository"];

        public string Hello()
        {
            return "Hi!";
        }

        public LibraryTransaction<string> GetFile(string fileName)
        {
            Console.WriteLine($"Klient próbuje pobrać plik o nazwie ${fileName}");
            try
            {
                var formattedFileName = FormatFileName(fileName);
                using (
                    var reader =
                        new StreamReader(File.Open(_mainDirectory + $"/{formattedFileName}.txt", FileMode.Open,
                            FileAccess.Read, FileShare.Read)))
                {
                    var fileContent = reader.ReadToEnd();

                    return new LibraryTransaction<string>
                    {
                        Result = fileContent,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                return new LibraryTransaction<string>
                {
                    Success = false,
                    ErrorMessage = $"Nie znaleziono pliku - {fileName}"
                };
            }
        }

        public LibraryTransaction<string> GetLines(string fileName, int lowerBoundary, int upperBoundary)
        {
            Console.WriteLine($"Klient próbuje pobrać linijki od  ${lowerBoundary} do ${upperBoundary} z pliku ${fileName}");
            if (lowerBoundary < 1 || upperBoundary < 0 || (upperBoundary != 0 && lowerBoundary > upperBoundary)) return string.Empty;

            var fileContent = string.Empty;
            try
            {
                var formattedFileName = FormatFileName(fileName);
                using (var reader = new StreamReader(File.Open(_mainDirectory + $"/{formattedFileName}.txt", FileMode.Open, FileAccess.Read, FileShare.Read)))
                {
                    //skip initial lines 
                    for (var i = 0; i < lowerBoundary; i++)
                    {
                        reader.ReadLine();
                    }

                    if (upperBoundary == 0)
                    {
                        fileContent += reader.ReadToEnd();
                    }
                    else
                    {
                        for (var i = lowerBoundary; i < upperBoundary + 1; i++)
                        {
                            fileContent += reader.ReadLine() + '\n';
                        }
                    }

                    return new LibraryTransaction<string>
                    {
                        Result = fileContent,
                        Success = true
                    };
                }
            }
            catch (Exception e)
            {
                return new LibraryTransaction<string>
                {
                    Result = fileContent,
                    Success = true,
                    ErrorMessage = $"Nie znaleziono pliku - {fileName}"
                };
            }
        }

        public LibraryTransaction<bool> FileExists(string fileName)
        {
            Console.WriteLine($"Klient sprawdza czy plik ${fileName} istnieje");
            try
            {
                var formattedFileName = FormatFileName(fileName);
                return new LibraryTransaction<bool>
                {
                    Result = File.Exists(_mainDirectory + $"/{formattedFileName}.txt"),
                    Success = true
                };
            }
            catch (Exception)
            {
                return new LibraryTransaction<bool>
                {
                    Success = false,
                    ErrorMessage = $"Nie znaleziono pliku - {fileName}"
                };
            }
        }

        public LibraryTransaction<List<string>> GetAllFileNames()
        {
            Console.WriteLine($"Klient pobiera nazwy wszystkich plików");
            try
            {
                var fullFileNames = Directory.GetFiles(_mainDirectory, "*.txt", SearchOption.AllDirectories);
                var fileNames = fullFileNames
                    .Select(Path.GetFileNameWithoutExtension);
                    //.Aggregate((name, acc) => acc + "\n" + name);
                return new LibraryTransaction<List<string>>
                {
                    Result = fileNames,
                    Success = true
                };
            }
            catch (Exception)
            {
                return "W repozytorium nie ma jeszcze żadnych plików";
            }
        }

        private static string FormatFileName(string fileName)
        {
            return Regex.Replace(fileName, @" +", "_").ToLower();
        }
    }
}
