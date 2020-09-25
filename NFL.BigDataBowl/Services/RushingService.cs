using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NFL.BigDataBowl.MLModels;
using NFL.BigDataBowl.Models;

namespace NFL.BigDataBowl.Services
{
    public class RushingService : IHostedService
    {
        private static ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private static DataTransformer _transformer;

        public RushingService(ILogger<TrackingService> logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _transformer = new DataTransformer(_logger);
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(appLifetime.ApplicationStopping);

            Environment.ExitCode = 1;

            _cancellationTokenSource.Token.Register(() =>
            {
                _logger.LogInformation("Shutting down..");
                appLifetime.StopApplication();
            });
        }

        public async Task StartAsync(CancellationToken token)
        {
            var rawPlays = _transformer.ReadTracking();
            var preProcessedPlays = _transformer.PreProcess(rawPlays);
            var rushingMetrics = _transformer.RusherRelativeMetrics(preProcessedPlays);

            // metrics for each player by features
            var playerMetricsPerPlay = rushingMetrics
                .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                .ToDictionary(g => g.Key, g => g.ToList());

            var rushingPlayFeatures =
                rushingMetrics
                    .Select(x => new PlayMetadata
                        {GameId = x.GameId, Season = x.Season, Yards = x.Yards, PlayId = x.PlayId})
                    .ToList();

            ModelConfigurator.Run(playerMetricsPerPlay, _cancellationTokenSource.Token);
        }

        public Task StopAsync(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}