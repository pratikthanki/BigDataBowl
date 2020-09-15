using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.FileIO;
using NFL.BigDataBowl.Models;
using NFL.BigDataBowl.Utilities;

namespace NFL.BigDataBowl.Services
{
    public class RushingService : IHostedService
    {
        private static ILogger Logger;
        private Task _executingTask;

        private static readonly string RelativePath = @"../../../../Data/train.csv";
        private static readonly string DataPath = CsvReader.GetAbsolutePath(RelativePath);

        public RushingService(ILogger<TrackingService> logger, IHostApplicationLifetime appLifetime)
        {
            Logger = logger;
            var source = new CancellationTokenSource();
            var token = source.Token;

            Environment.ExitCode = 1;
        }

        public async Task StartAsync(CancellationToken token)
        {
            var rushingPlays = ReadTracking();

            foreach (var rush in rushingPlays)
                Console.WriteLine(rush);
        }

        private static IEnumerable<Rushing> ReadTracking()
        {
            var rushingPlays = new List<Rushing>();
            const string GameClockFormat = "HH:mm:ss";
            const string TimeFormat = "yyyy-MM-ddTHH:mm:ss.fffZ";
            const string BirthDateFormat = "MM/dd/yyyy";

            Logger.LogInformation($"Reading from: {DataPath}");

            using var parser = new TextFieldParser(DataPath) {HasFieldsEnclosedInQuotes = true};
            parser.SetDelimiters(",");

            // Skip the header row
            if (!parser.EndOfData)
                parser.ReadLine();

            while (!parser.EndOfData)
            {
                var fields = parser.ReadFields();
                var play = new Rushing
                {
                    GameId = ParseLong(fields[0]),
                    PlayId = ParseLong(fields[1]),
                    Team = fields[2],
                    X = ParseFloat(fields[3]),
                    Y = ParseFloat(fields[4]),
                    S = ParseFloat(fields[5]),
                    A = ParseFloat(fields[6]),
                    Dis = ParseFloat(fields[7]),
                    Orientation = ParseFloat(fields[8]),
                    Dir = ParseFloat(fields[9]),
                    NflId = ParseLong(fields[10]),
                    DisplayName = fields[11],
                    JerseyNumber = ParseInt(fields[12]),
                    Season = ParseInt(fields[13]),
                    YardLine = ParseInt(fields[14]),
                    Quarter = ParseInt(fields[15]),
                    GameClock = ParseDateTime(fields[16], GameClockFormat),
                    PossessionTeam = fields[17],
                    Down = ParseInt(fields[18]),
                    Distance = ParseInt(fields[19]),
                    FieldPosition = fields[20],
                    HomeScoreBeforePlay = ParseInt(fields[21]),
                    VisitorScoreBeforePlay = ParseInt(fields[22]),
                    NflIdRusher = ParseLong(fields[23]),
                    OffenseFormation = fields[24],
                    OffensePersonnel = fields[25],
                    DefendersInTheBox = ParseInt(fields[26]),
                    DefensePersonnel = fields[27],
                    PlayDirection = fields[28],
                    TimeHandoff = ParseDateTime(fields[29], TimeFormat),
                    TimeSnap = ParseDateTime(fields[30], TimeFormat),
                    Yards = ParseInt(fields[31]),
                    PlayerHeight = fields[32],
                    PlayerWeight = ParseInt(fields[33]),
                    PlayerBirthDate = ParseDateTime(fields[34], BirthDateFormat),
                    PlayerCollegeName = fields[35],
                    Position = fields[36],
                    HomeTeamAbbr = fields[37],
                    VisitorTeamAbbr = fields[38],
                    Week = ParseInt(fields[39]),
                    Stadium = fields[40],
                    Location = fields[41],
                    StadiumType = fields[42],
                    Turf = fields[43],
                    GameWeather = fields[44],
                    Temperature = ParseInt(fields[45]),
                    Humidity = ParseInt(fields[46]),
                    WindSpeed = fields[47],
                    WindDirection = fields[48]
                };


                rushingPlays.Add(play);

                if (rushingPlays.Count % 10_000 == 0)
                    Console.WriteLine($"row count: {rushingPlays.Count}");
            }

            Logger.LogInformation($"Rows processed : {rushingPlays.Count}");

            return rushingPlays;
        }

        private static float ParseFloat(string number) =>
            (float) (number == "" || number == "0" ? 0.0 : float.Parse(number));

        private static int ParseInt(string number) => number == "" ? 0 : int.Parse(number);

        private static long ParseLong(string number) => long.Parse(number);

        private static DateTime ParseDateTime(string number, string format) =>
            DateTime.ParseExact(number, format, null);

        public Task StopAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}
