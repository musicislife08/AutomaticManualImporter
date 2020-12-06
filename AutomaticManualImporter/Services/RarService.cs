using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutomaticManualImporter.Models;
using Microsoft.Extensions.Options;
using SharpCompress.Archives;
using SharpCompress.Archives.Rar;
using SharpCompress.Common;
using SharpCompress.Common.Rar;

namespace AutomaticManualImporter.Services
{
    public class RarService : IRarService
    {
        public RarSettings _settings;
        public RarService(IOptions<RarSettings> options)
        {
            _settings = options.Value;
        }

        public RarService(RarSettings settings)
        {
            _settings = settings;
        }

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
