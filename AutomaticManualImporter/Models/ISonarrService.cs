using System.Collections.Generic;
using System.Threading.Tasks;
using AutomaticManualImporter.Models.ApiV3;

namespace AutomaticManualImporter.Models
{
    public interface ISonarrService
    {
        Task<V3Result> GetQueueItemsAsync();
        Task<ICollection<RemotePathMapping>> GetRemotePathMappingsAsync();
        Task<ICollection<ManualImportQuerryResult>> GetManualImportsAsync(string downloadId);
        Task<ManualImportCommandResult> PostManualImportAsync(File file);
        File MapManualImportResultToFile(ManualImportQuerryResult querryResult);
    }
}
