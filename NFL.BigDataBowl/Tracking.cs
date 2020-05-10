using System;
namespace NFL.BigDataBowl
{
    public class Tracking
    {
        public DateTime Time { get; set; }
        public double X { get; set; }
        public double y { get; set; }
        public double s { get; set; }
        public double dis { get; set; }
        public double dir { get; set; }
        public string eventType { get; set; }
        public int nflId { get; set; }
        public string displayName { get; set; }
        public int jerseyNumber { get; set; }
        public string team { get; set; }
        public int frameid { get; set; }
        public int gameId { get; set; }
        public int playId { get; set; }
    }
}
