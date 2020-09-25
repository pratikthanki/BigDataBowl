using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NFL.BigDataBowl.MLModels;

namespace NFL.BigDataBowl.Services
{
    public class RushingService : IHostedService
    {
        private static ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private Task _task;
        private static DataTransformer _transformer;

        public RushingService(ILogger<RushingService> logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _transformer = new DataTransformer(_logger);
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(appLifetime.ApplicationStopping);

            Environment.ExitCode = 1;

            _cancellationTokenSource.Token.Register(() =>
            {
                _logger.LogInformation($"Shutting down {nameof(RushingService)}..");
                appLifetime.StopApplication();
            });
        }

        public Task StartAsync(CancellationToken token)
        {
            _logger.LogInformation($"Starting {nameof(RushingService)}..");

            if (_task != null)
                throw new InvalidOperationException();

            if (!_cancellationTokenSource.IsCancellationRequested)
                _task = Task.Run(RunRushingService, token);

            _logger.LogInformation($"Starting {nameof(RushingService)}..");

            return Task.CompletedTask;
        }

        private async Task RunRushingService()
        {
            var rawPlays = _transformer.ReadTracking();
            var rushingMetrics = _transformer.PreProcess(rawPlays);

            // metrics for each player by features
            var playerMetricsPerPlay = rushingMetrics
                .GroupBy(x => (x.GameId, x.Season, x.PlayId, x.Yards))
                .ToDictionary(g => g.Key, g => g.ToList());

            ModelConfigurator.Run(playerMetricsPerPlay, _cancellationTokenSource.Token);
        }

        public async Task StopAsync(CancellationToken token)
        {
            _logger.LogInformation($"Stopping {nameof(RushingService)}..");

            _cancellationTokenSource.Cancel();
            var runningTask = Interlocked.Exchange(ref _task, null);
            if (runningTask != null)
                await runningTask;

            _logger.LogInformation($"Stopped {nameof(RushingService)}..");
        }
    }
}