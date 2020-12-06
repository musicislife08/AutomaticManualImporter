using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using AutomaticManualImporter.Models.ApiV3;

namespace AutomaticManualImporter.Services
{
    public class SonarrService : ISonarrService
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        public SonarrService(HttpClient httpClient)
        {
            _client = httpClient;
        }
        public async Task<V3Result> GetQueueItemsAsync()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "queue");
            var response = await _client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<V3Result>(content, _jsonSerializerOptions);
            return result;
        }

        public async Task<ICollection<ManualImportQuerryResult>> GetManualImportsAsync(string downloadId)
        {
            if (string.IsNullOrWhiteSpace(downloadId))
                throw new ArgumentNullException(nameof(downloadId), "downloadId Cannot be null or blank");
            var req = new HttpRequestMessage(HttpMethod.Get, $"manualimport?folder=&downloadId={downloadId}");
            var response = await _client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ICollection<ManualImportQuerryResult>>(content, _jsonSerializerOptions);
            result = result.Where(x => !x.Path.Contains("sample")).ToList();
            return result;
        }

        public async Task<ICollection<RemotePathMapping>> GetRemotePathMappingsAsync()
        {
            var req = new HttpRequestMessage(HttpMethod.Get, "remotepathmapping");
            var response = await _client.SendAsync(req);
            var content = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ICollection<RemotePathMapping>>(content, _jsonSerializerOptions);
            return result;
        }

        public async Task<ManualImportCommandResult> PostManualImportAsync(File file)
        {
            var req = new HttpRequestMessage(HttpMethod.Post, "command");
            var body = new ManualImportCommandBody()
            {
                Name = "ManualImport",
                ImportMode = "auto",
                Files = new List<File>() { file }
            };
            req.Content = new StringContent(JsonSerializer.Serialize(body));

            try
            {
                var response = await _client.SendAsync(req);
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<ManualImportCommandResult>(content, _jsonSerializerOptions);
                return result;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public File MapManualImportResultToFile(ManualImportQuerryResult querryResult)
        {
            return new File()
            {
                DownloadId = querryResult.DownloadId,
                EpisodeIds = querryResult.Episodes.Select(x => x.EpisodeFileId).ToArray(),
                FolderName = querryResult.FolderName,
                Language = querryResult.Language,
                Path = querryResult.Path,
                Quality = querryResult.Quality,
                SeriesId = querryResult.Series.Id
            };
        }
    }
}
