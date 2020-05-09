using System.Collections.Generic;

namespace NFL.Combine
{
    public class CombineRootObject
    {
        public string resultUnit { get; set; }
        public List<CombineWorkout> data { get; set; }
    }
}
