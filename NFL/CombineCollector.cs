using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace NFL.Combine
{
    public class Loader
    {
        public WorkoutResults[] results;
        public string[] workouts = new[] { "FORTY_YARD_DASH", "BENCH_PRESS", "VERTICAL_JUMP", "BROAD_JUMP", "THREE_CONE_DRILL", "TWENTY_YARD_SHUTTLE", "SIXTY_YARD_SHUTTLE" };
        public int[] seasons = new[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };
        public string baseurl;

        public Loader()
        {
            baseurl = @"http://www.nfl.com/liveupdate/combine/{0}/{1}/ALL.json";
        }

        public string getData(string url)
        {
            WebRequest request = WebRequest.Create(url);                            // Create a request for the URL.

            request.ContentType = "application/json";
            request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.75 Safari/537.36");

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();          // Get the response.
            Stream dataStream = response.GetResponseStream();                           // Get the stream containing content returned by the server.
            StreamReader reader = new StreamReader(dataStream);                         // Open the stream using a StreamReader for easy access.
            string responseFromServer = reader.ReadToEnd();                             // Read the content.

            // Cleanup the streams and the response.
            reader.Close();
            dataStream.Close();
            response.Close();

            return responseFromServer;
        }

        public void getCombineWorkout(int season)
        {
            foreach(var workout in workouts)
            {
                string url = String.Format(baseurl, season, workout);

                string resp = getData(url);
                RootObject resp_json = JsonConvert.DeserializeObject<RootObject>(resp);

                foreach (var row in resp_json.data)
                {
                    row.
                    Console.WriteLine(row.id);
                }
            }
        }
    }
}
