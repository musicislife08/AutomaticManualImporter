using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomaticManualImporter.Models.Api
{
    public class Episode
    {
        public int SeriesId { get; set; }
        public int EpisodeFileId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
        public DateTimeOffset AirDateUtc { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public bool Monitored { get; set; }
        public int AbsoluteEpisodeNumber { get; set; }
        public bool Unverire3ddSceneNumbering { get; set; }
        public int Id { get; set; }
    }
}
