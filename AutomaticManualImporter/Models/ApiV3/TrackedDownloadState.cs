namespace AutomaticManualImporter.Models.ApiV3
{
    public enum TrackedDownloadState
    {
        Downloading,
        ImportPending,
        Importing,
        Imported,
        FailedPending,
        Failed,
        Ignored
    }
}
