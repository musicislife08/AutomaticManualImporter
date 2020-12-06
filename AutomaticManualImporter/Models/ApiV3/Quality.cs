using System.Text.Json.Serialization;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class Quality
    {
        [JsonPropertyName("quality")]
        public QualityQuality QualityQuality { get; set; }
        public Revision Revision { get; set; }
    }
}
