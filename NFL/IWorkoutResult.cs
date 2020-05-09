namespace NFL.Combine
{
    interface IWorkoutResult
    {
        public int Id { get; set; }
        public string ShieldId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string College { get; set; }
        public string Position { get; set; }
        public float? FORTY_YARD_DASH { get; set; }
        public float? BENCH_PRESS { get; set; }
        public float? VERTICAL_JUMP { get; set; }
        public float? BROAD_JUMP { get; set; }
        public float? THREE_CONE_DRILL { get; set; }
        public float? TWENTY_YARD_SHUTTLE { get; set; }
        public float? SIXTY_YARD_SHUTTLE { get; set; }

        bool Equals(CombineWorkout row);
    }
}
