namespace AutomaticManualImporter.Models
{
    public class Settings
    {
        public string SftpHost { get; set; }
        public int SftpPort { get; set; }
        public string SftpUsername { get; set; }
        public string SftpPassword { get; set; }
        public string SftpBasePath { get; set; }
        public string SonarrUrl { get; set; }
        public string SonarrApiKey { get; set; }
        public string RadarrUrl { get; set; }
        public string RadarrApiKey { get; set; }
        public string RemoteTvFolder { get; set; }
        public string RemoteMovieFolder { get; set; }
        public string TempFileProcessingPath { get; set; }
    }
}
