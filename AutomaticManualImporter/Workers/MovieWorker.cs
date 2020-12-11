using System;
using System.Threading;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutomaticManualImporter
{
    public class MovieWorker : BackgroundService
    {
        private readonly ILogger<MovieWorker> _logger;
        private readonly ISonarrService _service;
        private readonly ISftpService _sftpService;

        public MovieWorker(ILogger<MovieWorker> logger, ISonarrService sonarrService)
        {
            _logger = logger;
            _service = sonarrService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
