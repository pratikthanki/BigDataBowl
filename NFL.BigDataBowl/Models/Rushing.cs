using System;
using CsvHelper.Configuration;

namespace NFL.BigDataBowl.Models
{
    public class Rushing
    {
        public long GameId { get; set; }
        public long PlayId { get; set; }
        public string Team { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float S { get; set; }
        public float A { get; set; }
        public float Dis { get; set; }
        public float Orientation { get; set; }
        public float Dir { get; set; }
        public long NflId { get; set; }
        public string DisplayName { get; set; }
        public int JerseyNumber { get; set; }
        public int Season { get; set; }
        public int YardLine { get; set; }
        public int Quarter { get; set; }
        public DateTime GameClock { get; set; }
        public string PossessionTeam { get; set; }
        public int Down { get; set; }
        public int Distance { get; set; }
        public string FieldPosition { get; set; }
        public int HomeScoreBeforePlay { get; set; }
        public int VisitorScoreBeforePlay { get; set; }
        public long NflIdRusher { get; set; }
        public string OffenseFormation { get; set; }
        public string OffensePersonnel { get; set; }
        public int DefendersInTheBox { get; set; }
        public string DefensePersonnel { get; set; }
        public string PlayDirection { get; set; }
        public DateTime TimeHandoff { get; set; }
        public DateTime TimeSnap { get; set; }
        public int Yards { get; set; }
        public string PlayerHeight { get; set; }
        public long PlayerWeight { get; set; }
        public DateTime PlayerBirthDate { get; set; }
        public string PlayerCollegeName { get; set; }
        public string Position { get; set; }
        public string HomeTeamAbbr { get; set; }
        public string VisitorTeamAbbr { get; set; }
        public int Week { get; set; }
        public string Stadium { get; set; }
        public string Location { get; set; }
        public string StadiumType { get; set; }
        public string Turf { get; set; }
        public string GameWeather { get; set; }
        public long Temperature { get; set; }
        public long Humidity { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
    }

    public class ModelClassMap : ClassMap<Rushing>
    {
        public ModelClassMap()
        {
            Map(m => m.GameId).Name("GameId");
            Map(m => m.PlayId).Name("PlayId");
            Map(m => m.Team).Name("Team");
            Map(m => m.X).Name("X");
            Map(m => m.Y).Name("Y");
            Map(m => m.S).Name("S");
            Map(m => m.A).Name("A");
            Map(m => m.Dis).Name("Dis");
            Map(m => m.Orientation).Name("Orientation");
            Map(m => m.Dir).Name("Dir");
            Map(m => m.NflId).Name("NflId");
            Map(m => m.DisplayName).Name("DisplayName");
            Map(m => m.JerseyNumber).Name("JerseyNumber");
            Map(m => m.Season).Name("Season");
            Map(m => m.YardLine).Name("YardLine");
            Map(m => m.Quarter).Name("Quarter");
            Map(m => m.GameClock).Name("GameClock");
            Map(m => m.PossessionTeam).Name("PossessionTeam");
            Map(m => m.Down).Name("Down");
            Map(m => m.Distance).Name("Distance");
            Map(m => m.FieldPosition).Name("FieldPosition");
            Map(m => m.HomeScoreBeforePlay).Name("HomeScoreBeforePlay");
            Map(m => m.VisitorScoreBeforePlay).Name("VisitorScoreBeforePlay");
            Map(m => m.NflIdRusher).Name("NflIdRusher");
            Map(m => m.OffenseFormation).Name("OffenseFormation");
            Map(m => m.OffensePersonnel).Name("OffensePersonnel");
            Map(m => m.DefendersInTheBox).Name("DefendersInTheBox");
            Map(m => m.DefensePersonnel).Name("DefensePersonnel");
            Map(m => m.PlayDirection).Name("PlayDirection");
            Map(m => m.TimeHandoff).Name("TimeHandoff");
            Map(m => m.TimeSnap).Name("TimeSnap");
            Map(m => m.Yards).Name("Yards");
            Map(m => m.PlayerHeight).Name("PlayerHeight");
            Map(m => m.PlayerWeight).Name("PlayerWeight");
            Map(m => m.PlayerBirthDate).Name("PlayerBirthDate");
            Map(m => m.PlayerCollegeName).Name("PlayerCollegeName");
            Map(m => m.Position).Name("Position");
            Map(m => m.HomeTeamAbbr).Name("HomeTeamAbbr");
            Map(m => m.VisitorTeamAbbr).Name("VisitorTeamAbbr");
            Map(m => m.Week).Name("Week");
            Map(m => m.Stadium).Name("Stadium");
            Map(m => m.Location).Name("Location");
            Map(m => m.StadiumType).Name("StadiumType");
            Map(m => m.Turf).Name("Turf");
            Map(m => m.GameWeather).Name("GameWeather");
            Map(m => m.Temperature).Name("Temperature");
            Map(m => m.Humidity).Name("Humidity");
            Map(m => m.WindSpeed).Name("WindSpeed");
            Map(m => m.WindDirection).Name("WindDirection");
        }
    }

}
