using System;
using System.IO;
using Newtonsoft.Json;

namespace NFL.Combine
{
    public class Writer
    {
        public Writer()
        {
        }

        public void writeToFile(CombineRootObject data)
        {
            using (StreamWriter file = File.CreateText(@"D:\path.txt"))
            {
                JsonSerializer serializer = new JsonSerializer();
                //serialize object directly into file stream
                serializer.Serialize(file, data);
            }

            string json = JsonConvert.SerializeObject(data);
            
        }
    }
}
