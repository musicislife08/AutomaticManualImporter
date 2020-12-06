using System.Collections.Generic;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class Body
    {
        public ICollection<File> Files { get; set; }
        public bool SendUpdatesToClient { get; set; }
        public bool RequiresDiskAccess { get; set; }
        public string ImportMode { get; set; }
        public bool UpdateScheduledTask { get; set; }
        public string CompletionMessage { get; set; }
        public bool IsExclusive { get; set; }
        public string Name { get; set; }
        public string Trigger { get; set; }
        public bool SuppressMessages { get; set; }
    }
}