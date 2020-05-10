using System;
namespace NFL.BigDataBowl
{
    public class Tracking
    {
        public DateTime Time { get; set; }
        public double X { get; set; }
        public double Y { get; set; }
        public double S { get; set; }
        public double Dis { get; set; }
        public double Dir { get; set; }
        public string EventType { get; set; }
        public int NflId { get; set; }
        public string DisplayName { get; set; }
        public int JerseyNumber { get; set; }
        public string Team { get; set; }
        public int Frameid { get; set; }
        public int GameId { get; set; }
        public int PlayId { get; set; }
    }
}
