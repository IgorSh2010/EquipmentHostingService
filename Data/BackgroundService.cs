using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace NewWebApplication2.Data
{
    public class BackgroundLoggerService : BackgroundService
    {
        private readonly Channel<string> _logChannel;
        private readonly ILogger<BackgroundLoggerService> _logger;

        public BackgroundLoggerService(Channel<string> logChannel, ILogger<BackgroundLoggerService> logger)
        {
            _logChannel = logChannel ?? throw new ArgumentNullException(nameof(logChannel));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Background logging service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Get message
                    var logMessage = await _logChannel.Reader.ReadAsync(stoppingToken);

                    // Logging message
                    _logger.LogInformation($"Log from background service: {logMessage}");
                }
                catch (OperationCanceledException)
                {
                    // Stop processing if the token is canceled
                    break;
                }
            }

            _logger.LogInformation("Background logging service stopped.");
        }
    }
}

