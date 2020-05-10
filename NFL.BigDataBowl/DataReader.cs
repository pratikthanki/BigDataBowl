using System.Collections.Generic;
using NFL.Shared;

namespace NFL.BigDataBowl
{
    public class DataReader
    {
        private readonly string _trackingUrl;
        private readonly string _gamesUrl;
        private readonly string _playUrl;

        private static List<Tracking> _tracking;
        private static List<Games> _games;
        private static List<Plays> _plays;
        private readonly Requester _requester;

        public DataReader()
        {
            _trackingUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/tracking_gameId_2017090700.csv";
            _gamesUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/games.csv";
            _playUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/plays.csv";
            
            _requester = new Requester();

        }


        private string GetCsv(string url)
        {

            return "yes";
        }

    }
}
