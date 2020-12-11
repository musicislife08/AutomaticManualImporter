using System;
using System.Threading;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AutomaticManualImporter
{
    public class TvWorker : BackgroundService
    {
        private readonly ILogger<TvWorker> _logger;
        private readonly ITvService _service;

        public TvWorker(ILogger<TvWorker> logger, ITvService tvService)
        {
            _logger = logger;
            _service = tvService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(TvWorker)} Started at {DateTimeOffset.UtcNow}");
            await _service.Start(stoppingToken);
        }
    }
}
