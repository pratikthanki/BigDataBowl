
namespace BigDataBowl.DataModels
{
    public class Play
    {
        public long GameId { get; set; }
        public long PlayId { get; set; }
        public int Quarter { get; set; }
        public string GameClock { get; set; }
        public int Down { get; set; }
        public int YardsToGo { get; set; }
        public string PossessionTeam { get; set; }
        public string YardlineSide { get; set; }
        public int? YardlineNumber { get; set; }
        public string OffenseFormation { get; set; }
        public string PersonnelOffense { get; set; }
        public int? DefendersInTheBox { get; set; }
        public int? NumberOfPassRushers { get; set; }
        public string PersonnelDefense { get; set; }
        public int HomeScoreBeforePlay { get; set; }
        public int VisitorScoreBeforePlay { get; set; }
        public int HomeScoreAfterPlay { get; set; }
        public int VisitorScoreAfterPlay { get; set; }
        public string IsPenalty { get; set; }
        public string IsStPlay { get; set; }
        public string SpecialTeamsPlayType { get; set; }
        public int? KickReturnYardage { get; set; }
        public int? PassLength { get; set; }
        public string PassResult { get; set; }
        public int? YardsAfterCatch { get; set; }
        public int PlayResult { get; set; }
        public string PlayDescription { get; set; }
    }
}