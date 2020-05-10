using System;
namespace NFL.BigDataBowl
{
    public class Plays
    {
        public int gameId { get; set; }
        public int playId { get; set; }
        public int quarter { get; set; }
        public string GameClock { get; set; }
        public int down { get; set; }
        public int yardsToGo { get; set; }
        public string possessionTeam { get; set; }
        public string yardlineSide { get; set; }
        public int yardlineNumber { get; set; }
        public string offenseFormation { get; set; }
        public string personnel_offense { get; set; }
        public object defendersInTheBox { get; set; }
        public object numberOfPassRushers { get; set; }
        public string personnel_defense { get; set; }
        public int HomeScoreBeforePlay { get; set; }
        public int VisitorScoreBeforePlay { get; set; }
        public int HomeScoreAfterPlay { get; set; }
        public int VisitorScoreAfterPlay { get; set; }
        public string isPenalty { get; set; }
        public string isSTPlay { get; set; }
        public string SpecialTeamsPlayType { get; set; }
        public object KickReturnYardage { get; set; }
        public object PassLength { get; set; }
        public string PassResult { get; set; }
        public object YardsAfterCatch { get; set; }
        public int PlayResult { get; set; }
        public string playDescription { get; set; }
    }
}
