
namespace NFL.BigDataBowl.Models
{
    public class Game
    {
        public int Season { get; set; }
        public int Week { get; set; }
        public string GameDate { get; set; }
        public int GameId { get; set; }
        public string GameTimeEastern { get; set; }
        public int HomeScore { get; set; }
        public int VisitorScore { get; set; }
        public string HomeTeamAbbr { get; set; }
        public string VisitorTeamAbbr { get; set; }
        public string HomeDisplayName { get; set; }
        public string VisitorDisplayName { get; set; }
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