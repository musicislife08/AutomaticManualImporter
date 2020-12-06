using System.Collections.Generic;

namespace AutomaticManualImporter.Models.Api
{
    public class StatusMessage
    {
        public string Title { get; set; }
        public ICollection<string> Messages { get; set; }
    }
}
