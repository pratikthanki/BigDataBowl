using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NFL.Shared;

namespace NFL.BigDataBowl
{
    public class DataReader : IHostedService
    {
        private ILogger _logger;
        private IHostApplicationLifetime _appLifetime;
        private static Requester _requester;
        private static string _trackingPath;

        public DataReader(
            ILogger<DataReader> logger, 
            IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _appLifetime = appLifetime;
            
            var basePath = @"https://raw.githubusercontent.com/nfl-football-ops/Big-Data-Bowl/master/Data";
            _trackingPath = $"{basePath}/tracking_gameId_2017090700.csv";
            _requester = new Requester();
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var trackingData = await ParseGames();
            foreach (var player in trackingData.Where(x => x.FrameId == 1))
                Console.WriteLine(player);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private static async Task<List<Tracking>> ParseGames()
        {
            var data = _requester.GetData(_trackingPath);
            
            var game = data.Split(
                new[] { Environment.NewLine },
                StringSplitOptions.None
            ).Skip(1).SkipLast(1).ToArray();

            var trackingData = new List<Tracking>();

            for (int row = 0; row < game.Length; row++)
            {
                var rowSplit = game[row].Split(",");

                if (rowSplit[0] == "NA")
                    continue;
                try
                {
                    trackingData.Add(new Tracking
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
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return trackingData;
        }
    }
}
