using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomaticManualImporter.Models
{
    public interface ISftpService
    {
        Task UploadMediaFileInPath(string path, string remotePath);
        Task<ICollection<string>> GetRemoteFilesAsync(string path);
        Task DownloadFileAsync(string path, string directory);
        Task DownloadFilesAsync(ICollection<string> files, string directory);
    }
}
