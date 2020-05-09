namespace NFL.Combine
{
    public class WorkoutResult : IWorkoutResult
    {
        private readonly CombineWorkout _playerWorkout;

        public WorkoutResult(CombineWorkout playerWorkout, int season)
        {
            _playerWorkout = playerWorkout;

            this.Id = _playerWorkout.Id;
            this.ShieldId = _playerWorkout.ShieldId;
            this.FirstName = _playerWorkout.FirstName;
            this.LastName = _playerWorkout.LastName;
            this.College = _playerWorkout.College;
            this.Position = _playerWorkout.Position;
            this.season = season;
        }
    
        public int Id { get; set; }
        public string ShieldId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Position { get; set; }
        public int season { get; set; }
        public float? FORTY_YARD_DASH { get; set; }
        public float? BENCH_PRESS { get; set; }
        public float? VERTICAL_JUMP { get; set; }
        public float? BROAD_JUMP { get; set; }
        public float? THREE_CONE_DRILL { get; set; }
        public float? TWENTY_YARD_SHUTTLE { get; set; }
        public float? SIXTY_YARD_SHUTTLE { get; set; }

        public bool Equals(CombineWorkout row)
        {
            return this.Id == row.Id;
        }

        public override string ToString()
        {
            return $"" +
                $"Season: {this.season}, " +
                $"Player: {FirstName} {LastName}, " +
                $"College: {College}, " +
                $"Position: {Position}, " +
                $"FORTY_YARD_DASH: {FORTY_YARD_DASH}, " +
                $"BENCH_PRESS: {BENCH_PRESS}, " +
                $"VERTICAL_JUMP: {VERTICAL_JUMP}, " +
                $"BROAD_JUMP: {BROAD_JUMP}, " +
                $"THREE_CONE_DRILL: {THREE_CONE_DRILL}, " +
                $"TWENTY_YARD_SHUTTLE: {TWENTY_YARD_SHUTTLE}, " +
                $"SIXTY_YARD_SHUTTLE: {SIXTY_YARD_SHUTTLE}";
        }
    }
}
