using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomaticManualImporter.Models
{
    public interface ISftpService
    {
        Task UploadFileAsync(string path, string remotePath);
        Task<ICollection<string>> GetRemoteFilesAsync(string path);
        Task DownloadFileAsync(string path);
        Task DownloadFilesAsync(ICollection<string> files);
    }
}
