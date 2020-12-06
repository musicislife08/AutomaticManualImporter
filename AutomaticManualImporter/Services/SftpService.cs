using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Renci.SshNet;

namespace AutomaticManualImporter.Services
{
    public class SftpService : ISftpService
    {
        private readonly SftpSettings _settings;
        private readonly ILogger<SftpService> _logger;
        private readonly SftpClient _client;

        public SftpService(ILogger<SftpService> logger, IOptions<SftpSettings> options)
        {
            _logger = logger;
            _settings = options.Value;
            _client = new SftpClient(_settings.Host, _settings.Port, _settings.Username, _settings.Password);
        }

        public SftpService(SftpSettings settings)
        {
            _settings = settings;
            _client = new SftpClient(_settings.Host, _settings.Port, _settings.Username, _settings.Password);
        }
        public async Task<ICollection<string>> GetRemoteFilesAsync(string path)
        {
            if (!_client.IsConnected)
                await Task.Run(() => _client.Connect());
            var files = await Task.Run(() => _client.ListDirectory(path));
            var list = new List<string>();
            foreach (var file in files)
            {
                if (!file.IsDirectory)
                    list.Add(file.FullName);
            }
            return list;
        }

        public async Task DownloadFilesAsync(ICollection<string> files)
        {
            var tasks = new List<Task>();
            foreach(var file in files)
            {
                tasks.Add(DownloadFileAsync(file));
            }
            await Task.WhenAll(tasks);
        }

        public async Task DownloadFileAsync(string path)
        {
            if (File.Exists(GetTempFilePath(path)))
                return;
            if (!_client.IsConnected)
                await Task.Run(() => _client.Connect());
            using var fs = File.OpenWrite(GetTempFilePath(path));
            await Task.Run(() => _client.DownloadFile(path, fs));
        }

        public async Task UploadFileAsync(string path, string remotePath)
        {
            if (!_client.IsConnected)
                await Task.Run(() => _client.Connect());
            var files = Directory.GetFiles(path);
            var mediaFile = files.Single(x => x.EndsWith(".mkv") || x.EndsWith(".mp4") || x.EndsWith(".m4v"));
            var fi = new FileInfo(mediaFile);
            var fullRemotePath = $"{remotePath}/{fi.Name}";
            using var fs = File.OpenRead(mediaFile);
            await Task.Run(() => _client.UploadFile(fs, fullRemotePath));
        }

        private string GetTempFilePath(string file)
        {
            var split = file.Split("/");
            var filename = split[split.Length - 1];
            return _settings.TempFileProcessingPath + "\\" + filename;
        }
    }
}
