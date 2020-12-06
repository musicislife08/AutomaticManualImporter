using System;
using System.Collections.Generic;
using AutomaticManualImporter.Models.Api;

namespace AutomaticManualImporter.Models.Api
{
    public class QueueItem
    {
        public Series Series { get; set; }
        public Episode Episode { get; set; }
        public Quality Quality { get; set; }
        public float Size { get; set; }
        public string Title { get; set; }
        public float SizeLeft { get; set; }
        public string TimeLeft { get; set; }
        public DateTimeOffset EstimatedCompletionTime { get; set; }
        public string Status { get; set; }
        public string TrackedDownloadStatus { get; set; }
        public ICollection<StatusMessage> StatusMessages { get; set; }
        public string DownloadId { get; set; }
        public int Id { get; set; }
    }
}
