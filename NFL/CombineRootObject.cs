using System;
using System.Collections.Generic;

namespace NFL.Combine
{
    public class Data
    {
        public int id { get; set; }
        public string shieldId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string college { get; set; }
        public string position { get; set; }
        public string workoutName { get; set; }
        public string video { get; set; }
        public string result { get; set; }
        public bool official { get; set; }
        public bool topPerformer { get; set; }
        public bool optOut { get; set; }
    }

    public class RootObject
    {
        public string resultUnit { get; set; }
        public List<Data> data { get; set; }
    }

    public class WorkoutResults
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string college { get; set; }
        public string position { get; set; }
        public float FORTY_YARD_DASH { get; set; }
        public float BENCH_PRESS { get; set; }
        public float VERTICAL_JUMP { get; set; }
        public float BROAD_JUMP { get; set; }
        public float THREE_CONE_DRILL { get; set; }
        public float TWENTY_YARD_SHUTTLE { get; set; }
        public float SIXTY_YARD_SHUTTLE { get; set; }
    }
}
