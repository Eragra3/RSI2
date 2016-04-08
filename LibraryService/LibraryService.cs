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

        public string GetFile(string fileName)
        {
            try
            {
                var formattedFileName = FormatFileName(fileName);
                using (var reader = new StreamReader(_mainDirectory + $"/{formattedFileName}.txt"))
                {
                    var fileContent = reader.ReadToEnd();

                    return fileContent;
                }
            }
            catch (Exception e)
            {
                return $"Nie znaleziono pliku - {fileName}";
            }

        }

        public string GetLines(string fileName, int lowerBoundary, int upperBoundary)
        {
            if (lowerBoundary < 1 || upperBoundary < 0 || (upperBoundary != 0 && lowerBoundary > upperBoundary)) return string.Empty;

            var fileContent = string.Empty;
            try
            {
                var formattedFileName = FormatFileName(fileName);
                using (var reader = new StreamReader(_mainDirectory + $"/{formattedFileName}.txt"))
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
                    return fileContent;
                }
            }
            catch (Exception e)
            {
                return string.IsNullOrEmpty(fileContent) ? fileContent : $"Nie znaleziono pliku - {fileName}";
            }
        }

        public bool FileExists(string fileName)
        {
            try
            {
                var formattedFileName = FormatFileName(fileName);
                return File.Exists(_mainDirectory + $"/{formattedFileName}.txt");
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string GetAllFileNames()
        {
            try
            {
                var fileNames = Directory.GetFiles(_mainDirectory, "*.txt");
                return fileNames
                    .Select(Path.GetFileNameWithoutExtension)
                    .Aggregate((name, acc) => acc + "\n" + name);
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
