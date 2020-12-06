using System.Collections.Generic;
using AutomaticManualImporter.Models.Api;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class ManualImportQuerryResult
    {
        public string Path { get; set; }
        public string RelativePath { get; set; }
        public string FolderName { get; set; }
        public string Name { get; set; }
        public float Size { get; set; }
        public Series Series { get; set; }
        public ICollection<Episode> Episodes { get; set; }
        public Quality Quality { get; set; }
        public Language Language { get; set; }
        public int QualityWeight { get; set; }
        public string DownloadId { get; set; }
        public ICollection<Rejection> Rejections { get; set; }
        public int Id { get; set; }
    }
}
