using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace AutomaticManualImporter.Services
{
    public class SftpService : ISftpService
    {
        private readonly Settings _settings;
        private readonly ILogger<SftpService> _logger;
        private readonly SftpClient _client;

        public SftpService(ILogger<SftpService> logger, IOptions<Settings> options)
        {
            _logger = logger;
            _settings = options.Value;
            _client = new SftpClient(_settings.SftpHost, _settings.SftpPort, _settings.SftpUsername, _settings.SftpPassword);
        }

        public SftpService(Settings settings)
        {
            _settings = settings;
            _client = new SftpClient(_settings.SftpHost, _settings.SftpPort, _settings.SftpUsername, _settings.SftpPassword);
        }
        public async Task<ICollection<string>> GetRemoteFilesAsync(string path)
        {
            if (!_client.IsConnected)
            {
                _logger.LogInformation("SftpClient not connected.  Connecting client");
                await Task.Run(() => _client.Connect());
            }
            else
            {
                _logger.LogInformation("SftpClient already connected");
            }
            _logger.LogDebug($"Grabbing files from {path}");
            var files = await Task.Run(() => _client.ListDirectory(path));
            var list = new List<string>();
            foreach (var file in files)
            {
                if (!file.IsDirectory)
                    list.Add(file.FullName);
            }
            return list;
        }

        public async Task DownloadFilesAsync(ICollection<string> files, string directory)
        {
            var tasks = new List<Task>();
            foreach(var file in files)
            {
                tasks.Add(DownloadFileAsync(file, directory));
            }
            await Task.WhenAll(tasks);
        }

        public async Task DownloadFileAsync(string path, string directory)
        {
            var tempPath = GetTempFilePath(path, directory);
            if (File.Exists(tempPath))
                return;
            if (!_client.IsConnected)
                await Task.Run(() => _client.Connect());
            using var fs = File.OpenWrite(tempPath);
            await Task.Run(() => _client.DownloadFile(path, fs));
        }

        public async Task UploadMediaFileInPath(string path, string remotePath)
        {
            if (!_client.IsConnected)
                await Task.Run(() => _client.Connect());
            var files = Directory.GetFiles(path);
            var mediaFile = files.Single(x => x.EndsWith(".mkv") || x.EndsWith(".mp4") || x.EndsWith(".m4v"));
            var fi = new FileInfo(mediaFile);
            var fullRemotePath = $"{remotePath}/{fi.Name}.incomplete";
            using var fs = File.OpenRead(mediaFile);
            await Task.Run(() => _client.UploadFile(fs, fullRemotePath));
            await Task.Run(() => _client.RenameFile(fullRemotePath, $"{remotePath}/{fi.Name}"));
        }

        private string GetTempFilePath(string file, string directory)
        {
            var split = file.Split("/");
            var filename = split[^1];
            return $"{_settings.TempFileProcessingPath}\\{directory}\\{filename}";
        }
    }
}
