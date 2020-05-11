using CsvHelper.Configuration;

namespace NFL.BigDataBowl
{
    public class Games
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

    public class GamesMap : ClassMap<Games>
    {
        public GamesMap()
        {
            Map(m => m.Season).Name("season");
            Map(m => m.Week).Name("week");
            Map(m => m.GameDate).Name("gameDate");
            Map(m => m.GameId).Name("gameId");
            Map(m => m.GameTimeEastern).Name("gameTimeEastern");
            Map(m => m.HomeScore).Name("HomeScore");
            Map(m => m.VisitorScore).Name("VisitorScore");
            Map(m => m.HomeTeamAbbr).Name("homeTeamAbbr");
            Map(m => m.VisitorTeamAbbr).Name("visitorTeamAbbr");
            Map(m => m.HomeDisplayName).Name("homeDisplayName");
            Map(m => m.VisitorDisplayName).Name("visitorDisplayName");
            Map(m => m.Stadium).Name("Stadium");
            Map(m => m.Location).Name("Location");
            Map(m => m.StadiumType).Name("StadiumType");
            Map(m => m.Turf).Name("Turf");
            Map(m => m.GameLength).Name("GameLength");
            Map(m => m.GameWeather).Name("GameWeather");
            Map(m => m.Temperature).Name("Temperature");
            Map(m => m.Humidity).Name("Humidity");
            Map(m => m.WindSpeed).Name("WindSpeed");
            Map(m => m.WindDirection).Name("WindDirection");
        }
    }
}
