namespace NFL.BigDataBowl.Models
{
    public class PlayMetrics
    {
        public long GameId { get; set; }
        public long PlayId { get; set; }
        public int Season { get; set; }
        public int Yards { get; set; }
        public int Quarter { get; set; }
        public int Down { get; set; }
        public float MinutesRemainingInQuarter { get; set; }
        public int YardsFromOwnGoal { get; set; }
        public float StandardisedX { get; set; }
        public float StandardisedY { get; set; }
        public float StandardisedDir { get; set; }
        public float RelativeX { get; set; }
        public float RelativeY { get; set; }
        public float RelativeSpeedX { get; set; }
        public float RelativeSpeedY { get; set; }
    }
}