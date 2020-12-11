using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AutomaticManualImporter.Services
{
    public class TvService : ITvService
    {
        private readonly ILogger<TvService> _logger;
        private readonly ISonarrService _sonarrService;
        private readonly ISftpService _sftpService;
        private readonly IRarService _rarService;
        private readonly Settings _settings;

        public TvService(ILogger<TvService> logger, ISonarrService sonarrService, ISftpService sftpService, IRarService rarService, IOptions<Settings> options)
        {
            _logger = logger;
            _sonarrService = sonarrService;
            _sftpService = sftpService;
            _rarService = rarService;
            _settings = options.Value;
        }
        public async Task Start(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queue = await _sonarrService.GetQueueItemsAsync();
                _logger.LogInformation($"Pulled {queue.Records.Count} records from Sonarr Queue");
                foreach (var record in queue.Records)
                {
                    var path = TranslateFolderPath(record.OutputPath);
                    _logger.LogDebug($"Translated path: {record.OutputPath} to: {path}");
                    var files = await _sftpService.GetRemoteFilesAsync(path);
                    _logger.LogInformation($"Downloading {files.Count} files from {_settings.SftpHost}");
                    await _sftpService.DownloadFilesAsync(files, record.Title);
                    _logger.LogInformation("Unzipping files");
                    await Task.Run(() => _rarService.UnzipFolder($"{_settings.TempFileProcessingPath}\\{record.Title}"), stoppingToken);
                    _logger.LogInformation($"Uploading unzipped media file");
                    await _sftpService.UploadMediaFileInPath($"{_settings.TempFileProcessingPath}\\{record.Title}", path);
                    _logger.LogInformation("Notifying Sonarr of new media file");
                    var manualImports = await _sonarrService.GetManualImportsAsync(record.DownloadId);
                    if (manualImports.Count > 0)
                    {
                        var manualImportResult = await _sonarrService.PostManualImportAsync(_sonarrService.MapManualImportResultToFile(manualImports.First()));
                        _logger.LogInformation($"{record.Title} Queued for manual import");
                        await Task.Run(() => CleanupTempFolder(record.Title), stoppingToken);
                    }
                }
                await Task.Delay(new TimeSpan(0, 10, 0), stoppingToken);
            }
        }

        private string TranslateFolderPath(string folderPath)
        {
            var split = folderPath.Split("\\");
            return $"{_settings.SftpBasePath}{_settings.RemoteTvFolder}/{split[^1]}";
        }

        private void CleanupTempFolder(string directory)
        {
            Directory.Delete($"{_settings.TempFileProcessingPath}\\{directory}");
        }
    }
}
