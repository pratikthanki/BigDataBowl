using System.IO;
using System.Net;

namespace NFL.Shared
{
    public class Requester
    {
        public Requester()
        {
        }
        public string GetData(string url)
        {
            WebRequest request = WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.75 Safari/537.36");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream dataStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            string content = reader.ReadToEnd();

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return content;
        }
    }
}
