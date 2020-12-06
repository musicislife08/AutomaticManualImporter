using System.Collections.Generic;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class File
    {
        public string Path { get; set; }
        public string FolderName { get; set; }
        public int SeriesId { get; set; }
        public ICollection<int> EpisodeIds { get; set; }
        public Quality Quality { get; set; }
        public Language Language { get; set; }
        public string DownloadId { get; set; }
    }
}
