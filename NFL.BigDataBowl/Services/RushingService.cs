using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NFL.BigDataBowl.Models;

namespace NFL.BigDataBowl.Services
{
    public class RushingService : IHostedService
    {
        private static ILogger _logger;
        private static DataTransformer _transformer;
        private Task _executingTask;

        public RushingService(ILogger<TrackingService> logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _transformer = new DataTransformer(logger);
            var source = new CancellationTokenSource();
            var token = source.Token;

            Environment.ExitCode = 1;
        }

        public async Task StartAsync(CancellationToken token)
        {
            var rawPlays = _transformer.ReadTracking();
            var preProcessedPlays = _transformer.PreProcess(rawPlays).ToList();
            var rushingMetrics = _transformer.RusherRelativeMetrics(preProcessedPlays).ToList();

            // metrics for each player by features
            var playerMetricsPerPlay =
                rushingMetrics
                    .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                    .ToDictionary(g => g.Key, g => g.ToList());

            var rushingPlayFeatures =
                rushingMetrics
                    .Select(x => new PlayMetadata
                        {GameId = x.GameId, Season = x.Season, Yards = x.Yards, PlayId = x.PlayId})
                    .ToList();
        }

        public Task StopAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}