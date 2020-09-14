using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace NFL.BigDataBowl.Utilities
{
    public class Requester
    {
        public static async Task<string> GetData(string url)
        {
            var request = WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.75 Safari/537.36");

            using var response = (HttpWebResponse) await request.GetResponseAsync();
            await using var dataStream = response.GetResponseStream();

            if (dataStream == null)
                return null;

            using var reader = new StreamReader(dataStream);
            var content = await reader.ReadToEndAsync();
            return content;
        }
    }
}
