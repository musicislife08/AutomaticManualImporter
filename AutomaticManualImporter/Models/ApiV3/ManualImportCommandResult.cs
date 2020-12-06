using System;

namespace AutomaticManualImporter.Models.ApiV3
{
    public class ManualImportCommandResult
    {
        public string Name { get; set; }
        public string CommandName { get; set; }
        public Body Body { get; set; }
        public string Priority { get; set; }
        public string Status { get; set; }
        public DateTimeOffset Queued { get; set; }
        public string Trigger { get; set; }
        public bool SendUpdatesToClient { get; set; }
        public bool UpdateScheduledTask { get; set; }
        public int Id { get; set; }
    }
}
