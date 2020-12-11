using System.Threading;
using System.Threading.Tasks;

namespace AutomaticManualImporter.Models
{
    public interface ITvService
    {
        Task Start(CancellationToken stoppingToken);
    }
}
