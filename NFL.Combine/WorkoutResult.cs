namespace NFL.Combine
{
    public class WorkoutResult : IWorkoutResult
    {
        public WorkoutResult(CombineWorkout playerWorkout, int season)
        {
            var PlayerWorkout = playerWorkout;

            this.Id = PlayerWorkout.Id;
            this.ShieldId = PlayerWorkout.ShieldId;
            this.FirstName = PlayerWorkout.FirstName;
            this.LastName = PlayerWorkout.LastName;
            this.College = PlayerWorkout.College;
            this.Position = PlayerWorkout.Position;
            this.Season = season;
        }
    
        public int Id { get; set; }
        public string ShieldId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Position { get; set; }
        public int Season { get; set; }
        public float? FortyYardDash { get; set; }
        public float? BenchPress { get; set; }
        public float? VerticalJump { get; set; }
        public float? BroadJump { get; set; }
        public float? ThreeConeDrill { get; set; }
        public float? TwentyYardShuttle { get; set; }
        public float? SixtyYardShuttle { get; set; }

        public bool Equals(CombineWorkout row)
        {
            return this.Id == row.Id;
        }

        public override string ToString()
        {
            return
                $"Season: {Season}, " +
                $"Player: {FirstName} {LastName}, " +
                $"College: {College}, " +
                $"Position: {Position}, " +
                $"FORTY_YARD_DASH: {FortyYardDash}, " +
                $"BENCH_PRESS: {BenchPress}, " +
                $"VERTICAL_JUMP: {VerticalJump}, " +
                $"BROAD_JUMP: {BroadJump}, " +
                $"THREE_CONE_DRILL: {ThreeConeDrill}, " +
                $"TWENTY_YARD_SHUTTLE: {TwentyYardShuttle}, " +
                $"SIXTY_YARD_SHUTTLE: {SixtyYardShuttle}";
        }
    }
}
