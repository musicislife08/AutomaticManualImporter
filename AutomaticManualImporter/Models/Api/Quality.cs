using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace AutomaticManualImporter.Models.Api
{
    public class Quality
    {
        [JsonPropertyName("quality")]
        public QualityQuality QualityQuality { get; set; }
        public Revision Revision { get; set; }
    }
}
