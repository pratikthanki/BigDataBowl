using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using NFL.BigDataBowl.Models;
using NFL.BigDataBowl.Utilities;

namespace NFL.BigDataBowl.Services
{
    public class TrackingService : IHostedService
    {
        private static ILogger Logger;
        private Task _executingTask;

        private const string GameId = "2017090700";
        private const string BasePath = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data";
        private readonly string TrackingPath = $"{BasePath}/tracking_gameId_{GameId}.csv";
        private readonly string PlaysPath = $"{BasePath}/plays.csv";
        private readonly string GamesPath = $"{BasePath}/games.csv";
        private readonly string PlayersPath = $"{BasePath}/players.csv";

        public TrackingService(ILogger<TrackingService> logger, IHostApplicationLifetime appLifetime)
        {
            Logger = logger;
            var source = new CancellationTokenSource();
            var token = source.Token;

            Environment.ExitCode = 1;
        }

        public async Task StartAsync(CancellationToken token)
        {
            var Tracking = await ReadTracking();
            var Plays = await ReadPlays();

            var playOne = Tracking.Where(x => x.PlayId == 1);
            foreach (var row in playOne)
                Console.WriteLine(row);
        }

        public Task StopAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private async Task<List<Tracking>> ReadTracking()
        {
            var Tracking = new List<Tracking>();

            Logger.LogInformation($"Reading from path: {TrackingPath}");
            var tracking = await CsvReader.ParseCsv(TrackingPath);

            foreach (var row in tracking)
            {
                var rowSplit = row.Split(",");

                if (rowSplit[0] == "NA")
                    continue;

                Tracking.Add(new Tracking
                {
                    Time = Convert.ToDateTime(rowSplit[0]),
                    X = Convert.ToDouble(rowSplit[1]),
                    Y = Convert.ToDouble(rowSplit[2]),
                    S = Convert.ToDouble(rowSplit[3]),
                    Dis = Convert.ToDouble(rowSplit[4]),
                    Dir = rowSplit[5] == "NA" ? 0 : Convert.ToDouble(rowSplit[5]),
                    EventType = rowSplit[6],
                    NflId = rowSplit[7] == "NA" ? -1 : Convert.ToInt64(rowSplit[7]),
                    DisplayName = rowSplit[8],
                    JerseyNumber = rowSplit[9] == "NA" ? -1 : Convert.ToInt32(rowSplit[9]),
                    Team = rowSplit[10],
                    FrameId = Convert.ToInt64(rowSplit[11]),
                    GameId = Convert.ToInt64(rowSplit[12]),
                    PlayId = Convert.ToInt64(rowSplit[13])
                });
            }

            Logger.LogInformation($"Tracking data parsed: {Tracking.Count}");
            return Tracking;
        }

        private async Task<List<Plays>> ReadPlays()
        {
            var Plays = new List<Plays>();
            
            Logger.LogInformation($"Reading from path: {PlaysPath}");
            var plays = await CsvReader.ParseCsv(PlaysPath);

            foreach (var row in plays)
            {
                var parser = new TextFieldParser(new StringReader(row)) {HasFieldsEnclosedInQuotes = true};
                parser.SetDelimiters(",");

                var rowSplit = parser.ReadFields();

                if (rowSplit[0] != GameId)
                    continue;

                Plays.Add(new Plays
                {
                    GameId = Convert.ToInt64(rowSplit[0]),
                    PlayId = Convert.ToInt64(rowSplit[1]),
                    Quarter = Convert.ToInt32(rowSplit[2]),
                    GameClock = rowSplit[3],
                    Down = Convert.ToInt32(rowSplit[4]),
                    YardsToGo = Convert.ToInt32(rowSplit[5]),
                    PossessionTeam = rowSplit[6],
                    YardlineSide = rowSplit[7],
                    YardlineNumber = rowSplit[8] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[8]),
                    OffenseFormation = rowSplit[9],
                    PersonnelOffense = rowSplit[10],
                    DefendersInTheBox = rowSplit[11] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[11]),
                    NumberOfPassRushers = rowSplit[12] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[12]),
                    PersonnelDefense = rowSplit[13],
                    HomeScoreBeforePlay = Convert.ToInt32(rowSplit[14]),
                    VisitorScoreBeforePlay = Convert.ToInt32(rowSplit[15]),
                    HomeScoreAfterPlay = Convert.ToInt32(rowSplit[16]),
                    VisitorScoreAfterPlay = Convert.ToInt32(rowSplit[17]),
                    IsPenalty = rowSplit[18],
                    IsStPlay = rowSplit[19],
                    SpecialTeamsPlayType = rowSplit[20],
                    KickReturnYardage = rowSplit[21] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[21]),
                    PassLength = rowSplit[22] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[22]),
                    PassResult = rowSplit[23],
                    YardsAfterCatch = rowSplit[24] == "NA" ? (int?) null : Convert.ToInt32(rowSplit[24]),
                    PlayResult = Convert.ToInt32(rowSplit[25]),
                    PlayDescription = rowSplit[26]
                });
            }

            Logger.LogInformation($"Plays data parsed: {Plays.Count}");
            return Plays;
        }
    }
}
