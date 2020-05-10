using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using NFL.Shared;

namespace NFL.Combine
{
    public class CombineCollector
    {
        private readonly string _baseurl;
        private readonly string[] _workouts;
        private readonly int[] _seasons;
        public List<WorkoutResult> Results;
        private Requester _requester;
        private FileWriter _fileWriter;
        private readonly string _combineStats;

        public CombineCollector()
        {
            _baseurl = @"http://www.nfl.com/liveupdate/combine/{0}/{1}/ALL.json";
            _workouts = new[] { "FORTY_YARD_DASH", "BENCH_PRESS", "VERTICAL_JUMP",
                "BROAD_JUMP", "THREE_CONE_DRILL", "TWENTY_YARD_SHUTTLE", "SIXTY_YARD_SHUTTLE" };
            _seasons = new[] { 2012, 2013, 2014, 2015, 2016, 2017, 2018, 2019, 2020 };

            Results = new List<WorkoutResult>();
            _requester = new Requester();
            _combineStats = "CombineStats";
            _fileWriter = new FileWriter(_combineStats);
        }

        private void CombineWorkout(int season, string workout)
        {
            string url = string.Format(_baseurl, season, workout);

            string response = _requester.GetData(url);
            CombineRootObject responseJson = JsonConvert.DeserializeObject<CombineRootObject>(response);

            foreach (CombineWorkout row in responseJson.Data)
            {
                if (row == null)
                {
                    continue;
                }

                if (!Results.Exists(x => x.Id == row.Id))
                {
                    Results.Add(new WorkoutResult(row, season));
                }

                UpsertCombineStat(row);
            }

            _fileWriter.WriteToFile(Results.FindAll(x => x.Season == season), $"{_combineStats}{season}");
        }

        private void UpsertCombineStat(CombineWorkout row)
        {
            string workoutName = row.WorkoutName;
            float? result = null;
            if (row.Result != null)
            {
                result = float.Parse(row.Result);
            }

            foreach (var item in Results.Where(r => r.Id == row.Id))
            {
                if (workoutName == WorkoutNames.FortyYardDash)
                {
                    item.FortyYardDash = result;
                }
                else if (workoutName == WorkoutNames.BenchPress)
                {
                    item.BenchPress = result;
                }
                else if (workoutName == WorkoutNames.VerticalJump)
                {
                    item.VerticalJump = result;
                }
                else if (workoutName == WorkoutNames.BroadJump)
                {
                    item.BroadJump = result;
                }
                else if (workoutName == WorkoutNames.ThreeConeDrill)
                {
                    item.ThreeConeDrill = result;
                }
                else if (workoutName == WorkoutNames.TwentyYardShuttle)
                {
                    item.TwentyYardShuttle = result;
                }
                else if (workoutName == WorkoutNames.SixtyYardShuttle)
                {
                    item.SixtyYardShuttle = result;
                }
                else
                {
                    Console.WriteLine($"Invalid workout: PlayerId: {item.Id}; WorkoutName: {workoutName}");
                }
            }
        }

        private void PrintResults()
        {
            foreach (var result in Results)
            {
                Console.WriteLine(result.ToString());
            }
        }

        public void AllCombineWorkouts(int season)
        {
            foreach(var workout in _workouts)
            {
                Console.WriteLine($"Season: {season}, Workout: {workout}");
                CombineWorkout(season, workout);
            }
        }

        public void BackloadCombineWorkouts()
        {
            foreach (var season in _seasons)
            {
                AllCombineWorkouts(season);
            }
        }
    }
}
