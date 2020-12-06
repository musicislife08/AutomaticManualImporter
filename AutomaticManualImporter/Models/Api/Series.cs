using System;
using System.Collections.Generic;

namespace AutomaticManualImporter.Models.Api
{
    public class Series
    {
        public string Title { get; set; }
        public string SortTitle { get; set; }
        public int SeasonCount { get; set; }
        public string Status { get; set; }
        public string Overview { get; set; }
        public string Network { get; set; }
        public string AirTime { get; set; }
        public ICollection<Image> Images { get; set; }
        public ICollection<Season> Seasons { get; set; }
        public int Year { get; set; }
        public string Path { get; set; }
        public int ProfileId { get; set; }
        public bool SeasonFolder { get; set; }
        public bool Monitored { get; set; }
        public bool UseSceneNumbering { get; set; }
        public int Runtime { get; set; }
        public int TvdbId { get; set; }
        public int TvRageId { get; set; }
        public int TvMazeId { get; set; }
        public DateTimeOffset FirstAired { get; set; }
        public DateTimeOffset LastInfoSync { get; set; }
        public string SeriesType { get; set; }
        public string CleanTitle { get; set; }
        public string ImdbId { get; set; }
        public string TitleSlug { get; set; }
        public string Certification { get; set; }
        public ICollection<string> Genres { get; set; }
        public ICollection<string> Tags { get; set; }
        public DateTimeOffset Added { get; set; }
        public Ratings Ratings { get; set; }
        public int QualityProfileId { get; set; }
        public int Id { get; set; }
    }
}
