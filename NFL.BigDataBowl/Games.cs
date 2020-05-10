using System;
namespace NFL.BigDataBowl
{
    public class Games
    {
        public int season { get; set; }
        public int week { get; set; }
        public string gameDate { get; set; }
        public int gameId { get; set; }
        public string gameTimeEastern { get; set; }
        public int HomeScore { get; set; }
        public int VisitorScore { get; set; }
        public string homeTeamAbbr { get; set; }
        public string visitorTeamAbbr { get; set; }
        public string homeDisplayName { get; set; }
        public string visitorDisplayName { get; set; }
        public string Stadium { get; set; }
        public string Location { get; set; }
        public string StadiumType { get; set; }
        public string Turf { get; set; }
        public string GameLength { get; set; }
        public string GameWeather { get; set; }
        public int Temperature { get; set; }
        public int Humidity { get; set; }
        public object WindSpeed { get; set; }
        public string WindDirection { get; set; }
    }
}
