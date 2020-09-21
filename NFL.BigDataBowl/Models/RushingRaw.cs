using System;

namespace NFL.BigDataBowl.Models
{
    public class RushingRaw
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

        // Flip location/tracking data so all plays appears as going from left to right
        public bool IsLeftDirection { get; set; }
        public bool IsBallCarrier { get; set; }
        public bool IsOnOffense { get; set; }
        public bool IsLeading { get; set; }
        public string TeamOnOffense { get; set; }
        public float MinutesRemainingInQuarter { get; set; }
        public int TimeDelta { get; set; }
        public int YardsFromOwnGoal { get; set; }
        public float StandardisedX { get; set; }
        public float StandardisedY { get; set; }
        public float StandardisedDir { get; set; }
        public float StandardisedSpeedX { get; set; }
        public float StandardisedSpeedY { get; set; }
        public int StandardisedYardLine { get; set; }
        public float StandardisedOrientation { get; set; }
    }
}