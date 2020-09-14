
namespace NFL.Combine
{
    public class CombineWorkout
    {
        public int Id { get; set; }
        public string ShieldId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Position { get; set; }
        public string WorkoutName { get; set; }
        public string Video { get; set; }
        public string Result { get; set; }
        public bool Official { get; set; }
        public bool TopPerformer { get; set; }
        public bool OptOut { get; set; }
    }
}
