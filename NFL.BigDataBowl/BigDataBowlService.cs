using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using NFL.Shared;

namespace NFL.BigDataBowl
{
    public class BigDataBowlService : IHostedService
    {
        private static ILogger Logger;
        private Task _executingTask;

        private static Requester _requester;
        private static string _trackingPath;
        private static string _playsPath;
        private const string GameId = "2017090700";

        private static List<Tracking> TrackingData;
        private static List<Plays> PlayData;

        public BigDataBowlService(
            ILogger<BigDataBowlService> logger,
            IHostApplicationLifetime appLifetime)
        {
            Logger = logger;
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;

            Environment.ExitCode = 1;


            var basePath = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data";
            _trackingPath = $"{basePath}/tracking_gameId_{GameId}.csv";
            _playsPath = $"{basePath}/plays.csv";
            _requester = new Requester();

            TrackingData = new List<Tracking>();
            PlayData = new List<Plays>();
        }

        public async Task StartAsync(CancellationToken token)
        {
            await ReadTracking();
            await ReadPlays();

            var playOne = TrackingData.Where(x => x.PlayId == 1);
            foreach (var row in playOne)
            {
                Console.WriteLine(row);
            }
        }

        public Task StopAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }

        private static async Task ReadTracking()
        {
            var tracking = await ParseCsv(_trackingPath);
            foreach (var row in tracking)
            {
                var rowSplit = row.Split(",");

                if (rowSplit[0] == "NA")
                    continue;

                TrackingData.Add(new Tracking
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

            Logger.LogInformation($"Tracking data parsed: {TrackingData.Count}");
        }

        private static async Task ReadPlays()
        {
            var plays = await ParseCsv(_playsPath);
            foreach (var row in plays)
            {
                TextFieldParser parser = new TextFieldParser(new StringReader(row));
                parser.HasFieldsEnclosedInQuotes = true;
                parser.SetDelimiters(",");

                var rowSplit = parser.ReadFields();

                if (rowSplit[0] != GameId)
                    continue;

                PlayData.Add(new Plays
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

            Logger.LogInformation($"Plays data parsed: {PlayData.Count}");
        }

        private static async Task<string[]> ParseCsv(string path)
        {
            Logger.LogInformation($"Reading from path: {path}");

            var data = await _requester.GetData(path);
            var csv = data.Split(
                new[] {Environment.NewLine},
                StringSplitOptions.None
            ).Skip(1).SkipLast(1).ToArray();

            return csv;
        }
    }
}
