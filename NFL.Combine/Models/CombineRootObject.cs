using System.Collections.Generic;

namespace NFL.Combine.Models
{
    public class CombineRootObject
    {
        public string ResultUnit { get; set; }
        public List<CombineWorkout> Data { get; set; }
    }
}
