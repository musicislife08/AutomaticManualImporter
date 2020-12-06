using System.Collections.Generic;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class ManualImportCommandBody
    {
        public string Name { get; set; }
        public ICollection<File> Files { get; set; }
        public string ImportMode { get; set; }
    }
}
