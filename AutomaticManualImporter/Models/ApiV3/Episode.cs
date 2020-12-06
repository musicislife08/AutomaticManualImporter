using System;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class Episode
    {
        public int SeriesId { get; set; }
        public int EpisodeFileId { get; set; }
        public int SeasonNumber { get; set; }
        public int episodeNumber { get; set; }
        public string Title { get; set; }
        public string AirDate { get; set; }
        public DateTimeOffset AirDateUtc { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public bool Monitored { get; set; }
        public int AbsoluteEpisodeNumber { get; set; }
        public bool UnverifiedSceneNumbering { get; set; }
        public int Id { get; set; }
    }
}
