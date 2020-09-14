using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NFL.Combine
{
    public class FileWriter
    {
        private const string CurrentDirectory = @"/Users/PratikThanki/Projects/NFL/Data/";
        private readonly string _folderPath;

        public FileWriter(string folder)
        {
            _folderPath = Path.Combine(CurrentDirectory, folder);
        }

        public void WriteToFile(List<WorkoutResult> data, string fileName)
        {
            // Create directory if it doesn't exist
            if (Directory.Exists(_folderPath))
                return;

            Directory.CreateDirectory(_folderPath);
            Console.WriteLine($"Directory created: {_folderPath}");

            var path = Path.Combine(_folderPath, fileName);
            var file = File.CreateText($"{path}.json");
            var serializer = new JsonSerializer();

            serializer.Serialize(file, data);
            file.Close();
        }
    }
}
