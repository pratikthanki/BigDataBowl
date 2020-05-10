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
        public float? FortyYardDash { get; set; }
        public float? BenchPress { get; set; }
        public float? VerticalJump { get; set; }
        public float? BroadJump { get; set; }
        public float? ThreeConeDrill { get; set; }
        public float? TwentyYardShuttle { get; set; }
        public float? SixtyYardShuttle { get; set; }

        bool Equals(CombineWorkout row);
    }
}
