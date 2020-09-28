using System;
using System.Threading;
using System.Threading.Tasks;
using BigDataBowl.MLModels;
using BigDataBowl.Utilities;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BigDataBowl.Services
{
    public class RushingService : IHostedService
    {
        private static ILogger _logger;
        private readonly CancellationTokenSource _cancellationTokenSource;
        private readonly ModelConfigurator _modelConfigurator;
        private Task _task;
        private static DataTransformer _transformer;

        public RushingService(ILogger<RushingService> logger, IHostApplicationLifetime appLifetime)
        {
            _logger = logger;
            _modelConfigurator = new ModelConfigurator();
            _transformer = new DataTransformer(_logger);
            _cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(appLifetime.ApplicationStopping);

            Environment.ExitCode = 0;

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
            try
            {
                var data = _transformer.ReadAndPreprocess();
                _modelConfigurator.Run(data);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception.ToString());
                _cancellationTokenSource.Cancel();

                Environment.ExitCode = 1;
            }

            _cancellationTokenSource.Cancel();
            Environment.ExitCode = 0;
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