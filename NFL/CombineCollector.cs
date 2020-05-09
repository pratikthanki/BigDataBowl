using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NFL.Combine
{
    public class CombineCollector
    {
        private readonly string baseurl;
        private readonly string[] workouts;
        private readonly int[] seasons;
        public List<WorkoutResult> results;
        private Requester requester;
        private FileWriter fileWriter;
        private readonly string CombineStats;

        public CombineCollector()
        {
            baseurl = @"http://www.nfl.com/liveupdate/combine/{0}/{1}/ALL.json";
            workouts = new[] { "FORTY_YARD_DASH", "BENCH_PRESS", "VERTICAL_JUMP",
                "BROAD_JUMP", "THREE_CONE_DRILL", "TWENTY_YARD_SHUTTLE", "SIXTY_YARD_SHUTTLE" };
            seasons = new[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };

            results = new List<WorkoutResult>();
            requester = new Requester();
            CombineStats = "CombineStats";
            fileWriter = new FileWriter(CombineStats);
        }

        private void CombineWorkout(int season, string workout)
        {
            string url = string.Format(baseurl, season, workout);

            string response = requester.GetData(url);
            CombineRootObject response_json = JsonConvert.DeserializeObject<CombineRootObject>(response);

            foreach (CombineWorkout row in response_json.data)
            {
                if (row == null)
                {
                    continue;
                }

                if (!results.Exists(x => x.Id == row.Id))
                {
                    results.Add(new WorkoutResult(row, season));
                }

                UpsertCombineStat(row);
            }

            fileWriter.WriteToFile(results.FindAll(x => x.season == season), $"{CombineStats}{season}");
        }

        private void UpsertCombineStat(CombineWorkout row)
        {
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
                    Console.WriteLine($"Invalid workout: PlayerId: {item.Id}; WorkoutName: {workoutName}");
                }
            }
        }

        private void PrintResults()
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
                Console.WriteLine($"Season: {season}, Workout: {workout}");
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
