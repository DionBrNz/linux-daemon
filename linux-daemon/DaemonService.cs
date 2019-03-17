using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace linux_daemon
{
    internal class DaemonService : IHostedService, IDisposable
    {
        private readonly ILogger _logger;
        private readonly IOptions<DaemonConfig> _config;
        private readonly Timer _timer;
        public DaemonService(IOptions<DaemonConfig> config, ILogger<DaemonService> logger)
        {
            _config = config;
            _logger = logger;
            _timer = new Timer(TimerProc, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(30));
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Starting daemon: " + _config.Value.DaemonName);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping daemon.");
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _logger.LogInformation("Disposing....");
        }

        private void TimerProc(object state)
        {
            _logger.LogInformation("Daemon running");
        }
    }
}