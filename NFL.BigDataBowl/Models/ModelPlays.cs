namespace NFL.BigDataBowl.Models
{
    public class ModelPlays
    {
        public int Quarter { get; set; }
        public int Down { get; set; }
        public float MinutesRemainingInQuarter { get; set; }
        public float StandardisedX { get; set; }
        public float StandardisedY { get; set; }
        public float StandardisedDir { get; set; }
        public float RelativeX { get; set; }
        public float RelativeY { get; set; }
        public float RelativeSpeedX { get; set; }
        public float RelativeSpeedY { get; set; }
    }
}