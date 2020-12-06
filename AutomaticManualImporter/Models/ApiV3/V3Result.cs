using System.Collections.Generic;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class V3Result
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Sortkey { get; set; }
        public string SortDirection { get; set; }
        public int TotalRecords { get; set; }
        public ICollection<Record> Records { get; set; }
    }
}
