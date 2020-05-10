using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace NFL.Combine
{
    public class FileWriter
    {
        private string _currentDirectory;
        private string folderPath;

        public FileWriter(string folder)
        {
            //currentDirectory = Directory.GetCurrentDirectory();
            _currentDirectory = @"/Users/PratikThanki/Projects/NFL/NFL/";
            folderPath = Path.Combine(_currentDirectory, folder);

        }

        public void WriteToFile(List<WorkoutResult> data, string fileName)
        {
            CreateDirectory();

            string path = Path.Combine(folderPath, fileName);
            StreamWriter file = File.CreateText($"{path}.json");
            JsonSerializer serializer = new JsonSerializer();

            //serialize object directly into file stream
            serializer.Serialize(file, data);
            file.Close();
        }

        private void CreateDirectory()
        {
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
                Console.WriteLine($"Directory created: {folderPath}");
            }
        }
    }
}
