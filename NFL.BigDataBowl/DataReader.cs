using System;
using System.Collections.Generic;

namespace NFL.BigDataBowl
{
    public class DataReader
    {

        private readonly string trackingUrl;
        private readonly string gamesUrl;
        private readonly string playUrl;

        private static List<Tracking> tracking;
        private static List<Games> games;
        private static List<Plays> plays;

        public DataReader()
        {
            trackingUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/tracking_gameId_2017090700.csv";
            gamesUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/games.csv";
            playUrl = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data/plays.csv";
        }

        private void ReadCsv(string url)
        {

        }
    }
}
