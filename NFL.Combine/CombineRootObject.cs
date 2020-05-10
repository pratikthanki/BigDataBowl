using System.Collections.Generic;

namespace NFL.Combine
{
    public class CombineRootObject
    {
        public string ResultUnit { get; set; }
        public List<CombineWorkout> Data { get; set; }
    }
}
