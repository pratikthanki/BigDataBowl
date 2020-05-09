using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace NFL.Combine
{
    public class CombineCollector
    {
        private readonly string baseurl;
        private readonly string[] workouts;
        private readonly int[] seasons;
        public List<WorkoutResult> results;

        public CombineCollector()
        {
            baseurl = @"http://www.nfl.com/liveupdate/combine/{0}/{1}/ALL.json";
            workouts = new[] { "FORTY_YARD_DASH", "BENCH_PRESS", "VERTICAL_JUMP",
                "BROAD_JUMP", "THREE_CONE_DRILL", "TWENTY_YARD_SHUTTLE", "SIXTY_YARD_SHUTTLE" };
            seasons = new[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };

            results = new List<WorkoutResult>();
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

        public void CombineWorkout(int season, string workout)
        {
            string url = String.Format(baseurl, season, workout);

            string response = GetData(url);
            CombineRootObject response_json = JsonConvert.DeserializeObject<CombineRootObject>(response);

            foreach (CombineWorkout row in response_json.data)
            {
                if (!results.Exists(x => x.Id == row.Id))
                {
                    results.Add(new WorkoutResult(row, season));
                }

                string workoutName = row.WorkoutName;
                float? result = null;
                if (row.Result != null)
                {
                    result = float.Parse(row.Result);
                }

                foreach (var item in results.Where(r => r.Id == row.Id))
                {
                    if (workoutName == WorkoutNames.FORTY_YARD_DASH)
                    {
                        item.FORTY_YARD_DASH = result;
                    }
                    else if (workoutName == WorkoutNames.BENCH_PRESS)
                    {
                        item.BENCH_PRESS = result;
                    }
                    else if (workoutName == WorkoutNames.VERTICAL_JUMP)
                    {
                        item.VERTICAL_JUMP = result;
                    }
                    else if (workoutName == WorkoutNames.BROAD_JUMP)
                    {
                        item.BROAD_JUMP = result;
                    }
                    else if (workoutName == WorkoutNames.THREE_CONE_DRILL)
                    {
                        item.THREE_CONE_DRILL = result;
                    }
                    else if (workoutName == WorkoutNames.TWENTY_YARD_SHUTTLE)
                    {
                        item.TWENTY_YARD_SHUTTLE = result;
                    }
                    else if (workoutName == WorkoutNames.SIXTY_YARD_SHUTTLE)
                    {
                        item.SIXTY_YARD_SHUTTLE = result;
                    }
                    else
                    {
                        Console.WriteLine($"Invalid workout: PlayerId: {item.Id}; WorkoutName: {workout}");
                    }
                }
            }
        }

        public void PrintResults()
        {
            foreach (var result in results)
            {
                Console.WriteLine(result.ToString());
            }
        }

        public void AllCombineWorkouts(int season)
        {
            foreach(var workout in workouts)
            {
                CombineWorkout(season, workout);
            }
        }

        public void BackloadCombineWorkouts()
        {
            foreach (var season in seasons)
            {
                AllCombineWorkouts(season);
            }
        }
    }
}
