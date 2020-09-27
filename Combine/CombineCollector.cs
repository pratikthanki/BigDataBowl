using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NFL.Combine.Models;
using System.Net;
using System.IO;

namespace NFL.Combine
{
    public class CombineCollector
    {
        private static List<WorkoutResult> Results;
        private static FileWriter _fileWriter;

        private const string BaseUrl = @"http://www.nfl.com/liveupdate/combine/{0}/{1}/ALL.json";
        private readonly int[] _seasons = {2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020};
        private const string CombineStats = "CombineStats";

        private static readonly string[] _workouts =
        {
            "FORTY_YARD_DASH", "BENCH_PRESS", "VERTICAL_JUMP",
            "BROAD_JUMP", "THREE_CONE_DRILL", "TWENTY_YARD_SHUTTLE", "SIXTY_YARD_SHUTTLE"
        };

        public CombineCollector()
        {
            Results = new List<WorkoutResult>();
            _fileWriter = new FileWriter(CombineStats);
        }

        private static async Task GetCombineWorkout(int season, string workout)
        {
            var url = string.Format(BaseUrl, season, workout);

            var response = await GetData(url);
            var responseJson = JsonConvert.DeserializeObject<CombineRootObject>(response);

            foreach (var row in responseJson.Data.Where(row => row != null))
            {
                if (!Results.Exists(x => x.Id == row.Id))
                    Results.Add(new WorkoutResult(row, season));

                UpsertCombineStat(row);
                Console.WriteLine(row.ToString());
            }

            // _fileWriter.WriteToFile(
                // Results.FindAll(x => x.Season == season), $"{CombineStats}{season}");
        }

        private static void UpsertCombineStat(CombineWorkout row)
        {
            var workoutName = row.WorkoutName;
            float? result = null;

            if (row.Result != null)
                result = float.Parse(row.Result);

            foreach (var item in Results.Where(r => r.Id == row.Id))
            {
                if (workoutName == Workouts.FortyYardDash)
                    item.FortyYardDash = result;

                else if (workoutName == Workouts.BenchPress)
                    item.BenchPress = result;

                else if (workoutName == Workouts.VerticalJump)
                    item.VerticalJump = result;

                else if (workoutName == Workouts.BroadJump)
                    item.BroadJump = result;

                else if (workoutName == Workouts.ThreeConeDrill)
                    item.ThreeConeDrill = result;

                else if (workoutName == Workouts.TwentyYardShuttle)
                    item.TwentyYardShuttle = result;

                else if (workoutName == Workouts.SixtyYardShuttle)
                    item.SixtyYardShuttle = result;

                else
                    Console.WriteLine($"Invalid workout: PlayerId: {item.Id}; WorkoutName: {workoutName}");
            }
        }

        private void PrintResults()
        {
            foreach (var result in Results)
                Console.WriteLine(result.ToString());
        }

        public static async Task AllCombineWorkouts(int season)
        {
            foreach (var workout in _workouts)
            {
                Console.WriteLine($"Season: {season}, Workout: {workout}");
                await GetCombineWorkout(season, workout);
            }
        }

        public static async Task<string> GetData(string url)
        {
            var request = WebRequest.Create(url);

            request.ContentType = "application/json";
            request.Headers.Add("User-Agent",
                "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.75 Safari/537.36");

            using var response = (HttpWebResponse)await request.GetResponseAsync();
            await using var dataStream = response.GetResponseStream();

            if (dataStream == null)
                return null;

            using var reader = new StreamReader(dataStream);
            var content = await reader.ReadToEndAsync();
            return content;
        }
    }
}
