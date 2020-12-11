using System.IO;
using System.Linq;
using AutomaticManualImporter.Models;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;

namespace AutomaticManualImporter.Services
{
    public class RarService : IRarService
    {
        public void UnzipFolder(string path)
        {
            var files = Directory.GetFiles(path);
            // Find first file
            var baseFile = files.Single(x => x.EndsWith(".rar"));
            using var archive = RarArchive.Open(baseFile);
            foreach(var entry in archive.Entries.Where(entry => !entry.IsDirectory))
            {
                entry.WriteToDirectory(path, new ExtractionOptions()
                { 
                    ExtractFullPath = false,
                    Overwrite = true
                });
            }
        }
    }
}
