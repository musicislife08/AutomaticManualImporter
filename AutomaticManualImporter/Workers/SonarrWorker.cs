using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutomaticManualImporter
{
    public class SonarrWorker : BackgroundService
    {
        private readonly ILogger<SonarrWorker> _logger;
        private readonly ISonarrService _service;

        public SonarrWorker(ILogger<SonarrWorker> logger, ISonarrService sonarrService)
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
