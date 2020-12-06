using System;
using System.Collections.Generic;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class Record
    {
        public int SeriesId { get; set; }
        public int EpisodeId { get; set; }
        public Language Language { get; set; }
        public Quality Quality { get; set; }
        public float Size { get; set; }
        public string Title { get; set; }
        public float SizeLeft { get; set; }
        public string Timeleft { get; set; }
        public DateTimeOffset EstimatedCompletionTime { get; set; }
        public string Status { get; set; }
        public TrackedDownloadStatus TrackedDownloadStatus { get; set; }
        public TrackedDownloadState TrackedDownloadState { get; set; }
        public ICollection<StatusMessage> StatusMessages { get; set; }
        public string DownloadId { get; set; }
        public DownloadProtocol Protocol { get; set; }
        public string DownloadClient { get; set; }
        public string Indexer { get; set; }
        public string OutputPath { get; set; }
        public int Id { get; set; }
    }
}
